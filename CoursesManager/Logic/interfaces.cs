using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic
{
    public interface IMainScreen
    {
        T MyDetails<T>(userLogin user);
        List<Course> MyCourses(userLogin user);
        List<Course> AllCourses();
        Course viewCourse(int CourseID);
    }
    public interface IUnitScreen
    {
        string GetStudy();
        string GetQuestions();
        void BackUnit();
        void NextUnit();
        void BackToCourse();
        void ConnectAsTeacher();
    }
    public  interface ITeacherUnitScreen : IUnitScreen
    {
        bool UpdateName();
        bool UpdatStudy();
        bool UpdatQuestions();
    }
    public interface ILoginScreen
    {
        bool CheckLogin();//מבקש שם משתמש וסיסמא. ובודק מול הטבלה. אם מצליח  מחזיר חיובי מזיר את המשתמש למשתנה סטטי. אם לא, שלילי.
                           //   bool NewUser();// מבקש פרטים ורושם אותם בבסיס הנתונים
    }
}
