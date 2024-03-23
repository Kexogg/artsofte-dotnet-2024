using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IEventsRepository _eventsRepository;
    private readonly IHttpRequestService _httpClient;

    // ReSharper disable once ConvertToPrimaryConstructor
    public CoursesService(ICoursesRepository coursesRepository, IEventsRepository eventsRepository, IHttpRequestService httpClient)
    {
        _coursesRepository = coursesRepository;
        _eventsRepository = eventsRepository;
        _httpClient = httpClient;
    }

    public async Task<Guid> CreateCourseAsync(Course course)
    {
        if (await _coursesRepository.ExistsAsync(course.Id))
        {
            throw new InvalidOperationException();
        }

        return await _coursesRepository.CreateAsync(course);
    }

    public async Task<Course[]> GetCoursesAsync()
    {
        return await _coursesRepository.GetAllAsync();
    }

    public async Task<CourseWithParticipants?> GetCourseByIdAsync(Guid id)
    {
        var course = await _coursesRepository.GetByIdAsync(id);
        if (course == null)
        {
            return null;
        }

        var httpRequestData = new HttpRequestData()
        {
            Method = HttpMethod.Get
        };
        
        var courseWithParticipants = new CourseWithParticipants
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description,
            Events = course.Events,
            Participants = new Participant[(course.Participants ?? []).Length]
        };
        
        if (course.Participants == null)
        {
            return courseWithParticipants;
        }
        
        for (var index = 0; index < course.Participants.Length; index++)
        {
            var participantId = course.Participants[index];
            httpRequestData.Uri = new Uri($"http://localhost:5212/api/users/{participantId}");
            var response = await _httpClient.SendRequestAsync<Participant>(httpRequestData);
            if (response == null)
            {
                throw new InvalidOperationException();
            }
            courseWithParticipants.Participants[index] = response.Body;
        }

        return courseWithParticipants;
    }

    public async Task<Course> UpdateCourseAsync(Course course)
    {
        if (!await _coursesRepository.ExistsAsync(course.Id))
        {
            throw new InvalidOperationException();
        }

        return await _coursesRepository.UpdateAsync(course);
    }

    public async Task AddEventToCourseAsync(Guid courseId, Guid eventId)
    {
        var course = await _coursesRepository.GetByIdAsync(courseId);
        var @event = await _eventsRepository.GetByIdAsync(eventId);
        if (course == null || @event == null)
        {
            throw new InvalidOperationException();
        }

        var events = course.Events ?? Array.Empty<Event>();
        var newCourse = course with { Events = events.Append(@event).ToArray() };
        await _coursesRepository.UpdateAsync(newCourse);
    }

    public async Task RemoveEventFromCourseAsync(Guid courseId, Guid eventId)
    {
        var course = await _coursesRepository.GetByIdAsync(courseId);
        if (course?.Events == null || !await _eventsRepository.ExistsAsync(eventId))
        {
            throw new InvalidOperationException();
        }

        var newCourse = course with { Events = course.Events.Where(value => value.Id != eventId).ToArray() };
        await _coursesRepository.UpdateAsync(newCourse);
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        await _coursesRepository.DeleteAsync(id);
    }
}