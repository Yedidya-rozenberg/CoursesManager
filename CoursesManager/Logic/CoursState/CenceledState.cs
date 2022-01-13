using CoursesManager.Models;
using System;

namespace CoursesManager.Logic.CourseState
{
    public class CanceledState : ICourseState
    {
        private Course _course;
        private Teacher _teacher;
        public CanceledState(Course course, Teacher teacher)
        {
            this._course = course;
            this._teacher = teacher;
        }
        public ICourseState Editing()
        {
            if (_course.Teacher == _teacher)
            {
                Display.Message("You move to Edit mode");
                return new EditState(_course, _teacher);
            }
            else
            { Console.WriteLine("You do not have permission to edit this course"); return this; }
        }

        public ICourseState Activate()
        {
            Display.Message("this Course canceled. If you want to run it, edit it beforehand. ");
            return this;
        }

        public ICourseState Remove()
        {
            Display.Message("This Course hed been deleted.");
            return this;
        }

        public void EnterUnit(int unitID)
        {
            Display.Message("This Course hed been deleted.");

        }

        public void OtherOptions(CourseContext CourseContext)
        {
            Display.Message("There are no other options available.");
        }

        public void Register()
        {
            Display.Message("This Course hed been deleted.");
        }


    }
}
