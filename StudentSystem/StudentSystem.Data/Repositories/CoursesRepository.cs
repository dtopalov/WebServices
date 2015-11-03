namespace StudentSystem.Data.Repositories
{
    using StudentSystem.Models;

    public class CoursesRepository : GenericRepository<Course>, IGenericRepository<Course>
    {
        public CoursesRepository(IStudentSystemDbContext context)
            : base(context)
        {
        }
    }
}