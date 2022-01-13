using CoursesManager.DAL;
using CoursesManager.Models;

namespace CoursesManager.Logic
{
    public static class LoginLogic //: ILoginScreen
    {
        public static bool CheckLogin(string userName, string password)
        {
            UserLogin user = new() { UserName = userName, Password = password };
            user = StudentAccess.CheckUser(user);
            if (user == null)
            { return false; }
            else
            { Program.user = user; return true; }
        }
    }
}
