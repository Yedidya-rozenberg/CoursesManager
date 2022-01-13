using CoursesManager.DAL;
using CoursesManager.Logic.CourseState;
using CoursesManager.Models;
using System;

namespace CoursesManager.Logic
{
    static class MainLogic // : IMainScreen
    {
        public static void AllCourses()
        {
            var Courses = CourseAccess.ViewAllCourses();
            Display.PrintCourses(Courses);
        }

        public static void MyCourses(UserLogin user)
        {
          var Courses =  CourseAccess.ViewCoursesListByUser(user);
            Display.PrintCourses(Courses);
        }

        internal static void OtherCourse(UserLogin user)
        {
            var Courses = CourseAccess.ViewOtherCuorsesListByUser(user);
            Display.PrintCourses(Courses);
        }


        public static void EnterCourse(char user)
        {
            //parameters
            int CourseID;
            CourseContext CourseContext = null;
            Display.Message("Enter ID of Course.");
            var success = int.TryParse(Console.ReadLine(), out CourseID);
            if (!success)
            { Display.Exception(new ArgumentException("Not a number")); return; }
            //Course access
            var course = CourseAccess.ViewCourse(CourseID);
            if (course == null)
            { Display.Exception(new NullReferenceException("Can't find course")); return; }
            // Filter bag's
            if (course.CourseStatus != 'C' && course.CourseStatus != 'O')
            { Display.Message("The Course status undefined."); return; }
            if (user != 's' && user != 't' && user != 'd')
            { Display.Message("The user status undefined."); return; }
            //Defind Course state
            if (user == 's')
            { if (course.CourseStatus == 'C')
                { Display.Exception(new AccessViolationException("The Course is closed.")); return; }
                else
                { CourseContext = new(new StudentState(course, Program.Student), course); }
            }
            else
            { if (Program.Teacher.TeacherID == course.TeacherID)
                {
                    if (course.CourseStatus == 'O')
                    { CourseContext = new(new ActiveState(course, Program.Teacher), course); }
                    else if (course.CourseStatus == 'C')
                    { CourseContext = new(new CanceledState(course, Program.Teacher), course); }
                }
                else if (user == 't')
                { Display.Exception(new AccessViolationException("You are not teach this Course. If you want, register as a student.")); return; }
                else if (course.CourseStatus == 'C')
                { Display.Exception(new AccessViolationException("The Course are closed.")); return; }
                else
                { CourseContext = new(new StudentState(course, Program.Student), course); }

            }
            //active Course screen
            if (CourseContext == null)
            { Display.Message("Error occur"); return; }
            else
            {
                Display.CoursScreen(CourseContext);
            }

        }

        public static void PrintMyDetails()
        {
            if (Program.Student != null) {Display.PrintMyDetails(Program.Student); }
            else
            {
                if (Program.Teacher != null) {Display.PrintMyDetails(Program.Teacher); }
                else { Display.Message("User undefined. Contact technical support."); }
            }
        }

        public static void UpdateDetails(char user)
        {
            if (user==default(char))
            { Display.Message("The user type undefined. Contact technical support."); return; }
            Display.Message("Enter new details or press Enter.");
            Display.Message("First name:");
            var firstName = Console.ReadLine();
            Display.Message("Lest name:");
            var lastName = Console.ReadLine();
            Display.Message("E-mail:");
            var email = Console.ReadLine();
            Display.Message("Phone number:");
            int phone;
            int.TryParse(Console.ReadLine(),out phone);
            switch (user)
            {
                case ('s'):
                    Student student = new() { FirstName = firstName?? default(string), LastName = lastName ?? default(string), Email = email ?? default(string), PhoneNumber = phone };
                    var success = StudentAccess.UpdateDetails(Program.Student.StudentID, student);
                    var messege = success ? "The details have been updated" : "Error. try again";
                    Display.Message(messege);
                                break;
                case ('t'):
                    Teacher teacher = new() { FirstName = firstName ?? default(string), LastName = lastName ?? default(string), Email = email ?? default(string), PhoneNumber = phone };
                     success = TeacherAccess.UpdateDetails(Program.Teacher.TeacherID, teacher);
                     messege = success ? "The details have been updated" : "Error. try again";
                    Display.Message(messege);
                    break;
                case ('d'):
                     student = new() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phone };
                     success = StudentAccess.UpdateDetails(Program.Student.StudentID, student);
                     messege = success ? "The student details have been updated" : "Error. try again";
                    Display.Message(messege);
                     teacher = new() { FirstName = firstName, LastName = lastName, Email = email, PhoneNumber = phone };
                    success = TeacherAccess.UpdateDetails(Program.Teacher.TeacherID, teacher);
                    messege = success ? "The teacher details have been updated" : "Error. try again";
                    Display.Message(messege);
                    break;
                default:
                    Display.Message("The user type undefined. Contact technical support.");
                    break;
            }
        }


    }
}
