namespace StudentSystem.Api.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Data;

    using Models;

    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class TestsController : ApiController
    {
        private static readonly StudentSystemDbContext data = new StudentSystemDbContext();
        private static TestsRepository testsRepository = new TestsRepository(data);

        // GET api/Tests
        public IEnumerable<TestServiceModel> Get()
        {
            var result = testsRepository.All().Select(x => new TestServiceModel
            {
                CourseId = x.CourseId,
                Id = x.Id
            }).ToList();

            return result;
        }

        // GET api/Tests/2
        public TestServiceModel Get(int id)
        {
            var result = testsRepository.All().Where(x => x.Id == id).Select(x => new TestServiceModel
            {
                CourseId = x.CourseId,
                Id = x.Id
            }).FirstOrDefault();
            return result;
        }

        // POST api/Tests
        public IHttpActionResult Post([FromBody]Test value)
        {
            var test = new Test
                           {
                               CourseId = value.CourseId
                           };

            testsRepository.Add(value);
            data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Tests/5
        public IHttpActionResult Put(int id, [FromBody]Test value)
        {
            var testToUpdate = testsRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (testToUpdate == null)
            {
                return this.BadRequest("No such test");
            }

            if (value.Course != null)
            {
                testToUpdate.Course = value.Course;
            }

            if (value.Students != null)
            {
                testToUpdate.Students = value.Students;
            }

            testsRepository.Update(testToUpdate);
            data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Found));
        }

        // DELETE api/Tests/5
        public void Delete(int id)
        {
            var testToRemove = testsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            testsRepository.Delete(testToRemove);
            data.SaveChanges();
        }
    }
}
