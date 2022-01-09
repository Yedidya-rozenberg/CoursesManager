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
        public Request(int CourseID, char requestCode, string requestDetails, int? studentID = null, int? teacherID = null)
        {
            this.CourseID = CourseID;
            this.RequestCode = requestCode;
            this.RequestDetails = requestDetails;
            this.StudentID = studentID;
            this.TeacherID = teacherID;
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [ForeignKey("Student")]
        public int? StudentID { get; set; }
        public Student Student { get; set; }

        [ForeignKey("Teacher")]
        public int? TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        [Required]
        public char RequestCode { get; set; }
        [Required]
        public string RequestDetails { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        [Required]
        public string RequestStatus { get; set; }
    }
}
