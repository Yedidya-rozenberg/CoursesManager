using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    [Table("Teachers")]
    public class Teacher : UserDetails
    {
        public Teacher()
        {
            this.Type = TypeOfUser.Teacher;
        }

        public float Salary { get; set; }

       virtual public ICollection<Course> TeachCourses{ get; set; }

    }
}
