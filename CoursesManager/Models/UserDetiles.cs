using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Models
{
    [Table("Users")]
    public class UserDetails
    {
        [Key]
        public int UserDetailsID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Phone]
        public int PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [ForeignKey("UserLogins")]
        public int UserLoginID { get; set; }
        virtual public UserLogin UserLogin { get; set; }

        [NotMapped]
        public TypeOfUser Type { get; set; }

    }
}
