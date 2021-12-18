using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.Logic
{
   public static class LogginLogic //: ILogginScreen
    {
        public static bool ChackLoggin(string userName, string Password)
        {
            UserLoggin user = new() { UserName = userName, Password = Password };
            user = StudentAccess.ChackUser(user);
            if (user == null)
            { return false; }
            else
            { Program.user = user; return true; }
        }
    }
}
