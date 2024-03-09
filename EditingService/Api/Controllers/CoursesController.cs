using Api.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/editor/courses")]
public class CoursesController : ControllerBase
{
    private readonly ICoursesService _coursesService;

    public CoursesController(ICoursesService coursesService)
    {
        _coursesService = coursesService;
    }

    [HttpGet]
    [ProducesResponseType<List<GetCourseResponse>>(200)]
    public async Task<IActionResult> GetCourses()
    {
        var courses = await _coursesService.GetCoursesAsync();
        return Ok(courses);
    }

    [HttpGet("{courseId:guid}")]
    [ProducesResponseType<GetCourseResponse>(200)]
    public async Task<IActionResult> GetCourseById([FromRoute] Guid courseId)
    {
        var course = await _coursesService.GetCourseByIdAsync(courseId);
        return Ok(course);
    }

    [HttpPost]
    [ProducesResponseType<CreateResponse>(200)]
    public async Task<IActionResult> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var response = await _coursesService.CreateCourseAsync(new Course
        {
            Name = request.Name,
            Description = request.Description
        });
        return Ok(response);
    }
    
    [HttpPut("{courseId:guid}")]
    [ProducesResponseType<GetCourseResponse>(200)]
    public async Task<IActionResult> UpdateCourse([FromRoute] Guid courseId, [FromBody] GetCourseResponse request)
    {
        var response = await _coursesService.UpdateCourseAsync(new Course
        {
            Id = courseId,
            Name = request.Name,
            Description = request.Description
        });
        return Ok(response);
    }
    
    [HttpDelete("{courseId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteCourse([FromRoute] Guid courseId)
    {
        await _coursesService.DeleteCourseAsync(courseId);
        return Ok();
    }
    
    [HttpPost("{courseId:guid}/events/{eventId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> AddEventToCourse([FromRoute] Guid courseId, [FromRoute] Guid eventId)
    {
        await _coursesService.AddEventToCourseAsync(courseId, eventId);
        return Ok();
    }
    
    [HttpDelete("{courseId:guid}/events/{eventId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> RemoveEventFromCourse([FromRoute] Guid courseId, [FromRoute] Guid eventId)
    {
        await _coursesService.RemoveEventFromCourseAsync(courseId, eventId);
        return Ok();
    }
}