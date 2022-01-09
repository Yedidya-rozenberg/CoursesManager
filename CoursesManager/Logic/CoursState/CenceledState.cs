using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CourseState
{
    public class CanceledState : ICourseState
    {
        private Course course;
        private Teacher teacher;
        public CanceledState(Course course, Teacher teacher)
        {
            this.Course = course;
            this.teacher = teacher;
        }
        public ICourseState Editing()
        {
            if (Course.Teacher == teacher)
            {
                Console.WriteLine("You move to Edit mode");
                return new EditState(course, teacher);
            }
            else
            { Console.WriteLine("You do not have permission to edit this course"); return this; }
        }

        public ICourseState Activate()
        {
            Console.WriteLine("this Course canceled. If you want to run it, edit it beforehand. ");
            return this;
        }

        public ICourseState Remove()
        {
            Console.WriteLine("This Course hed been deleted.");
            return this;
        }

        public void EnterUnit(int unitID)
        {
            Console.WriteLine("This Course hed been deleted.");

        }

        public void OtherOptions(CourseContext CourseContext)
        {
            Console.WriteLine("There are no other options available.");
        }

        public void Register()
        {
            Console.WriteLine("This Course hed been deleted.");
        }


    }
}
