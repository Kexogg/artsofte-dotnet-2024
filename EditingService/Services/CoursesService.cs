using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class CoursesService : ICoursesService
{
    private readonly ICoursesRepository _coursesRepository;
    private readonly IEventsRepository _eventsRepository;

    // ReSharper disable once ConvertToPrimaryConstructor
    public CoursesService(ICoursesRepository coursesRepository, IEventsRepository eventsRepository)
    {
        _coursesRepository = coursesRepository;
        _eventsRepository = eventsRepository;
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

    public async Task<Course?> GetCourseByIdAsync(Guid id)
    {
        return await _coursesRepository.GetByIdAsync(id);
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