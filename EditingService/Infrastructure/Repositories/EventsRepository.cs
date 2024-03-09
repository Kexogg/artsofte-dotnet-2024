using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class EventsRepository: BaseRepository<Event>, IEventsRepository;