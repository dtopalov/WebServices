namespace StudentSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Student
    {
        private ICollection<Student> trainees;

        private ICollection<Course> courses;

        private ICollection<Homework> homeworks;

        private StudentInfo additionalInformation;

        public Student()
        {
            this.courses = new HashSet<Course>();
            this.homeworks = new HashSet<Homework>();
            this.trainees = new HashSet<Student>();
            this.additionalInformation = new StudentInfo();
        }

        [Key]
        public int StudentIdentification { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        public string LastName { get; set; }

        public int Level { get; set; }

        public virtual Student Assistant { get; set; }

        [InverseProperty("Assistant")]
        public virtual ICollection<Student> Trainees
        {
            get
            {
                return this.trainees;
            }
            set
            {
                this.trainees = value;
            }
        }

        public virtual ICollection<Course> Courses
        {
            get
            {
                return this.courses;
            }
            set
            {
                this.courses = value;
            }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get
            {
                return this.homeworks;
            }
            set
            {
                this.homeworks = value;
            }
        }

        public StudentInfo AdditionalInformation
        {
            get
            {
                return this.additionalInformation;
            }
            set
            {
                this.additionalInformation = value;
            }
        }

        [NotMapped]
        public bool IsAssistant
        {
            get
            {
                return this.Trainees.Count > 0;
            }
        }
    }
}
