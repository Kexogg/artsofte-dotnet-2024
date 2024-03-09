using Domain.Entities;

namespace Services.Interfaces;

public interface IEventsService
{
    Task<Guid> CreateEventAsync(Event @event);
    
    Task<Event[]> GetEventsAsync();
    
    Task<Event?> GetEventByIdAsync(Guid id);
    
    Task<Event> UpdateEventAsync(Event @event);
    
    Task DeleteEventAsync(Guid id);
}