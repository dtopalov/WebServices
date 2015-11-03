namespace StudentSystem.Api.Models
{
    using System;

    public class TestServiceModel
    {
        public TestServiceModel()
        {
        }

        public int Id { get; set; }

        public virtual Guid CourseId { get; set; }
    }
}