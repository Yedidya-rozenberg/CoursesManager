using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Linq;

namespace CoursesManager.Logic.CourseState
{
    public interface ICourseState
    {
        ICourseState Activate();
        ICourseState Editing();
        ICourseState Remove();
        void EnterUnit(int unitID);
        void Register();
        void OtherOptions(CourseContext CourseContext);
    }

    public class CourseContext
    {
        private ICourseState _state;
        public Course Course;
        public CourseContext(ICourseState state, Course course)
        {
            this._state = state;
            this.Course = course;
        }

        public void Activate()
        {
            this._state = _state.Activate();
        }
        public void Editing()
        {
            this._state = _state.Editing();
        }
       public void Remove()
        {
            this._state = _state.Remove();
        }
        public void EnterUnit(int unitID)
        {
            if (Course.Units.FirstOrDefault(u=>u.UnitID==unitID)!=null)
                { _state.EnterUnit(unitID);}
            else
            { Display.Message("The course does not include this unit."); }
        }

        public void OtherOptions()
        {
            _state.OtherOptions(this);
        }

        public void Register()
        {
            _state.Register();
        }

        public void getUnits()
        {
            foreach (var unit in Course.Units)
            {
                Display.Message($"Unit ID: {unit.UnitID} : {unit.UnitName}");
            }        
        }
        public void ViewTeacherDetails()
        {
            UserDetails teacher = new();
            TeacherAccess.ViewDetails(Course.TeacherID,ref teacher);
            Display.PrintDetails(teacher);        
        }

        internal void UpdateName()
        {
            Display.Message("Enter new name:");
            var name = Console.ReadLine();
            var success = CourseAccess.UpdateCourseName(Course.CourseID, name);
            var messege = success ? "The name update succeed." : "Error. try again";
            Display.Message(messege);
        }

        internal void Addunit()
        {
            Display.Message("Enter Unit name:");
            var name = Console.ReadLine();
            Display.Message("Enter learn material:");
            var material = Console.ReadLine();
            Display.Message("Enter questions:");
            var questions = Console.ReadLine();
            Unit unit = new() { UnitName = name, StudyContent = material, Questions = questions };
            var success = UnitAccess.AddUnit(unit, Course.CourseID);
            var messege = success ? "Successfully added." : "Error. try again";
            Display.Message(messege);
        }

        internal void RemoveUnit()
        {
            Display.Message("Enter Unit ID:");
            int unitID;
            var success =int.TryParse(Console.ReadLine(),out unitID);
            if (success)
            { UnitAccess.RemoveUnit(unitID, Course, Program.User); }
            else
            { Display.Message("Not a number. try again."); }
        }

        internal static void editUnit(int unitID)
        {
            Display.Message("Enter new details or press enter");
            Display.Message("Enter new name:");
            var name = Console.ReadLine();
            Display.Message("Enter new study material:");
            var study = Console.ReadLine();
            Display.Message("Enter new questions:");
            var questions = Console.ReadLine();
            Unit unit = new() { UnitName = name, StudyContent = study, Questions = questions };
            var success = UnitAccess.UpdateUnit(unitID, unit);
            var messege = (success) ? "The unit was updated successfully." : "The update failed.";
            Display.Message(messege);
        }
    }
}
