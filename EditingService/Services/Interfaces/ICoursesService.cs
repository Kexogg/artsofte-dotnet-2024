using Domain.Entities;

namespace Services.Interfaces;

public interface ICoursesService
{
    Task<Guid> CreateCourseAsync(Course course);
    
    Task<Course[]> GetCoursesAsync();
    
    Task<CourseWithParticipants?> GetCourseByIdAsync(Guid id);
    
    Task<Course> UpdateCourseAsync(Course course);
    
    Task AddEventToCourseAsync(Guid courseId, Guid eventId);
    
    Task RemoveEventFromCourseAsync(Guid courseId, Guid eventId);
    
    Task DeleteCourseAsync(Guid id);
}