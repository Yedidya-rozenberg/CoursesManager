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

        public static bool ViewDediles(int ID, ref Teacher t)
        {
            Teacher teacher = new();
            try
            {
                rw.EnterReadLock();
                teacher = _dbcontext.Teachers.FirstOrDefault(s => s.userLoggin.UserID == ID);
                rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
            }
            if ((teacher == null))
            {
                return false;
            }
            teacher.Selery = default(float);
            teacher.userLoggin = null;
            t = teacher;
            return true;
        }

        //public static bool ViewMyTeacherDediles(int TeacherID, ref Teacher teacher)
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
        //        Display.Exeption(ex);
        //    }
        //    if ((T == null))
        //    {
        //        return false;
        //    }
        //    teacher = T;
        //    return true;
        //}

        public static bool UpdateDetiles(int TeacherID, Teacher updated)
        {
            Teacher teacher = new();
            try
            {
                rw.EnterReadLock();
                teacher = _dbcontext.Teachers.Find(TeacherID);
                rw.ExitReadLock();

            teacher.FirstName = (updated.FirstName != "") ? updated.FirstName : teacher.FirstName;
            teacher.LestName = (updated.LestName != "") ? updated.LestName : teacher.LestName;
            teacher.email = (updated.email != "") ? updated.email : teacher.email;
            teacher.Selery = (updated.Selery != default(float)) ? updated.Selery : teacher.Selery;
            teacher.PhonNumber = (updated.PhonNumber != default(int)) ? updated.PhonNumber : teacher.PhonNumber;
     
                rw.EnterWriteLock();
                _dbcontext.Update(teacher);
                _dbcontext.SaveChanges();
                rw.ExitWriteLock();
                return true;
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return false;
            }

        }


    }
}
