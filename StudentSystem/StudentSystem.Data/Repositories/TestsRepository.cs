namespace StudentSystem.Data.Repositories
{
    using System.Linq;

    using StudentSystem.Models;

    public class TestsRepository : GenericRepository<Test>, IGenericRepository<Test>
    {
        public TestsRepository(IStudentSystemDbContext context)
            : base(context)
        {
        }
    }
}