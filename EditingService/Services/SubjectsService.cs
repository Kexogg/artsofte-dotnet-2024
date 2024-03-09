using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class SubjectsService : ISubjectsService
{
    private readonly ISubjectsRepository _subjectsRepository;
    private readonly ICoursesRepository _coursesRepository;
    // ReSharper disable once ConvertToPrimaryConstructor
    public SubjectsService(ISubjectsRepository subjectsRepository, ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;
        _subjectsRepository = subjectsRepository;
    }

    public async Task<Guid> CreateSubjectAsync(Subject subject)
    {
        if (await _subjectsRepository.ExistsAsync(subject.Id))
        {
            throw new InvalidOperationException();
        }
        return await _subjectsRepository.CreateAsync(subject);
    }

    public async Task<Subject[]> GetSubjectsAsync()
    {
        return await _subjectsRepository.GetAllAsync();
    }

    public async Task<Subject?> GetSubjectByIdAsync(Guid id)
    {
        return await _subjectsRepository.GetByIdAsync(id);
    }

    public async Task<Subject> UpdateSubjectAsync(Subject subject)
    {
        if (!await _subjectsRepository.ExistsAsync(subject.Id))
        {
            throw new InvalidOperationException();
        }
        return await _subjectsRepository.UpdateAsync(subject);
    }

    public async Task AddCourseToSubjectAsync(Guid subjectId, Guid courseId)
    {
        var subject = await _subjectsRepository.GetByIdAsync(subjectId);
        var course = await _coursesRepository.GetByIdAsync(courseId);
        if (subject == null || course == null)
        {
            throw new InvalidOperationException();
        }
        var courses = subject.Courses ?? Array.Empty<Course>();
        var newSubject = subject with { Courses = courses.Append(course).ToArray() };
        await _subjectsRepository.UpdateAsync(newSubject);
    }

    public async Task RemoveCourseFromSubjectAsync(Guid subjectId, Guid courseId)
    {
        var subject = await _subjectsRepository.GetByIdAsync(subjectId);
        if (subject?.Courses == null || await _coursesRepository.GetByIdAsync(courseId) == null)
        {
            throw new InvalidOperationException();
        }
        var newSubject = subject with { Courses = subject.Courses.Where(c => c.Id != courseId).ToArray() };
        await _subjectsRepository.UpdateAsync(newSubject);
    }

    public async Task DeleteSubjectAsync(Guid id)
    {
        await _subjectsRepository.DeleteAsync(id);
    }
}