using CoursesManager.DAL;
using CoursesManager.Models;

namespace CoursesManager.Logic
{
    public static class LoginLogic //: ILoginScreen
    {
        public static bool CheckLogin(UserLogin user)
        {
            var loggin = UserAccess.CheckUser(user);
            return (loggin != null);
        }
    }
}
