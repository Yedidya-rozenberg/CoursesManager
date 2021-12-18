using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Models
{
    public class Cours
    {
        [Key]
        public int CoursID { get; set; }
        [Required]
        public string CoursName { get; set; }
        [Required]
        public char CuorsStatus { get; set; }

        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }

        public ICollection<Unit> Units { get; set; }
    }
}
