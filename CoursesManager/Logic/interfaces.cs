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
        T MyDetiles<T>(UserLoggin user);
        List<Cours> MyCourses(UserLoggin user);
        List<Cours> Allcourses();
        Cours viewCours(int coursID);
    }
    public interface IUnitScreen
    {
        string GetStudy();
        string GetQuestions();
        void BackUnit();
        void NextUnit();
        void BackToCours();
        void ConnectAsTeacher();
    }
    public  interface ITeacherUnitScreen : IUnitScreen
    {
        bool UpdateName();
        bool UpdatStudy();
        bool UpdatQuestions();
    }
    public interface ILogginScreen
    {
        bool ChackLoggin();//מבקש שם משתמש וסיסמא. ובודק מול הטבלה. אם מצליח  מחזיר חיובי מזיר את המשתמש למשתנה סטטי. אם לא, שלילי.
                           //   bool NewUser();// מבקש פרטים ורושם אותם בבסיס הנתונים
    }
}
