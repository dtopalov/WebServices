namespace StudentSystem.Data.Repositories
{
    using StudentSystem.Models;

    public class HomeworksRepository : GenericRepository<Homework>, IGenericRepository<Homework>
    {
        public HomeworksRepository(IStudentSystemDbContext context)
            : base(context)
        {
        }
    }
}
