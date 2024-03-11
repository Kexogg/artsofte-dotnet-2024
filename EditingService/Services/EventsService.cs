using Domain.Entities;
using Domain.Interfaces;
using Services.Interfaces;

namespace Services;

public class EventsService : IEventsService
{
    private readonly IEventsRepository _eventsRepository;

    // ReSharper disable once ConvertToPrimaryConstructor
    public EventsService(IEventsRepository eventsRepository)
    {
        _eventsRepository = eventsRepository;
    }

    public async Task<Guid> CreateEventAsync(Event @event)
    {
        if (await _eventsRepository.ExistsAsync(@event.Id))
        {
            throw new InvalidOperationException();
        }
        return await _eventsRepository.CreateAsync(@event);
    }

    public async Task<Event[]> GetEventsAsync()
    {
        return await _eventsRepository.GetAllAsync();
    }

    public async Task<Event?> GetEventByIdAsync(Guid id)
    {
        return await _eventsRepository.GetByIdAsync(id);
    }

    public async Task<Event> UpdateEventAsync(Event @event)
    {
        if (!await _eventsRepository.ExistsAsync(@event.Id))
        {
            throw new InvalidOperationException();
        }
        return await _eventsRepository.UpdateAsync(@event);
    }

    public async Task DeleteEventAsync(Guid id)
    {
        await _eventsRepository.DeleteAsync(id);
    }
}