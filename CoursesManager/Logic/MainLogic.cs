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

        public static void MyCourses(UserDetails user)
        {
          var Courses =  CourseAccess.ViewCoursesListByUser(user);
            Display.PrintCourses(Courses);
        }

        internal static void OtherCourse(UserDetails user)
        {
            var Courses = CourseAccess.ViewOtherCuorsesListByUser(user);
            Display.PrintCourses(Courses);
        }


        public static void EnterCourse()
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
            if (Program.User.Type == default(TypeOfUser))
            { Display.Message("The user status undefined."); return; }
            //Defind Course state
            if (Program.User.Type == TypeOfUser.Student)
            { if (course.CourseStatus == 'C')
                { Display.Exception(new AccessViolationException("The Course is closed.")); return; }
                else
                { CourseContext = new(new StudentState(course, Program.User), course); }
            }
            else
            {
                Teacher teacher = TeacherAccess.ViewTeacher(Program.User.UserDetailsID);
                if (teacher != null)
                {
                    if (teacher.TeacherID == course.TeacherID)
                    {
                        if (course.CourseStatus == 'O')
                        { CourseContext = new(new ActiveState(course, teacher), course); }
                        else if (course.CourseStatus == 'C')
                        { CourseContext = new(new CanceledState(course, teacher), course); }
                    }
                    else //if (Program.User.Type == TypeOfUser.Teacher)
                    { Display.Exception(new AccessViolationException("You are not teach this Course. If you want, register as a student.")); return; }
                    //else if (course.CourseStatus == 'C')
                    //{ Display.Exception(new AccessViolationException("The Course are closed.")); return; }
                    //else
                    //{ CourseContext = new(new StudentState(course, Program.User), course); }
                }

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
            if (Program.User.Type == TypeOfUser.Teacher)
            {
                Teacher teacher = TeacherAccess.ViewTeacher(Program.User.UserDetailsID);
                Display.PrintMyDetails<Teacher>(Program.User, teacher);
            }
            else if (Program.User.Type == TypeOfUser.Student)
            {
                Student student = StudentAccess.ViewStudentDetails(Program.User.UserDetailsID);
                Display.PrintMyDetails<Student>(Program.User, student);

            }
            else { Display.Message("User undefined. Contact technical support."); }

        }

        public static void UpdateDetails()
        {
            //if (Program.User.Type==default(TypeOfUser))
            //{ Display.Message("The user type undefined. Contact technical support."); return; }
            UserDetails user = new();
            Display.Message("Enter new details or press Enter.");
            Display.Message("First name:");
            user.FirstName = Console.ReadLine();
            Display.Message("Lest name:");
            user.LastName = Console.ReadLine();
            Display.Message("E-mail:");
            user.Email = Console.ReadLine();
            Display.Message("Phone number:");
            int phone;
            int.TryParse(Console.ReadLine(), out phone);
            user.PhoneNumber = phone;
            UserAccess.UpdateDetails(user);
        }
    }
}

