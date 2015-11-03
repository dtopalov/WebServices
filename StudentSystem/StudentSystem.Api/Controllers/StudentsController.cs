namespace StudentSystem.Api.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using Data;

    using Models;

    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class StudentsController : ApiController
    {
        private static readonly StudentSystemDbContext data = new StudentSystemDbContext();
        private static StudentsRepository studentsRepository = new StudentsRepository(data);

        // GET api/Students
        public IEnumerable<StudentServiceModel> Get()
        {
            var result = studentsRepository.All().Select(x => new StudentServiceModel
                                                            {
                                                                FirstName = x.FirstName,
                                                                LastName = x.LastName,
                                                                Level = x.Level,
                                                                StudentIdentification = x.StudentIdentification
                                                            }).ToList();

            return result;
        }

        // GET api/Students/2
        public StudentServiceModel Get(int id)
        {
            var result = studentsRepository.All().Where(x => x.StudentIdentification == id).Select(x => new StudentServiceModel
                                                                                                      {
                                                                                                          FirstName = x.FirstName,
                                                                                                          LastName = x.LastName,
                                                                                                          Level = x.Level,
                                                                                                          StudentIdentification = x.StudentIdentification
                                                                                                      }).FirstOrDefault();
            return result;
        }

        // POST api/Students
        public IHttpActionResult Post([FromBody]Student value)
        {
            var student = new Student
                              {
                                  FirstName = value.FirstName,
                                  LastName = value.LastName
                              };

            studentsRepository.Add(student);
            data.SaveChanges();
            return this.StatusCode(HttpStatusCode.Created);
        }

        // PUT api/Students/5
        public IHttpActionResult Put(int id, [FromBody]Student value)
        {
            var studentToUpdate = studentsRepository.All().Where(x => x.StudentIdentification == id).FirstOrDefault();

            if (studentToUpdate == null)
            {
                return this.BadRequest("No such student");
            }

            if (value.FirstName != null)
            {
                studentToUpdate.FirstName = value.FirstName;
            }

            if (value.LastName != null)
            {
                studentToUpdate.LastName = value.LastName;
            }

            if (value.AdditionalInformation != null)
            {
                studentToUpdate.AdditionalInformation = value.AdditionalInformation;
            }

            if (value.Assistant != null)
            {
                studentToUpdate.Assistant = value.Assistant;
            }

            if (value.Level != 0)
            {
                studentToUpdate.Level = value.Level;
            }

            if (value.Courses != null)
            {
                studentToUpdate.Courses = value.Courses;
            }

            if (value.Homeworks != null)
            {
                studentToUpdate.Homeworks = value.Homeworks;
            }

            if (value.Trainees != null)
            {
                studentToUpdate.Trainees = value.Trainees;
            }

            studentsRepository.Update(studentToUpdate);
            data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Found));
        }

        // DELETE api/Students/5
        public void Delete(int id)
        {
            var studentToRemove = studentsRepository.All().Where(x => x.StudentIdentification == id).FirstOrDefault();
            studentsRepository.Delete(studentToRemove);
            data.SaveChanges();
        }
    }
}
