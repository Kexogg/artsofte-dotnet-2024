using System.Security.Principal;
using Core.HttpLogic.Services;
using Core.HttpLogic.Services.Interfaces;
using DataExchange.Identity;
using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IEventsRepository _eventsRepository;
    private readonly IHttpRequestService _httpClient;
    private readonly IIdentityDataService _identityDataService;

    // ReSharper disable once ConvertToPrimaryConstructor
    public CoursesService(ICoursesRepository coursesRepository, IEventsRepository eventsRepository,
        IHttpRequestService httpClient, IIdentityDataService identityDataService)
    {
        _coursesRepository = coursesRepository;
        _eventsRepository = eventsRepository;
        _httpClient = httpClient;
        _identityDataService = identityDataService;
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
            var user = await _identityDataService.GetUserByIdAsync(participantId, "chat");
            courseWithParticipants.Participants[index] = new Participant
            {
                Id = participantId,
                Name = user.Name,
            };
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