using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    public class ActiveState : ICoursState
    {
        private Cours cours;
        private Teacher teacher;
        public ActiveState(Cours cours, Teacher teacher)
        {
            this.cours = cours;
            this.teacher = teacher;
        }

        public ICoursState Editing()
        {
            if (cours.TeacherID == teacher.TeacherID)
            {
                Console.WriteLine("You move to Edit mode");
                return new EditState(cours, teacher);
            }
            else
            { Console.WriteLine("You do not have permission to edit this course"); return this; }
        }

        public ICoursState Activate()
        {
            Console.WriteLine("This cours are active.");
            return this;
        }
       public ICoursState Remove()
        {
            Console.WriteLine("You can remove cours any in Edit mode");
            return this;
        }
        public void OtherOptions(CoursContext coursContext)
        {
            Console.WriteLine("There are no other options available.");        
        }

        public void Register()
        {
            Console.WriteLine("To regisrer connect as a student.");        
        }

        public void EnterUnit(int unitID)
        {
            Display.UnitScreen(unitID);
        }
    }
}
