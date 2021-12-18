using CoursesManager.DAL;
using CoursesManager.Logic;
using CoursesManager.Logic.CoursState;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager
{
    public static class Display
    {
        public static void CoursScreen(CoursContext coursContext)
        {
            string choice = "";
            while (!(choice == "B"))
            {
                Display.Message($" Whelcome to {coursContext.cours.CoursName} cours.What do you want to do?");
                Display.Message("T - view teacer detiles");
                Display.Message("U - view cours units");
                Display.Message("I - enter to unit");
                Display.Message("R - register");
                Display.Message("E - edit");
                Display.Message("M - remove");
                Display.Message("A - active");
                Display.Message("O - other options");
                Display.Message("B - back");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "T":
                        coursContext.ViewTeacherDetiles();
                        break;
                    case "U":
                        coursContext.getUnits();
                        break;
                    case "I":
                        int unitID;
                        Display.Message("Enter unit ID");
                        var success = int.TryParse(Console.ReadLine(), out unitID);
                        if (success)
                        { coursContext.EnterUnit(unitID); }
                        else { Display.Message("Not a number.Try again."); }
                        break;
                    case "R":
                        coursContext.Register();
                        break;
                    case "E":
                        coursContext.Editing();
                        break;
                    case "M":
                        coursContext.Remove();
                        break;
                    case "A":
                        coursContext.Activate();
                        break;
                    case "O":
                        coursContext.OtherOptions();
                        break;
                    case "B":
                        Display.Message("You remove to main screen");
                        break;
                    default:
                        Display.Message("Error. Try again.");
                        break;
                }
            }
        }

        public static void LogginScreen()
        {
            string choice = "";
            bool enter = false;
            while (!(choice == "X" || enter == true))
            {
                Display.Message("What do you want to do?");
                Display.Message("E - enter");
                Display.Message("R - register");
                Display.Message("X - exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "E":
                        Display.Message("Enter user name");
                        var userName = Console.ReadLine();
                        Display.Message("Enter Password");
                        var passwors = Console.ReadLine();
                        bool Success = LogginLogic.ChackLoggin(userName, passwors);
                        var messege = Success ? " The connection was successful" : "faild";
                        enter = Success;
                        break;
                    case "R":
                        Display.Message("This option is currently unavailable.");
                        break;
                    case "X":
                        Display.Message("The program closed.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }
            }
            if (enter == true)
            {
                var success = StudentAccess.ViewMyDediles(Program.user, ref Program.Student, ref Program.Teacher);
                if (success)
                {
                  var name = (Program.Student != null) ? Program.Student.FirstName : Program.Teacher.FirstName;
                Display.Message($"Welcome {name}!");
                Display.MainScreen();
                }
                else
                { Display.Message("Please contact us to complete the registration."); }
         
            }


        }
        public static void MainScreen()
        {
            char user = default(char);
            if (Program.Student != null && Program.Teacher != null) { user = 'd'; }
            else

            if (Program.Student != null) { user = 's'; }
            else
            if (Program.Teacher != null) { user = 't'; }
            else { Display.Message("User undifind. please update youre detiles."); }

            string choice = "";
            while (!(choice == "X"))
            {
                Display.Message("What do you want to do?");
                Display.Message("V - view my detiles");
                Display.Message("U - update my detiles");
                Display.Message("C - view my courses");
                Display.Message("O - view other courses");
                Display.Message("A - view all courses");
                Display.Message("E - enter to course");
                Display.Message("X - exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "V":
                        MainLogic.printMyDetiles();
                        break;
                    case "U":
                        MainLogic.UpdateDetiles(user);
                        break;
                    case "C":
                        MainLogic.MyCourses(Program.user);
                        break;
                    case "O":
                        MainLogic.OtherCours(Program.user);
                        break;
                    case "A":
                        MainLogic.Allcourses();
                        break;
                    case "E":
                        MainLogic.EnterCours(user);
                        break;
                    case "X":
                        Display.Message("The program closed.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }
            }

        }
        public static void UnitScreen(int UnitID)
        {
            Unit unit = UnitAccess.ViewUnit(UnitID);
            string choice = "";
            while (choice != "B")
            {
                Display.Message($"Whelcome to {unit.UnitName} unit");
                Display.Message("What do you want to do?");
                Display.Message("L - learn");
                Display.Message("Q - Questions");
                //Display.Message("N - next unit");
                //Display.Message("P - previous unit");
                Display.Message("B - back to cours");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "L":
                        Display.Message(unit.StudyContent);
                        break;
                    case "Q":
                        Display.Message(unit.Questions);
                        break;
                    //case "N":
                    //    UnitLogic.NextUnit();
                    //    break;
                    //case "P":
                    //    UnitLogic.BackUnit();
                    //break;
                    case "B":
                        Display.Message("You move to cours.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }

            }
        }
        public static void EditUnitScreen(int UnitID)
        {
            Unit unit = UnitAccess.ViewUnit(UnitID);
            string choice = "";
            while (choice != "B")
            {
                Display.Message($"Whelcome to {unit.UnitName} unit");
                Display.Message("What do you want to do?");
                Display.Message("L - learn");
                Display.Message("Q - Questions");
                Display.Message("E - edit unit");
                Display.Message("B - back to cours");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "L":
                        Display.Message(unit.StudyContent);
                        break;
                    case "Q":
                        Display.Message(unit.Questions);
                        break;
                    case "E":
                        CoursContext.editUnit(unit.UnitID);
                        break;
                    case "B":
                        Display.Message("You move to cours.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }

            }
        }

        public static void EditCouursOption(CoursContext coursContext)
        {
            string choice = "";
            while (choice != "B")
            {
                Display.Message("Other options");
                Display.Message("What do you want to do?");
                Display.Message("N - edit name cours");
                Display.Message("A - add unit");
                Display.Message("R - remove unit");
                Display.Message("B - back");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "N":
                        coursContext.UpdateName();
                        break;
                    case "A":
                        coursContext.Addunit();
                        break;
                    case "R":
                        coursContext.getUnits();
                        coursContext.RemoveUnit();
                        break;
                    case "B":
                        Display.Message("Return to edit cours.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }

            }
        }
        public static void Exeption(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }

        public static void Message(string Message)
        {
            if (Message != null)
            { Console.WriteLine(Message); }
            else
            { Display.Exeption(new NullReferenceException()); }
        }

        public static void PrintDetiles<T>(T user)
        {
            var student = user as Student;
            if (student != null)
            {
                Console.WriteLine($"Student name: {student.FirstName} {student.LestName}\nPhone number: {student.PhonNumber}\nE-mail: {student.email}");
            }
            else
            {
                var teacher = user as Teacher;
                if (teacher != null)
                {
                    Console.WriteLine($"Teacher name: {teacher.FirstName} {teacher.LestName}\nPhone number: {teacher.PhonNumber}\nE-mail: {teacher.email}");
                }
                else
                { Display.Exeption(new TypeLoadException()); }
            }




        }

        public static void PrintMyDetiles<T>(T user)
        {
            var student = user as Student;
            if (student != null)
            {
                Console.WriteLine($"Student name: {student.FirstName} {student.LestName}\nPhone number: {student.PhonNumber}\nE-mail: {student.email}\nPayment: {student.Payment}");
            }
            else
            {
                var teacher = user as Teacher;
                if (teacher != null)
                {
                    Console.WriteLine($"Teacher name: {teacher.FirstName} {teacher.LestName}\nPhone number: {teacher.PhonNumber}\nE-mail: {teacher.email}\nSelety: {teacher.Selery}");
                }
                else
                { Display.Exeption(new TypeLoadException()); }
            }
        }

        public static void PrintCourses(IEnumerable<Cours> courses)
        {
            if (courses != null)
            {
                foreach (var item in courses)
                {
                    Console.WriteLine($"Cours ID: {item.CoursID}\t Cours name: {item.CoursName}\t Cours status:{item.CuorsStatus}");
                }
            }
            else
            {
                Display.Exeption(new NullReferenceException());
            }
        }
        public static void PrintRequest(IEnumerable<Request> requests)
        {
            if (requests != null)
            {
                foreach (var item in requests)
                {
                    Console.WriteLine($"Cours ID: {item.CoursID}\t Request: {item.RequestDetiles}\tCode:{item.RequestCode}\tRequest time:{item.RequestTime}");
                }
            }
            else
            {
                Display.Exeption(new NullReferenceException());
            }
        }
    }
}
