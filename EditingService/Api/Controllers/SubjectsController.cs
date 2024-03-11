using Api.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/editor/subjects")]
public class SubjectsController : ControllerBase
{
    private readonly ISubjectsService _subjectsService;

    public SubjectsController(ISubjectsService subjectsService)
    {
        _subjectsService = subjectsService;
    }
    
    [HttpGet]
    [ProducesResponseType<List<GetSubjectResponse>>(200)]
    public async Task<IActionResult> GetSubjects()
    {
        var subjects = await _subjectsService.GetSubjectsAsync();
        return Ok(subjects);
    }
    
    [HttpGet("{subjectId:guid}")]
    [ProducesResponseType<GetSubjectResponse>(200)]
    public async Task<IActionResult> GetSubjectById([FromRoute] Guid subjectId)
    {
        var subject = await _subjectsService.GetSubjectByIdAsync(subjectId);
        return Ok(subject);
    }
    
    [HttpPost]
    [ProducesResponseType<CreateResponse>(200)]
    public async Task<IActionResult> CreateSubject([FromBody] CreateSubjectRequest request)
    {
        var response = await _subjectsService.CreateSubjectAsync(new Subject
        {
            Name = request.Name,
            Description = request.Description,
        });
        return Ok(response);
    }
    
    [HttpPut("{subjectId:guid}")]
    [ProducesResponseType<GetSubjectResponse>(200)]
    public async Task<IActionResult> UpdateSubject([FromRoute] Guid subjectId, [FromBody] CreateSubjectRequest request)
    {
        var response = await _subjectsService.UpdateSubjectAsync(new Subject
        {
            Id = subjectId,
            Name = request.Name,
            Description = request.Description,
        });
        return Ok(response);
    }
    
    [HttpPost("{subjectId:guid}/courses/{courseId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddCourseToSubject([FromRoute] Guid subjectId, [FromRoute] Guid courseId)
    {
        await _subjectsService.AddCourseToSubjectAsync(subjectId, courseId);
        return Ok();
    }
    
    [HttpDelete("{subjectId:guid}/courses/{courseId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveCourseFromSubject([FromRoute] Guid subjectId, [FromRoute] Guid courseId)
    {
        await _subjectsService.RemoveCourseFromSubjectAsync(subjectId, courseId);
        return Ok();
    }
    
    [HttpDelete("{subjectId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteSubject([FromRoute] Guid subjectId)
    {
        await _subjectsService.DeleteSubjectAsync(subjectId);
        return Ok();
    }
}