using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudentSystem.Api.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using StudentSystem.Api.Models;
    using StudentSystem.Data;
    using StudentSystem.Data.Repositories;
    using StudentSystem.Models;

    public class CoursesController : ApiController
    {
        private static readonly StudentSystemDbContext data = new StudentSystemDbContext();
        private static CoursesRepository coursesRepository = new CoursesRepository(data);

        // GET api/Courses
        public IEnumerable<CourseServiceModel> Get()
        {
            var result = coursesRepository.All().Select(x => new CourseServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).ToList();

            return result;
        }

        // GET api/Courses/?name=...
        public CourseServiceModel Get(string name)
        {
            var result = coursesRepository.All().Where(x => x.Name == name).Select(x => new CourseServiceModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).FirstOrDefault();
            return result;
        }

        // POST api/Courses
        public IHttpActionResult Post([FromBody]Course value)
        {
            coursesRepository.Add(value);
            data.SaveChanges();
            return this.CreatedAtRoute("Courses", "api/Courses", value);
        }

        // PUT api/Courses/{guid}
        public IHttpActionResult Put(Guid id, [FromBody]Course value)
        {
            var courseToUpdate = coursesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (courseToUpdate == null)
            {
                return this.BadRequest("No such Course");
            }

            if (value.Description != null)
            {
                courseToUpdate.Description = value.Description;
            }

            if (value.Students != null)
            {
                courseToUpdate.Students = value.Students;
            }

            courseToUpdate.Id = value.Id;

            coursesRepository.Update(courseToUpdate);
            data.SaveChanges();

            return this.ResponseMessage(new HttpResponseMessage(HttpStatusCode.Found));
        }

        // DELETE api/Courses/{guid}
        public IHttpActionResult Delete(Guid id)
        {
            var courseToRemove = coursesRepository.All().Where(x => x.Id == id).FirstOrDefault();

            if (courseToRemove == null)
            {
                return this.BadRequest("No such course");
            }

            coursesRepository.Delete(courseToRemove);
            data.SaveChanges();
            return this.Ok();
        }
    }
}