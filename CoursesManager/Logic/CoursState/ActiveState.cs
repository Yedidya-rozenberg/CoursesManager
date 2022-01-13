using CoursesManager.Models;

namespace CoursesManager.Logic.CourseState
{
    public class ActiveState : ICourseState
    {
        private Course _course;
        private Teacher _teacher;
        public ActiveState(Course course, Teacher teacher)
        {
            this._course = course;
            this._teacher = teacher;
        }

        public ICourseState Editing()
        {
            if (_course.TeacherID == _teacher.TeacherID)
            {
                Display.Message("You move to Edit mode");
                return new EditState(_course, _teacher);
            }
            else
            { Display.Message("You do not have permission to edit this course"); return this; }
        }

        public ICourseState Activate()
        {
            Display.Message("This Course is active.");
            return this;
        }
       public ICourseState Remove()
        {
            Display.Message("You can remove any course in Edit mode");
            return this;
        }
        public void OtherOptions(CourseContext CourseContext)
        {
            Display.Message("There are no other available options.");        
        }

        public void Register()
        {
            Display.Message("To register connect as a student.");        
        }

        public void EnterUnit(int unitID)
        {
            Display.UnitScreen(unitID);
        }
    }
}
