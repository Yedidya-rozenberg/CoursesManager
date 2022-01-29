using CoursesManager.DAL;
using CoursesManager.Logic;
using CoursesManager.Logic.CourseState;
using CoursesManager.Models;
using System;
using System.Collections.Generic;

namespace CoursesManager
{
    public static class Display
    {
        public static void CoursScreen(CourseContext CourseContext)
        {
            string choice = "";
            while (!(choice == "B"))
            {
                Display.Message($" Whelcome to {CourseContext.Course.CourseName} Course.What do you want to do?");
                Display.Message("T - view teacer Details");
                Display.Message("U - view Course units");
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
                        CourseContext.ViewTeacherDetails();
                        break;
                    case "U":
                        CourseContext.getUnits();
                        break;
                    case "I":
                        int unitID;
                        Display.Message("Enter unit ID");
                        var success = int.TryParse(Console.ReadLine(), out unitID);
                        if (success)
                        { CourseContext.EnterUnit(unitID); }
                        else { Display.Message("Not a number. Try again."); }
                        break;
                    case "R":
                        CourseContext.Register();
                        break;
                    case "E":
                        CourseContext.Editing();
                        break;
                    case "M":
                        CourseContext.Remove();
                        break;
                    case "A":
                        CourseContext.Activate();
                        break;
                    case "O":
                        CourseContext.OtherOptions();
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

        public static void LoginScreen()
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
                        bool Success = LoginLogic.CheckLogin(userName, passwors);
                        var messege = Success ? " The connection was successful" : "faild";
                        Display.Message(messege);
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
                var success = StudentAccess.ViewMyDetails(Program.user, ref Program.Student, ref Program.Teacher);
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
            TypeOfUser user = TypeOfUser.Undifind;
            if (Program.Student != null && Program.Teacher != null) { user = TypeOfUser.TeacherAndStudent; }
            else
            if (Program.Student != null) { user = TypeOfUser.Student; }
            else
            if (Program.Teacher != null) { user = TypeOfUser.Teacher; }
            else { Display.Message("User undefined. please update your Details."); }

            string choice = "";
            while (!(choice == "X"))
            {
                Display.Message("What do you want to do?");
                Display.Message("V - view my Details");
                Display.Message("U - update my Details");
                Display.Message("C - view my courses");
                Display.Message("O - view other courses");
                Display.Message("A - view all courses");
                Display.Message("E - enter to course");
                Display.Message("X - exit");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "V":
                        MainLogic.PrintMyDetails();
                        break;
                    case "U":
                        MainLogic.UpdateDetails(user);
                        break;
                    case "C":
                        MainLogic.MyCourses(Program.user);
                        break;
                    case "O":
                        MainLogic.OtherCourse(Program.user);
                        break;
                    case "A":
                        MainLogic.AllCourses();
                        break;
                    case "E":
                        MainLogic.EnterCourse(user);
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
                Display.Message("B - back to course");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "L":
                        Display.Message(unit.StudyContent);
                        break;
                    case "Q":
                        Display.Message(unit.Questions);
                        break;
                    case "B":
                        Display.Message("You move to Course.");
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
                Display.Message("B - back to course");
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
                        CourseContext.editUnit(unit.UnitID);
                        break;
                    case "B":
                        Display.Message("You move to Course.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }

            }
        }

        public static void EditCouursOption(CourseContext CourseContext)
        {
            string choice = "";
            while (choice != "B")
            {
                Display.Message("Other options");
                Display.Message("What do you want to do?");
                Display.Message("N - edit name course");
                Display.Message("A - add unit");
                Display.Message("R - remove unit");
                Display.Message("B - back");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "N":
                        CourseContext.UpdateName();
                        break;
                    case "A":
                        CourseContext.Addunit();
                        break;
                    case "R":
                        CourseContext.getUnits();
                        CourseContext.RemoveUnit();
                        break;
                    case "B":
                        Display.Message("Return to edit Course.");
                        break;
                    default:
                        Display.Message("Error. try again.");
                        break;
                }

            }
        }
        public static void Exception(Exception exception)
        {
            Console.WriteLine(exception.Message);
        }

        public static void Message(string Message)
        {
            if (Message != null)
            { Console.WriteLine(Message); }
            else
            { Display.Exception(new NullReferenceException()); }
        }

        public static void PrintDetails<T>(T user)
        {
            var student = user as Student;
            if (student != null)
            {
                Console.WriteLine($"Student name: {student.FirstName} {student.LastName}\nPhone number: {student.PhoneNumber}\nE-mail: {student.Email}");
            }
            else
            {
                var teacher = user as Teacher;
                if (teacher != null)
                {
                    Console.WriteLine($"Teacher name: {teacher.FirstName} {teacher.LastName}\nPhone number: {teacher.PhoneNumber}\nE-mail: {teacher.Email}");
                }
                else
                { Display.Exception(new TypeLoadException()); }
            }




        }

        public static void PrintMyDetails<T>(T user)
        {
            var student = user as Student;
            if (student != null)
            {
                Console.WriteLine($"Student name: {student.FirstName} {student.LastName}\nPhone number: {student.PhoneNumber}\nE-mail: {student.Email}\nPayment: {student.Payment}");
            }
            else
            {
                var teacher = user as Teacher;
                if (teacher != null)
                {
                    Console.WriteLine($"Teacher name: {teacher.FirstName} {teacher.LastName}\nPhone number: {teacher.PhoneNumber}\nE-mail: {teacher.Email}\nSelety: {teacher.Salary}");
                }
                else
                { Display.Exception(new TypeLoadException()); }
            }
        }

        public static void PrintCourses(IEnumerable<Course> courses)
        {
            if (courses != null)
            {
                foreach (var item in courses)
                {
                    var status = item.CourseStatus ? "Open" : "Closed";
                    Console.WriteLine($"Course ID: {item.CourseID}\t Course name: {item.CourseName}\t Course status:{status}");
                }
            }
            else
            {
                Display.Exception(new NullReferenceException());
            }
        }
        public static void PrintRequest(IEnumerable<Request> requests)
        {
            if (requests != null)
            {
                foreach (var item in requests)
                {
                    Console.WriteLine($"Course ID: {item.CourseID}\t Request: {item.RequestDetails}\tCode:{item.RequestCode}\tRequest time:{item.RequestTime}");
                }
            }
            else
            {
                Display.Exception(new NullReferenceException());
            }
        }
    }
}
