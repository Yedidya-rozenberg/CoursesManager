using CoursesManager.DAL;
using CoursesManager.Models;
using System;

namespace CoursesManager.Logic.CourseState
{
    public class StudentState : ICourseState
    {
        private Course _course;
        private UserDetails _user;
        public StudentState(Course course, UserDetails user)
        {
            this._course = course;
            this._user = user;
        }
        public ICourseState Activate()
        {
            Display.Message("You do not have permission to do this.");
            return this;

        }

        public ICourseState Editing()
        {
            Display.Message("You do not have permission to do this.");
            return this;

        }

        public void EnterUnit(int unitID)
        {
            if (CourseAccess.CheckStudentCourse(_user ,_course.CourseID))
            {
                Display.UnitScreen(unitID);
            }
            else
            { Display.Message("You are not registered for this course."); }
        }

        public void OtherOptions(CourseContext CourseContext)
        {
            Display.Message("There are no other options available.");
        }

        public void Register()
        {
            if (!CourseAccess.CheckStudentCourse(_user, _course.CourseID))
            {

                Display.Message("Do you want to register for the course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(_course.CourseID, RequestCode.Register, _user.UserDetailsID);
                    var requsrID = RequestAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request was sent.";
                    Display.Message(massege);
                    // Execution Request
                    if ((requsrID != 0))
                    {
                        var success = RequestAccess.MakeRequest(request);
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
            if (CourseAccess.CheckStudentCourse(_user, _course.CourseID))
            {
                Display.Message("Do you want to remove from this course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(_course.CourseID, RequestCode.RemoveStudent, _user.UserDetailsID);
                    var requsrID = RequestAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request was sent.";
                    Display.Message(massege);
                    // Execution Request
                    if ((requsrID != null))
                    {
                        var success = RequestAccess.MakeRequest(request);
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
