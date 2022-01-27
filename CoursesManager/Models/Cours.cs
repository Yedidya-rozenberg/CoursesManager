using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public char CourseStatus { get; set; }

        [ForeignKey("Teacher")]
        public int UserDetailsID { get; set; }
        virtual public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Unit> Units { get; set; }
    }
}
