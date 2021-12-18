using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Models
{
    public class Teacher //: UserDetiles
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LestName { get; set; }

        [Phone]
        public int PhonNumber { get; set; }
        [EmailAddress]
        public string email { get; set; }

        public UserLoggin userLoggin { get; set; }

        public float Selery { get; set; }

        public ICollection<Cours> TeachCours{ get; set; }

    }
}
