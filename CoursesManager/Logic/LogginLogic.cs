using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic
{
   public static class LoginLogic //: ILoginScreen
    {
        public static bool CheckLogin(string userName, string Password)
        {
            userLogin user = new() { UserName = userName, Password = Password };
            user = StudentAccess.CheckUser(user);
            if (user == null)
            { return false; }
            else
            { Program.user = user; return true; }
        }
    }
}
