using CoursesManager.DAL;
using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesManager
{
    class Program
    {
       public static UserLogin user;
       public static Student Student;
       public static Teacher Teacher;

        public static void Main(string[] args)
        {
            FullDatabase();
            Display.LoginScreen();

        }
        static async void FullDatabase()
        {
            CoursesDBContext _db = GetDB.GetInstance();
            if (await _db.Users.AnyAsync()) { return; }

            List<Student> students = new()
            {
                new() { FirstName = "Avi", LastName = "Av", Email = "aaa@Course.memail", PhoneNumber = 051111111, Payment = 11000, UserLogin = new() { UserName = "a1", Password = "a1A1a1" } },
                new() { FirstName = "Beni", LastName = "Bar", Email = "bbb@Course.memail", PhoneNumber = 052222222, Payment = 12000, UserLogin = new() { UserName = "b2", Password = "b2B2b2" } },
                new() { FirstName = "Gadi", LastName = "Gal", Email = "ccc@Course.memail", PhoneNumber = 053333333, Payment = 13000, UserLogin = new() { UserName = "c3", Password = "c3C3c3" } },
                new() { FirstName = "Dagi", LastName = "Dov", Email = "ddd@Course.memail", PhoneNumber = 054444444, Payment = 14000, UserLogin = new() { UserName = "d4", Password = "d4D4d4" } },
                new() { FirstName = "Henzel", LastName = "Hertz", Email = "eee@Course.memail", PhoneNumber = 055555555, Payment = 15000, UserLogin = new() { UserName = "e5", Password = "e5E5e5" } },
                new() { FirstName = "Vered", LastName = "Vav", Email = "fff@Course.memail", PhoneNumber = 056666666, Payment = 16000, UserLogin = new() { UserName = "f6", Password = "f6F6f6" } },
                new() { FirstName = "Zeev", LastName = "Zits", Email = "ggg@Course.memail", PhoneNumber = 057777777, Payment = 17000, UserLogin = new() { UserName = "g7", Password = "g7G7g7" } },
                new() { FirstName = "Haim", LastName = "Hen", Email = "hhh@Course.memail", PhoneNumber = 058888888, Payment = 18000, UserLogin = new() { UserName = "h8", Password = "h8H8h8" }},
                new() { FirstName = "Tovia", LastName = "Tik", Email = "iii@Course.memail", PhoneNumber = 059999999, Payment = 19000, UserLogin = new() { UserName = "i9", Password = "i9I9i9" } },
                new() { FirstName = "Yosi", LastName = "Yoval", Email = "jjj@Course.memail", PhoneNumber = 050000000, Payment = 20000, UserLogin = new() { UserName = "j10", Password = "j10J10j10" } }
            };
            List<Teacher> teachers = new()
            {
                new() { FirstName = "Ash", LastName = "AV", Email = "ta@Course.memail", PhoneNumber = 0501010101, Salary = 11000, UserLogin = new() { UserName = "aa", Password = "a1A1a1" } },
                new() { FirstName = "Ben", LastName = "Bab", Email = "tb@Course.memail", PhoneNumber = 0520202020, Salary = 12000, UserLogin = new() { UserName = "bb", Password = "b2B2b2" } },
                new() { FirstName = "Gal", LastName = "Gig", Email = "tc@Course.memail", PhoneNumber = 053030303, Salary = 13000, UserLogin = new() { UserName = "cc", Password = "c3C3c3" } },
                new() { FirstName = "Dov", LastName = "Dim", Email = "td@Course.memail", PhoneNumber = 0540004404, Salary = 14000, UserLogin = new() { UserName = "dd", Password = "d4D4d4" } },
                new() { FirstName = "Hod", LastName = "Her", Email = "te@Course.memail", PhoneNumber = 0550505050, Salary = 15000, UserLogin = new() { UserName = "ee", Password = "e5E5e5" } },
                new() { FirstName = "Vivi", LastName = "Vais", Email = "tf@Course.memail", PhoneNumber = 0560606060, Salary = 16000, UserLogin = new() { UserName = "ff", Password = "f6F6f6" } },
                new() { FirstName = "Zuck", LastName = "Zor", Email = "tg@Course.memail", PhoneNumber = 0570500507, Salary = 17000, UserLogin = new() { UserName = "gg", Password = "g7G7g7" } },
                new() { FirstName = "Hai", LastName = "Hovav", Email = "th@Course.memail", PhoneNumber = 0580808080, Salary = 18000, UserLogin = new() { UserName = "hh", Password = "h8H8h8" } },
                new() { FirstName = "Titi", LastName = "Tok", Email = "ti@Course.memail", PhoneNumber = 0590909909, Salary = 19000, UserLogin = new() { UserName = "ii", Password = "i9I9i9" } },
                new() { FirstName = "Yam", LastName = "Yong", Email = "tg@Course.memail", PhoneNumber = 0501234567, Salary = 20000, UserLogin = new() { UserName = "gg", Password = "j10J10j10" } }
            };
            List<Course> Course = new()
            {
                new() { CourseName = "c#", Teacher = teachers[7], CourseStatus = true, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "c++", Teacher = teachers[3], CourseStatus = true, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "SQL", Teacher = teachers[5], CourseStatus = false, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "Python", Teacher = teachers[1], CourseStatus = true, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "Cyber", Teacher = teachers[7], CourseStatus = true, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "Gaming", Teacher = teachers[2], CourseStatus = false, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "VR", Teacher = teachers[7], CourseStatus = true, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CourseName = "UX/UI", Teacher = teachers[4], CourseStatus = false, Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } }
            };
            _db.AddRange(teachers);
            _db.SaveChanges();
            _db.AddRange(students);
            _db.SaveChanges();
            _db.AddRange(Course);
            _db.SaveChanges();
            students = _db.Students.ToList();
            Course = _db.Courses.ToList();
   
            students[3].Course = new List<Course>() { Course[2], Course[4], Course[0] };
            students[5].Course = new List<Course>() { Course[1], Course[3], Course[4] };
            students[7].Course = new List<Course>() { Course[4], Course[6], Course[5] };
            students[9].Course = new List<Course>() { Course[7], Course[0], Course[6] };
            _db.UpdateRange(students);
            _db.SaveChanges();
        }
    }
}
