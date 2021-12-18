using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    public class EditState : ICoursState
    {
        private Cours cours;
        private Teacher teacher;
        public EditState(Cours cours, Teacher teacher)
        {
            this.cours = cours;
            this.teacher = teacher;
        }
        public ICoursState Activate()
        {
            Display.Message("Are you sure you want to active the course? y/n");
            string answer = Console.ReadLine();
            if (answer == "y")
            {// send request
                var request = new Request(cours.CoursID, 'A', "activat cours", null, teacher.TeacherID);
                var requsrID = requastAccess.AddRequest(request);
                string massege = (requsrID == 0) ? "Error. try again" : "Request send.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = requastAccess.MakeRequest(request);
                    if (success)
                    {
                        Console.WriteLine("the transaction completed successfully.");
                        return new ActiveState(cours, teacher);
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

        public ICoursState Editing()
        {
            Console.WriteLine("You are in edit mode.");
            return this;
        }

        public void EnterUnit(int unitID)
        {
            Display.EditUnitScreen(unitID);
        }

        public void OtherOptions(CoursContext coursContext)
        {
            Display.EditCouursOption(coursContext);
        }

        public void Register()
        {
            Console.WriteLine("You are registered as a lecturer in this course.");
        }

        public ICoursState Remove()
        {
            Console.WriteLine("Are you sure you want to delete the course? y/n");
            string confirm = Console.ReadLine();
            if (confirm == "y")
            {// send request
                var request = new Request( cours.CoursID, 'D', "Delete cours", null, teacher.TeacherID);
                var requsrID = requastAccess.AddRequest(request);
                string massege = (requsrID == null)? "Error. try again":"Request send.";
                Display.Message(massege);
                // Execution Request
                if ((requsrID != null))
                {

                    var success = requastAccess.MakeRequest(request);
                    if (success)
                    {
                        Console.WriteLine("the transaction completed successfully.");
                        return new CenceledState(cours, teacher);
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
