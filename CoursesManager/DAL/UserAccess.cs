using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.DAL
{
    public static class UserAccess // : Iuser
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
       public static UserLogin CheckUser(UserLogin user)
        {
            try
            {
                UserLogin userLogin = _dbContext.UserLogins.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                return userLogin;
            
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }

        public static bool UpdateDetails(UserDetails updated)
        {
            UserDetails user = new();
            try
            {
                user = _dbContext.Users.FirstOrDefault(u => u == Program.User);

                user.FirstName = (updated.FirstName != "") ? updated.FirstName : user.FirstName;
                user.LastName = (updated.LastName != "") ? updated.LastName : user.LastName;
                user.Email = (updated.Email != "") ? updated.Email : user.Email;
                user.PhoneNumber = (updated.PhoneNumber != default(int)) ? updated.PhoneNumber : user.PhoneNumber;

                _dbContext.Update(user);
                Program.User = user;
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }
        }

  

        //public static T ViewDetails<T>(int ID)
        //{
        //    throw new NotImplementedException();
        //}

        //public static T ViewMyDetails<T>(int ID)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
