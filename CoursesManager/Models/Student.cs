using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Models
{
   public class Student //:UserDetails
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string email { get; set; }

        public userLogin userLogin { get; set; }

        public float Payment { get; set; }

        public ICollection<Course> Course { get; set; }
    }
}
