using CoursesManager.DAL;
using CoursesManager.Logic.CoursState;
using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoursesManager
{
    class Program
    {
        public static UserLoggin user;
        public static Student Student;
       public static Teacher Teacher;
       public static Cours Cours;

       public static void Main(string[] args)
        {
           // var _dbContext = GetDB.GetInstance();
            Display.LogginScreen();

        }
        static void FuLLDatabase()
        {
            CoursesDBcontext _db = GetDB.GetInstance();
            List<Student> students = new()
            {
                new() { FirstName = "Avi", LestName = "Av", email = "aaa@cours.memail", PhonNumber = 051111111, Payment = 11000, userLoggin = new() { UserName = "a1", Password = "a1a1a1" } },
                new() { FirstName = "Beni", LestName = "Bar", email = "bbb@cours.memail", PhonNumber = 052222222, Payment = 12000, userLoggin = new() { UserName = "b2", Password = "b2b2b2" } },
                new() { FirstName = "Gadi", LestName = "Gal", email = "ccc@cours.memail", PhonNumber = 053333333, Payment = 13000, userLoggin = new() { UserName = "c3", Password = "c3c3c3" } },
                new() { FirstName = "Dagi", LestName = "Dov", email = "ddd@cours.memail", PhonNumber = 054444444, Payment = 14000, userLoggin = new() { UserName = "d4", Password = "d4d4d4" } },
                new() { FirstName = "Henzel", LestName = "Hertz", email = "eee@cours.memail", PhonNumber = 055555555, Payment = 15000, userLoggin = new() { UserName = "e5", Password = "e5e5e5" } },
                new() { FirstName = "Vered", LestName = "Vav", email = "fff@cours.memail", PhonNumber = 056666666, Payment = 16000, userLoggin = new() { UserName = "f6", Password = "f6f6f6" } },
                new() { FirstName = "Zeev", LestName = "Zits", email = "ggg@cours.memail", PhonNumber = 057777777, Payment = 17000, userLoggin = new() { UserName = "g7", Password = "g7g7g7" } },
                new() { FirstName = "Haim", LestName = "Hen", email = "hhh@cours.memail", PhonNumber = 058888888, Payment = 18000, userLoggin = new() { UserName = "h8", Password = "h8h8h8" }},
                new() { FirstName = "Tovia", LestName = "Tik", email = "iii@cours.memail", PhonNumber = 059999999, Payment = 19000, userLoggin = new() { UserName = "i9", Password = "i9i9i9" } },
                new() { FirstName = "Yosi", LestName = "Yoval", email = "jjj@cours.memail", PhonNumber = 050000000, Payment = 20000, userLoggin = new() { UserName = "j10", Password = "j10j10j10" } }
            };
            List<Teacher> teachers = new()
            {
                new() { FirstName = "Ash", LestName = "AV", email = "ta@cours.memail", PhonNumber = 0501010101, Selery = 11000, userLoggin = new() { UserName = "aa", Password = "a1a1a1" } },
                new() { FirstName = "Ben", LestName = "Bab", email = "tb@cours.memail", PhonNumber = 0520202020, Selery = 12000, userLoggin = new() { UserName = "bb", Password = "b2b2b2" } },
                new() { FirstName = "Gal", LestName = "Gig", email = "tc@cours.memail", PhonNumber = 053030303, Selery = 13000, userLoggin = new() { UserName = "cc", Password = "c3c3c3" } },
                new() { FirstName = "Dov", LestName = "Dim", email = "td@cours.memail", PhonNumber = 0540004404, Selery = 14000, userLoggin = new() { UserName = "dd", Password = "d4d4d4" } },
                new() { FirstName = "Hod", LestName = "Her", email = "te@cours.memail", PhonNumber = 0550505050, Selery = 15000, userLoggin = new() { UserName = "ee", Password = "e5e5e5" } },
                new() { FirstName = "Vivi", LestName = "Vais", email = "tf@cours.memail", PhonNumber = 0560606060, Selery = 16000, userLoggin = new() { UserName = "ff", Password = "f6f6f6" } },
                new() { FirstName = "Zuck", LestName = "Zor", email = "tg@cours.memail", PhonNumber = 0570500507, Selery = 17000, userLoggin = new() { UserName = "gg", Password = "g7g7g7" } },
                new() { FirstName = "Hai", LestName = "Hovav", email = "th@cours.memail", PhonNumber = 0580808080, Selery = 18000, userLoggin = new() { UserName = "hh", Password = "h8h8h8" } },
                new() { FirstName = "Titi", LestName = "Tok", email = "ti@cours.memail", PhonNumber = 0590909909, Selery = 19000, userLoggin = new() { UserName = "ii", Password = "i9i9i9" } },
                new() { FirstName = "Yam", LestName = "Yong", email = "tg@cours.memail", PhonNumber = 0501234567, Selery = 20000, userLoggin = new() { UserName = "gg", Password = "j10j10j10" } }
            };
            List<Cours> cours = new()
            {
                new() { CoursName = "c#", Teacher = teachers[7], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "c++", Teacher = teachers[3], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "SQL", Teacher = teachers[5], CuorsStatus = 'C', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "Payton", Teacher = teachers[1], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "Syber", Teacher = teachers[7], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "Gaming", Teacher = teachers[2], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "VR", Teacher = teachers[7], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } },
                new() { CoursName = "UX/UI", Teacher = teachers[4], CuorsStatus = 'O', Units = new List<Unit>() { new() { UnitName = "Basic" }, new() { UnitName = "Middle" }, new() { UnitName = "advanced" } } }
            };
            _db.AddRange(teachers);
            _db.SaveChanges();
            _db.AddRange(students);
            _db.SaveChanges();
            _db.AddRange(cours);
            _db.SaveChanges();
            students = _db.Students.ToList();
            cours = _db.Courses.ToList();
            //List<StudentCours> studentCourses = new()
            //{
            //    new() { Cours = cours[7], Student = students[1] },
            //    new() { Cours = cours[6], Student = students[2] },
            //    new() { Cours = cours[5], Student = students[3] },
            //    new() { Cours = cours[4], Student = students[4] },
            //    new() { Cours = cours[3], Student = students[5] },
            //    new() { Cours = cours[3], Student = students[6] },
            //    new() { Cours = cours[2], Student = students[7] },
            //    new() { Cours = cours[1], Student = students[8] },
            //    new() { Cours = cours[0], Student = students[9] },
            //    new() { Cours = cours[7], Student = students[0] },
            //    new() { Cours = cours[6], Student = students[1] },
            //    new() { Cours = cours[5], Student = students[2] },
            //    new() { Cours = cours[4], Student = students[3] },
            //    new() { Cours = cours[3], Student = students[4] },
            //    new() { Cours = cours[2], Student = students[5] },
            //    new() { Cours = cours[1], Student = students[6] },
            //    new() { Cours = cours[0], Student = students[7] },
            //    new() { Cours = cours[7], Student = students[8] },
            //    new() { Cours = cours[6], Student = students[9] },
            //    new() { Cours = cours[5], Student = students[0] },
            //    new() { Cours = cours[4], Student = students[1] },
            //    new() { Cours = cours[3], Student = students[2] },
            //    new() { Cours = cours[2], Student = students[3] },
            //};
            students[3].Cours = new List<Cours>() { cours[2], cours[4], cours[0] };
            students[5].Cours = new List<Cours>() { cours[1], cours[3], cours[4] };
            students[7].Cours = new List<Cours>() { cours[4], cours[6], cours[5] };
            students[9].Cours = new List<Cours>() { cours[7], cours[0], cours[6] };
            _db.UpdateRange(students);
            _db.SaveChanges();
        }
    }
}
