using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoursesManager.Models
{
   public class Unit
    {
        [Key]
        public int UnitID { get; set; }
        [ForeignKey("Cours")]
        public int coursID { get; set; }
        public Cours Cours { get; set; }

        [Required]
        public string UnitName { get; set; }

        public string StudyContent { get; set; }
        public string Questions { get; set; }
    }
}
