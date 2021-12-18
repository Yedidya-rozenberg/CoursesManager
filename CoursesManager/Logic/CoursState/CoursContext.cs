using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    public interface ICoursState
    {
        ICoursState Activate();
        ICoursState Editing();
        ICoursState Remove();
        void EnterUnit(int unitID);
        void Register();
        void OtherOptions(CoursContext coursContext);
    }

    public class CoursContext
    {
        private ICoursState State;
        public Cours cours;
        public CoursContext(ICoursState state, Cours cours)
        {
            this.State = state;
            this.cours = cours;
        }

        public void Activate()
        {
            this.State = State.Activate();
        }
        public void Editing()
        {
            this.State = State.Editing();
        }
       public void Remove()
        {
            this.State = State.Remove();
        }
        public void EnterUnit(int unitID)
        {
            if (cours.Units.FirstOrDefault(u=>u.UnitID==unitID)!=null)
                { State.EnterUnit(unitID);}
            else
            { Display.Message("The course does not include this unit."); }
        }

        public void OtherOptions()
        {
            State.OtherOptions(this);
        }

        public void Register()
        {
            State.Register();
        }

        public void getUnits()
        {
            foreach (var unit in cours.Units)
            {
                Display.Message($"Unit ID: {unit.UnitID} : {unit.UnitName}");
            }        
        }
        public void ViewTeacherDetiles()
        {
            Teacher teacher = new();
            TeacherAccess.ViewDediles(cours.TeacherID,ref teacher);
            Display.PrintDetiles(teacher);        
        }

        internal void UpdateName()
        {
            Display.Message("Enter new name:");
            var name = Console.ReadLine();
            var success = CoursAccess.UpdateCoursName(cours.CoursID, name);
            var messege = success ? "The name update succeed." : "Error. try agaig";
            Display.Message(messege);
        }

        internal void Addunit()
        {
            Display.Message("Enter Unit name:");
            var name = Console.ReadLine();
            Display.Message("Enter learn material:");
            var material = Console.ReadLine();
            Display.Message("Enter questions:");
            var Questions = Console.ReadLine();
            Unit unit = new() { UnitName = name, StudyContent = material, Questions = Questions };
            var success = UnitAccess.AddUnit(unit, cours.CoursID);
            var messege = success ? "Success added." : "Error. try agaig";
            Display.Message(messege);
        }

        internal void RemoveUnit()
        {
            Display.Message("Enter Unit ID:");
            int unitID;
            var success =int.TryParse(Console.ReadLine(),out unitID);
            if (success)
            { UnitAccess.RemoveUnit(unitID, Program.Teacher.TeacherID, cours); }
            else
            { Display.Message("Not a number. try again."); }
        }

        internal static void editUnit(int unitID)
        {
            Display.Message("Enter new detiles or press enter");
            Display.Message("Enter new name:");
            var name = Console.ReadLine();
            Display.Message("Enter new study material:");
            var study = Console.ReadLine();
            Display.Message("Enter new questions:");
            var questions = Console.ReadLine();
            Unit unit = new() { UnitName = name, StudyContent = study, Questions = questions };
            var success = UnitAccess.UpdateUnit(unitID, unit);
            var messege = (success) ? "The unit update successfuly." : "The update filed.";
            Display.Message(messege);
        }
    }
}
