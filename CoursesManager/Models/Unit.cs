using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [Required]
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }
    }
}
