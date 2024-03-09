using Api.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Api.Controllers;

[ApiController]
[Route("api/editor/events")]
public class EventsController : ControllerBase
{
    private readonly IEventsService _eventsService;

    public EventsController(IEventsService eventsService)
    {
        _eventsService = eventsService;
    }
    
    [HttpGet]
    [ProducesResponseType<List<GetEventResponse>>(200)]
    public async Task<IActionResult> GetEvents()
    {
        var events = await _eventsService.GetEventsAsync();
        return Ok(events);
    }
    
    [HttpGet("{eventId:guid}")]
    [ProducesResponseType<GetEventResponse>(200)]
    public async Task<IActionResult> GetEventById([FromRoute] Guid eventId)
    {
        var @event = await _eventsService.GetEventByIdAsync(eventId);
        return Ok(@event);
    }
    
    [HttpPost]
    [ProducesResponseType<CreateResponse>(200)]
    public async Task<IActionResult> CreateEvent([FromBody] CreateEventRequest request)
    {
        var response = await _eventsService.CreateEventAsync(new Event
        {
            Name = request.Name,
            Description = request.Description,
            StartTime = request.StartDate,
            EndTime = request.EndDate,
            Location = request.Location 
        });
        return Ok(response);
    }
    
    [HttpPut("{eventId:guid}")]
    [ProducesResponseType<GetEventResponse>(200)]
    public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] CreateEventRequest request)
    {
        var response = await _eventsService.UpdateEventAsync(new Event
        {
            Id = eventId,
            Name = request.Name,
            Description = request.Description,
            StartTime = request.StartDate,
            EndTime = request.EndDate,
            Location = request.Location
        });
        return Ok(response);
    }
    
    [HttpDelete("{eventId:guid}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
    {
        await _eventsService.DeleteEventAsync(eventId);
        return Ok();
    }
}