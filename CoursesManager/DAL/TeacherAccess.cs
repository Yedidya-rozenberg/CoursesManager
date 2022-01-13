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

        public static bool ViewDetails(int ID, ref Teacher t)
        {
            Teacher teacher = new();
            try
            {
                _rw.EnterReadLock();
                teacher = _dbcontext.Teachers.FirstOrDefault(s => s.UserLogin.UserID == ID);
                _rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            if ((teacher == null))
            {
                return false;
            }
            teacher.Salary = default(float);
            teacher.UserLogin = null;
            t = teacher;
            return true;
        }

        public static bool UpdateDetails(int TeacherID, Teacher updated)
        {
            Teacher teacher = new();
            try
            {
                _rw.EnterReadLock();
                teacher = _dbcontext.Teachers.Find(TeacherID);
                _rw.ExitReadLock();

            teacher.FirstName = (updated.FirstName != "") ? updated.FirstName : teacher.FirstName;
            teacher.LastName = (updated.LastName != "") ? updated.LastName : teacher.LastName;
            teacher.Email = (updated.Email != "") ? updated.Email : teacher.Email;
            teacher.Salary = (updated.Salary != default(float)) ? updated.Salary : teacher.Salary;
            teacher.PhoneNumber = (updated.PhoneNumber != default(int)) ? updated.PhoneNumber : teacher.PhoneNumber;
     
                _rw.EnterWriteLock();
                _dbcontext.Update(teacher);
                _dbcontext.SaveChanges();
                _rw.ExitWriteLock();
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
