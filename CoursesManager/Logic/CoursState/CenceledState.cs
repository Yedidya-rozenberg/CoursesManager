using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    public class CenceledState : ICoursState
    {
        private Cours cours;
        private Teacher teacher;
        public CenceledState(Cours cours, Teacher teacher)
        {
            this.cours = cours;
            this.teacher = teacher;
        }
        public ICoursState Editing()
        {
            if (cours.Teacher == teacher)
            {
                Console.WriteLine("You move to Edit mode");
                return new EditState(cours, teacher);
            }
            else
            { Console.WriteLine("You do not have permission to edit this course"); return this; }
        }

        public ICoursState Activate()
        {
            Console.WriteLine("this cours cenceled. If you want to run it, edit it beforehand. ");
            return this;
        }

        public ICoursState Remove()
        {
            Console.WriteLine("This cours hed been deleted.");
            return this;
        }

        public void EnterUnit(int unitID)
        {
            Console.WriteLine("This cours hed been deleted.");

        }

        public void OtherOptions(CoursContext coursContext)
        {
            Console.WriteLine("There are no other options available.");
        }

        public void Register()
        {
            Console.WriteLine("This cours hed been deleted.");
        }


    }
}
