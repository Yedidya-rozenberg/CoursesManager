using CoursesManager.DAL;
using CoursesManager.Models;
using System;

namespace CoursesManager.Logic.CourseState
{
    public class EditState : ICourseState
    {
        private Course _course;
        private Teacher _teacher;
        public EditState(Course course, Teacher teacher)
        {
            this._course = course;
            this._teacher = teacher;
        }
        public ICourseState Activate()
        {
            Display.Message("Are you sure you want to active the course? y/n");
            string answer = Console.ReadLine();
            if (answer == "y")
            {// send request
                var request = new Request(_course.CourseID, 'A', "activate course", null, _teacher.TeacherID);
                var requsrID = RequestAccess.AddRequest(request);
                string massege = (requsrID == 0) ? "Error. try again" : "Request was sent.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = RequestAccess.MakeRequest(request);
                    if (success)
                    {
                        Display.Message("the transaction has completed successfully.");
                        return new ActiveState(_course, _teacher);
                    }
                    else
                    {
                        Display.Message("The request was denied.");
                        return this;
                    }
                }
                else
                {
                    return this;
                }
            }
            else
            {
                Display.Message("The action has been canceled.");
                return this;
            }
        }

        public ICourseState Editing()
        {
            Display.Message("You are in edit mode.");
            return this;
        }

        public void EnterUnit(int unitID)
        {
            Display.EditUnitScreen(unitID);
        }

        public void OtherOptions(CourseContext CourseContext)
        {
            Display.EditCouursOption(CourseContext);
        }

        public void Register()
        {
            Console.WriteLine("You are registered as a lecturer in this course.");
        }

        public ICourseState Remove()
        {
            Console.WriteLine("Are you sure you want to delete the course? y/n");
            string confirm = Console.ReadLine();
            if (confirm == "y")
            {// send request
                var request = new Request( _course.CourseID, 'D', "Delete course", null, _teacher.TeacherID);
                var requsrID = RequestAccess.AddRequest(request);
                string massege = (requsrID == null)? "Error. try again":"Request send.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = RequestAccess.MakeRequest(request);
                    if (success)
                    {
                        Display.Message("the transaction has completed successfully.");
                        return new CanceledState(_course, _teacher);
                    }
                    else
                    {
                        Display.Message("The request was denied.");
                        return this;
                    }
                }
                else
                {
                    return this;
                }


            }
            else
            {
                Display.Message("The action has been canceled.");
                return this;
            }

        }
    }
}
