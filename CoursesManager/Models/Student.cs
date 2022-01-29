using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesManager.Models
{
    public class Student //:UserDetails
    {
        [Key]
        public int StudentID { get; set; }
        [Required]
        [MinLength(2), MaxLength(10)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(2), MaxLength(10)]
        public string LastName { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public UserLogin UserLogin { get; set; }

        public float Payment { get; set; }

        public ICollection<Course> Course { get; set; }
    }
}
