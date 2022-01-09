using CoursesManager.DAL;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesManager.DAL
{
    public class TeacherAccess
    {
        private static CoursesDBcontext _dbcontext = GetDB.GetInstance();
        static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();

        public static bool ViewDetails(int ID, ref Teacher t)
        {
            Teacher teacher = new();
            try
            {
                rw.EnterReadLock();
                teacher = _dbcontext.Teachers.FirstOrDefault(s => s.userLogin.UserID == ID);
                rw.ExitReadLock();
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
            teacher.userLogin = null;
            t = teacher;
            return true;
        }

        //public static bool ViewMyTeacherDetails(int TeacherID, ref Teacher teacher)
        //{
        //    Teacher T = new();
        //    try
        //    {
        //        rw.EnterReadLock();
        //        T = _dbcontext.Teachers.FirstOrDefault(s => s.TeacherID == TeacherID);
        //        rw.ExitReadLock();
        //    }
        //    catch (Exception ex)
        //    {
        //        Display.Exception(ex);
        //    }
        //    if ((T == null))
        //    {
        //        return false;
        //    }
        //    teacher = T;
        //    return true;
        //}

        public static bool UpdateDetails(int TeacherID, Teacher updated)
        {
            Teacher teacher = new();
            try
            {
                rw.EnterReadLock();
                teacher = _dbcontext.Teachers.Find(TeacherID);
                rw.ExitReadLock();

            teacher.FirstName = (updated.FirstName != "") ? updated.FirstName : teacher.FirstName;
            teacher.LastName = (updated.LastName != "") ? updated.LastName : teacher.LastName;
            teacher.email = (updated.email != "") ? updated.email : teacher.email;
            teacher.Salary = (updated.Salary != default(float)) ? updated.Salary : teacher.Salary;
            teacher.PhoneNumber = (updated.PhoneNumber != default(int)) ? updated.PhoneNumber : teacher.PhoneNumber;
     
                rw.EnterWriteLock();
                _dbcontext.Update(teacher);
                _dbcontext.SaveChanges();
                rw.ExitWriteLock();
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
