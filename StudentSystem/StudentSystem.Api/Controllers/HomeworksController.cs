namespace StudentSystem.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StudentSystem.Api.Models;
    using StudentSystem.Data;
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class HomeworksController : ApiController
    {
        private static readonly StudentSystemDbContext data = new StudentSystemDbContext();
        private static HomeworksRepository homeworksRepository = new HomeworksRepository(data);

        // GET api/Homeworks
        public IEnumerable<HomeworkServiceModel> Get()
        {
            var result = homeworksRepository.All().Select(x => new HomeworkServiceModel
            {
                FileUrl = x.FileUrl,
                TimeSent = x.TimeSent,
                Student = x.Student,
                Course = x.Course
            }).ToList();

            return result;
        }

        // GET api/Homeworks/2
        public HomeworkServiceModel Get(int id)
        {
            var result = homeworksRepository.All().Where(x => x.Id == id).Select(x => new HomeworkServiceModel
            {
                FileUrl = x.FileUrl,
                TimeSent = x.TimeSent,
                Student = x.Student,
                Course = x.Course
            }).FirstOrDefault();
            return result;
        }

        // POST api/Homeworks
        public IHttpActionResult Post([FromBody]Homework value)
        {
            var test = new Homework
            {
                FileUrl = value.FileUrl,
                TimeSent = value.TimeSent,
                Student = value.Student,
                Course = value.Course
            };

            homeworksRepository.Add(value);
            data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Homeworks/5
        public IHttpActionResult Put(int id, [FromBody]Homework value)
        {
            var homeworkToUpdate = homeworksRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (homeworkToUpdate == null)
            {
                return this.BadRequest("No such homework");
            }

            if (value.Course != null)
            {
                homeworkToUpdate.Course = value.Course;
            }

            if (value.Student != null)
            {
                homeworkToUpdate.Student = value.Student;
            }

            if (value.FileUrl != null)
            {
                homeworkToUpdate.FileUrl = value.FileUrl;
            }

            if (value.TimeSent != DateTime.MinValue)
            {
                homeworkToUpdate.TimeSent = value.TimeSent;
            }

            if (value.StudentIdentification != 0)
            {
                homeworkToUpdate.StudentIdentification = value.StudentIdentification;
            }

            homeworksRepository.Update(homeworkToUpdate);
            data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Found));
        }

        // DELETE api/Homeworks/5
        public void Delete(int id)
        {
            var testToRemove = homeworksRepository.All().Where(x => x.Id == id).FirstOrDefault();
            homeworksRepository.Delete(testToRemove);
            data.SaveChanges();
        }
    }
}
