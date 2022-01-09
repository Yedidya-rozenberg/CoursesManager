using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CourseState
{
    public class StudentState : ICourseState
    {
        private Course course;
        private Student Student;
        public StudentState(Course course, Student student)
        {
            this.Course = course;
            this.Student = student;
        }
        public ICourseState Activate()
        {
            Console.WriteLine("You do not have permission to do this.");
            return this;

        }

        public ICourseState Editing()
        {
            Console.WriteLine("You do not have permission to do this.");
            return this;

        }

        public void EnterUnit(int unitID)
        {
            if (CourseAccess.CheckStudentCourse(Student.StudentID ,Course.CourseID))
            {
                Display.UnitScreen(unitID);
            }
            else
            { Console.WriteLine("You are not registered for this course."); }
        }

        public void OtherOptions(CourseContext CourseContext)
        {
            Console.WriteLine("There are no other options available.");
        }

        public void Register()
        {
            if (!CourseAccess.CheckStudentCourse(Student.StudentID, Course.CourseID))
            {

                Console.WriteLine("Do you want to register for the course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(Course.CourseID, 'G', "Register student", Student.StudentID);
                    var requsrID = requestAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request was sent.";
                    Display.Message(massege);
                    // Execution Request
                    if ((requsrID != 0))
                    {
                        var success = requestAccess.MakeRequest(request);
                        massege = (success) ? "the transaction was completed successfully." : "The request was denied.";
                        Display.Message(massege);
                    }
                }
                else
                {
                    Display.Message("The action has been canceled.");
                }
            }
            else
            { Display.Message("You are already enrolled in this course"); }
        }

        public ICourseState Remove()
        {
            if (CourseAccess.CheckStudentCourse(Student.StudentID, Course.CourseID))
            {
                Console.WriteLine("Do you want to remove from this course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(Course.CourseID, 'M', "Remove student from course", Student.StudentID);
                    var requsrID = requestAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request was sent.";
                    Console.WriteLine(massege);
                    // Execution Request
                    if ((requsrID != null))
                    {
                        var success = requestAccess.MakeRequest(request);
                        massege = (success) ? "the transaction completed successfully." : "The request was denied.";
                        Display.Message(massege);
                    }
                }
                else
                {
                    Display.Message("The action has been canceled.");
                }
            }
            else
            { Display.Message("You are not registered for this course"); }
                return this;

        }
    }
}
