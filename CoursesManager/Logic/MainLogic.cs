using CoursesManager.DAL;
using CoursesManager.Logic.CoursState;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic
{
   static class MainLogic // : IMainScreen
    {
        public static void Allcourses()
        {
            var Courses = CoursAccess.VeiwAllCuorses();
            Display.PrintCourses(Courses);
        }

        public static void MyCourses(UserLoggin user)
        {
          var Courses =  CoursAccess.VeiwCuorsesListByUser(user);
            Display.PrintCourses(Courses);
        }

        internal static void OtherCours(UserLoggin user)
        {
            var Courses = CoursAccess.VeiwOtherCuorsesListByUser(user);
            Display.PrintCourses(Courses);
        }


        public static void EnterCours(char user)
        {
            //parameters
            int coursID;
            CoursContext coursContext = null;
            Display.Message("Enter ID of cours.");
            var success = int.TryParse(Console.ReadLine(), out coursID);
            if (!success)
            { Display.Message("Not a number"); return; }
            //cours access
            var cours = CoursAccess.VeiwCours(coursID);
            if (cours == null)
            { Display.Message("Can't find cours"); return; }
            // Filter bag's
            if (cours.CuorsStatus != 'C' && cours.CuorsStatus != 'O')
            { Display.Message("The cours statud undifind."); return; }
            if (user != 's' && user != 't' && user != 'd')
            { Display.Message("The user statud undifind."); return; }
            //Defind cours state
            if (user == 's')
            { if (cours.CuorsStatus == 'C')
                { Display.Message("The cours are closed."); return; }
                else
                { coursContext = new(new StudentState(cours, Program.Student), cours); }
            }
            else
            { if (Program.Teacher.TeacherID == cours.TeacherID)
                {
                    if (cours.CuorsStatus == 'O')
                    { coursContext = new(new ActiveState(cours, Program.Teacher), cours); }
                    else if (cours.CuorsStatus == 'C')
                    { coursContext = new(new CenceledState(cours, Program.Teacher), cours); }
                }
                else if (user == 't')
                { Display.Message("You are not teach this cours. If you want, register as a student."); return; }
                else if (cours.CuorsStatus == 'C')
                { Display.Message("The cours are closed."); return; }
                else
                { coursContext = new(new StudentState(cours, Program.Student), cours); }

            }
            //active cours screen
            if (coursContext == null)
            { Display.Message("Error occor"); return; }
            else
            {
                Display.CoursScreen(coursContext);
            }

        }

        public static void printMyDetiles()
        {
            if (Program.Student != null) {Display.PrintMyDetiles(Program.Student); }
            else
            {
                if (Program.Teacher != null) {Display.PrintMyDetiles(Program.Teacher); }
                else { Display.Message("User undifind. Contact technical support."); }
            }
        }

        public static void UpdateDetiles(char user)
        {
            if (user==default(char))
            { Display.Message("The user type undifinde. Contact technical support."); return; }
            Display.Message("Enter new details or press Enter.");
            Display.Message("First name:");
            var firstName = Console.ReadLine();
            Display.Message("Lest name:");
            var lestName = Console.ReadLine();
            Display.Message("E-mail:");
            var email = Console.ReadLine();
            Display.Message("Phone number:");
            int phone;
            int.TryParse(Console.ReadLine(),out phone);
            switch (user)
            {
                case ('s'):
                    Student student = new() { FirstName = firstName?? default(string), LestName = lestName ?? default(string), email = email ?? default(string), PhonNumber = phone };
                    var success = StudentAccess.UpdateDetiles(Program.Student.StudentID, student);
                    var messege = success ? "The details have been updated" : "Error. rty again";
                    Display.Message(messege);
                                break;
                case ('t'):
                    Teacher teacher = new() { FirstName = firstName ?? default(string), LestName = lestName ?? default(string), email = email ?? default(string), PhonNumber = phone };
                     success = TeacherAccess.UpdateDetiles(Program.Teacher.TeacherID, teacher);
                     messege = success ? "The details have been updated" : "Error. rty again";
                    Display.Message(messege);
                    break;
                case ('d'):
                     student = new() { FirstName = firstName, LestName = lestName, email = email, PhonNumber = phone };
                     success = StudentAccess.UpdateDetiles(Program.Student.StudentID, student);
                     messege = success ? "The student details have been updated" : "Error. rty again";
                    Display.Message(messege);
                     teacher = new() { FirstName = firstName, LestName = lestName, email = email, PhonNumber = phone };
                    success = TeacherAccess.UpdateDetiles(Program.Teacher.TeacherID, teacher);
                    messege = success ? "The teacher details have been updated" : "Error. rty again";
                    Display.Message(messege);
                    break;
                default:
                    Display.Message("The user type undifinde. Contact technical support.");
                    break;
            }
        }


    }
}
