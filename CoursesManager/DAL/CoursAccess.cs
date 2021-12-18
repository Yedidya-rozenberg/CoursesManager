using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoursesManager.DAL
{
   public static class CoursAccess //: Icours
    {
        private static CoursesDBcontext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();

        public static bool addCours(Cours cours)
        {
            try
            {
                rw.EnterWriteLock();
                Cours c = _dbContext.Courses.FirstOrDefault(co=>co.CoursName==cours.CoursName);
                rw.ExitWriteLock();
                if (c != null)
                {
                    Display.Message("There is a course with the same name.");
                    return false;
                }
                _dbContext.Add(cours);
                rw.EnterWriteLock();
                _dbContext.SaveChanges();
                rw.ExitWriteLock();
                return true;
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return false;
            }

        }

        public static Cours VeiwCours(int coursID)
        {
            try
            {
                rw.EnterWriteLock();
                Cours c = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c.CoursID==coursID);
                rw.ExitWriteLock();
                return c;
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return null;
            }

        }

        public static bool UpdateCoursName(int CoursID, string New)
        {
            try
            {
                rw.EnterWriteLock();
                Cours c = _dbContext.Courses.Find(CoursID);
                rw.ExitWriteLock();
                if (c == null)
                {
                    Display.Message("This course not exixst.");
                    return false;
                }
                else
                {
                    c.CoursName = New;
                    _dbContext.Update(c);
                    rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    rw.ExitWriteLock();
                    return true;
                }
            }
            catch (Exception ex)
            {

                Display.Exeption(ex);
                return false;
            }
        }

        public static IEnumerable<Cours> VeiwAllCuorses()
        {
            try
            {
                rw.EnterReadLock();
                var courses = _dbContext.Courses.Include(c=>c.Teacher);
                rw.ExitReadLock();
                return courses;
            }
            catch (Exception ex)
            {
               Display.Exeption(ex);
                return null;
            }

        }

        public static IEnumerable<Cours> VeiwCuorsesListByUser(UserLoggin user)
        {
            try
            {
                var courses = new List<Cours>();
                if (user.StudentID != null)
                {
                    rw.EnterReadLock();
                    courses.AddRange(_dbContext.Students.Include(s=>s.Cours).Where(s=>s.StudentID==user.StudentID).Select(s=>s.Cours).FirstOrDefault().ToList());
                    rw.ExitReadLock();
                }
                if (user.TeacherID != null)
                {
                    rw.EnterReadLock();
                    courses.AddRange(_dbContext.Teachers.Include(t => t.TeachCours).Where(t => t.TeacherID == user.TeacherID).Select(t => t.TeachCours).FirstOrDefault().ToList());
                    rw.ExitReadLock();
                }


                return courses;
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return null;
            }

        }

        public static IEnumerable<Cours> VeiwOtherCuorsesListByUser(UserLoggin user)
        {
            var allCours = VeiwAllCuorses();
            var userCours = VeiwCuorsesListByUser(user);
            var otherCours = allCours.Except(userCours);
            return otherCours;
        }

        public static bool CheckStudentCours (int StudentID, int CoursID)
        {
            try
            {
                rw.EnterReadLock();
                var check = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CoursID == CoursID).Students.FirstOrDefault(s => s.StudentID == StudentID);
                rw.ExitReadLock();
                return (check != null);
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return false;
            }        
        }

        //public static bool CheckCoursActive(int CoursID)
        //{
        //    try
        //    {
        //        rw.EnterReadLock();
        //        var check = _dbContext.Courses.Find(CoursID);
        //        rw.ExitReadLock();
        //        if (check != null)
        //        { return (check.CuorsStatus == 'O'); }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Display.Exeption(ex);
        //        return false;
        //    }
        //}

    }
}
