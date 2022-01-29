using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    public class UserLogin
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [MinLength(2), MaxLength(10)]
        public string UserName { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get { return Password; } set 
            {
                bool x = false, y = false, z = false;
                int i;
                foreach (char c in value)
                {
                    if(c.ToString().ToUpper() == c.ToString()) { x = true; }
                    if(c.ToString().ToLower() == c.ToString()) { y = true; }
                    var t = int.TryParse(c.ToString(), out i);
                    if (t) { z = true; }
                    if (x && y && z) { Password = value; return; }
                }
            } }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
    }
}
