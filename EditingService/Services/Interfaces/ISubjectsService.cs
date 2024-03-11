using Domain.Entities;

namespace Services.Interfaces;

public interface ISubjectsService
{
    Task<Guid> CreateSubjectAsync(Subject subject);
    
    Task<Subject[]> GetSubjectsAsync();
    
    Task<Subject?> GetSubjectByIdAsync(Guid id);
    
    Task<Subject> UpdateSubjectAsync(Subject subject);
    
    Task AddCourseToSubjectAsync(Guid subjectId, Guid courseId);
    
    Task RemoveCourseFromSubjectAsync(Guid subjectId, Guid courseId);
    
    Task DeleteSubjectAsync(Guid id);
}