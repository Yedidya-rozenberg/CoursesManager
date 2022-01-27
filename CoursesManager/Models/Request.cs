using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoursesManager.Models
{
    public class Request
    {
        public Request()
        {
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        public Request(int CourseID, RequestCode requestCode, int userDetailsesID, string requestDetails = null)
        {
            this.CourseID = CourseID;
            this.Code = requestCode;
            this.RequestDetails = requestDetails;
            this.UserDetailsID = userDetailsesID;
            this.RequestTime = DateTime.Now;
            this.RequestStatus = "Open";
        }
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("Course")]
        public int CourseID { get; set; }
        public Course Course { get; set; }

        [ForeignKey("UserDetailses")]
        public int UserDetailsID { get; set; }
        public UserDetails UserDetails { get; set; }

        [Required]
        public RequestCode Code { get; set; }
        [Required]
        public string RequestDetails { get; set; }
        [Required]
        public DateTime RequestTime { get; set; }
        [Required]
        public string RequestStatus { get; set; }
    }

    public enum RequestCode
    {
        ActiveCourse,
        DeleteCourse,
        Register,
        RemoveStudent,
        CreateCourse
    }
}
