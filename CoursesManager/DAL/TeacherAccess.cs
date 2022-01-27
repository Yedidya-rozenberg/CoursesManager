using CoursesManager.Models;
using System;
using System.Linq;
using System.Threading;

namespace CoursesManager.DAL
{
    public class TeacherAccess
    {
        private static CoursesDBContext _dbcontext = GetDB.GetInstance();
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();

        public static bool ViewDetails(int ID, ref UserDetails teacher)
        {
            try
            {
                var teach = _dbcontext.Teachers.FirstOrDefault(t => t.TeacherID == ID);
                teacher = _dbcontext.Users.Find(teach.UserDetailsID);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            if ((teacher == null))
            {
                return false;
            }
            return true;
        }
        public static Teacher ViewTeacher(int UserDetilesID)
        {
            try
            {
               var teacher =  _dbcontext.Teachers.FirstOrDefault(t => t.UserDetailsID == UserDetilesID);
                return teacher;
            }
            catch (Exception ex)
            {

                Display.Exception(ex);
                return null;
            }
        }


        public static bool UpdateDetails(int TeacherID, Teacher updated)
        {
            Teacher teacher = new();
            try
            {
                teacher = _dbcontext.Teachers.Find(TeacherID);

            teacher.FirstName = (updated.FirstName != "") ? updated.FirstName : teacher.FirstName;
            teacher.LastName = (updated.LastName != "") ? updated.LastName : teacher.LastName;
            teacher.Email = (updated.Email != "") ? updated.Email : teacher.Email;
            teacher.Salary = (updated.Salary != default(float)) ? updated.Salary : teacher.Salary;
            teacher.PhoneNumber = (updated.PhoneNumber != default(int)) ? updated.PhoneNumber : teacher.PhoneNumber;
     
                _dbcontext.Update(teacher);
                _dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }

        }


    }
}
