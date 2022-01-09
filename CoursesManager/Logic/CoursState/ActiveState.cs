using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CourseState
{
    public class ActiveState : ICourseState
    {
        private Course course;
        private Teacher teacher;
        public ActiveState(Course course, Teacher teacher)
        {
            this.Course = course;
            this.teacher = teacher;
        }

        public ICourseState Editing()
        {
            if (Course.TeacherID == teacher.TeacherID)
            {
                Console.WriteLine("You move to Edit mode");
                return new EditState(course, teacher);
            }
            else
            { Console.WriteLine("You do not have permission to edit this course"); return this; }
        }

        public ICourseState Activate()
        {
            Console.WriteLine("This Course is active.");
            return this;
        }
       public ICourseState Remove()
        {
            Console.WriteLine("You can remove any course in Edit mode");
            return this;
        }
        public void OtherOptions(CourseContext CourseContext)
        {
            Console.WriteLine("There are no other available options.");        
        }

        public void Register()
        {
            Console.WriteLine("To register connect as a student.");        
        }

        public void EnterUnit(int unitID)
        {
            Display.UnitScreen(unitID);
        }
    }
}
