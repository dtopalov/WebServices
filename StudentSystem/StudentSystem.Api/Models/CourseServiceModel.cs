namespace StudentSystem.Api.Models
{
    using System;
    using System.Collections.Generic;

    using StudentSystem.Models;

    public class CourseServiceModel
    {
        public CourseServiceModel()
        {
            this.Id = Guid.NewGuid();
            this.Students = new HashSet<Student>();
            this.Homeworks = new HashSet<Homework>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }
    }
}