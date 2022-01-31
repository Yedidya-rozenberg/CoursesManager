﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoursesManager.Models
{
    public class Teacher //: UserDetails
    {
        [Key]
        public int TeacherID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public UserLogin UserLogin { get; set; }

        public float Salary { get; set; }

        public ICollection<Course> TeachCourses{ get; set; }

    }
}
