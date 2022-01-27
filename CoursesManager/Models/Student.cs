using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    [Table("Students")]
    public class Student :UserDetails
    {
        public Student()
        {
            this.Type = TypeOfUser.Student;
        }
     
        public float Payment { get; set; }

        public ICollection<Course> StudyCourses { get; set; }
    }
}
