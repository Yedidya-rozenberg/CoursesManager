using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic.CoursState
{
    public class StudentState : ICoursState
    {
        private Cours cours;
        private Student Student;
        public StudentState(Cours cours, Student student)
        {
            this.cours = cours;
            this.Student = student;
        }
        public ICoursState Activate()
        {
            Console.WriteLine("You do not have permission to do this.");
            return this;

        }

        public ICoursState Editing()
        {
            Console.WriteLine("You do not have permission to do this.");
            return this;

        }

        public void EnterUnit(int unitID)
        {
            if (CoursAccess.CheckStudentCours(Student.StudentID ,cours.CoursID))
            {
                Display.UnitScreen(unitID);
            }
            else
            { Console.WriteLine("You are not registered for this course."); }
        }

        public void OtherOptions(CoursContext coursContext)
        {
            Console.WriteLine("There are no other options available.");
        }

        public void Register()
        {
            if (!CoursAccess.CheckStudentCours(Student.StudentID, cours.CoursID))
            {

                Console.WriteLine("Do you want to register for the course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(cours.CoursID, 'G', "Register student", Student.StudentID);
                    var requsrID = requastAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request send.";
                    Display.Message(massege);
                    // Execution Request
                    if ((requsrID != 0))
                    {
                        var success = requastAccess.MakeRequest(request);
                        massege = (success) ? "the transaction completed successfully." : "The request was denied.";
                        Display.Message(massege);
                    }
                }
                else
                {
                    Display.Message("The action has been canceled.");
                }
            }
            else
            { Display.Message("You are already enrolled in this course"); }
        }

        public ICoursState Remove()
        {
            if (CoursAccess.CheckStudentCours(Student.StudentID, cours.CoursID))
            {
                Console.WriteLine("Do you want to remove from this course? y/n");
                var answer = Console.ReadLine();
                if (answer == "y")
                {// send request
                    var request = new Request(cours.CoursID, 'M', "Remove student from course", Student.StudentID);
                    var requsrID = requastAccess.AddRequest(request);
                    string massege = (requsrID == null) ? "Error. try again" : "Request send.";
                    Console.WriteLine(massege);
                    // Execution Request
                    if ((requsrID != null))
                    {
                        var success = requastAccess.MakeRequest(request);
                        massege = (success) ? "the transaction completed successfully." : "The request was denied.";
                        Display.Message(massege);
                    }
                }
                else
                {
                    Display.Message("The action has been canceled.");
                }
            }
            else
            { Display.Message("You are not registered for this course"); }
                return this;

        }
    }
}
