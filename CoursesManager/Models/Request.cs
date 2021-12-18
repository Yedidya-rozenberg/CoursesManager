using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Models
{
    public class Request
    {
        public Request()
        {
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        public Request(int coursID, char requesrCode, string requestDetiles, int? studentID = null, int? teacherID = null)
        {
            this.CoursID = coursID;
            this.RequestCode = requesrCode;
            this.RequestDetiles = requestDetiles;
            this.StudentID = studentID;
            this.TeacherID = teacherID;
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("Cours")]
        public int CoursID { get; set; }
        public Cours Cours { get; set; }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public char RequestCode { get; set; }
        [Required]
        public string RequestDetiles { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        [Required]
        public string RequestStatus { get; set; }
    }
}
