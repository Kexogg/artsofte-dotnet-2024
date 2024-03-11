using Domain.Entities;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class CoursesRepository : BaseRepository<Course>, ICoursesRepository;