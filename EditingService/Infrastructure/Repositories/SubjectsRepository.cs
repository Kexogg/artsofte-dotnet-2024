using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class SubjectsRepository : BaseRepository<Subject>, ISubjectsRepository;