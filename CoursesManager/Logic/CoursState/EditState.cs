using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CourseState
{
    public class EditState : ICourseState
    {
        private Course course;
        private Teacher teacher;
        public EditState(Course course, Teacher teacher)
        {
            this.Course = course;
            this.teacher = teacher;
        }
        public ICourseState Activate()
        {
            Display.Message("Are you sure you want to active the course? y/n");
            string answer = Console.ReadLine();
            if (answer == "y")
            {// send request
                var request = new Request(Course.CourseID, 'A', "activate course", null, teacher.TeacherID);
                var requsrID = requestAccess.AddRequest(request);
                string massege = (requsrID == 0) ? "Error. try again" : "Request was sent.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = requestAccess.MakeRequest(request);
                    if (success)
                    {
                        Console.WriteLine("the transaction has completed successfully.");
                        return new ActiveState(course, teacher);
                    }
                    else
                    {
                        Console.WriteLine("The request was denied.");
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
                Console.WriteLine("The action has been canceled.");
                return this;
            }
        }

        public ICourseState Editing()
        {
            Console.WriteLine("You are in edit mode.");
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
                var request = new Request( Course.CourseID, 'D', "Delete course", null, teacher.TeacherID);
                var requsrID = requestAccess.AddRequest(request);
                string massege = (requsrID == null)? "Error. try again":"Request send.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = requestAccess.MakeRequest(request);
                    if (success)
                    {
                        Console.WriteLine("the transaction has completed successfully.");
                        return new CanceledState(course, teacher);
                    }
                    else
                    {
                        Console.WriteLine("The request was denied.");
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
                Console.WriteLine("The action has been canceled.");
                return this;
            }

        }
    }
}
