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
   public static class CourseAccess //: ICourse 
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim rw = new ReaderWriterLockSlim(); // what is slim?

        public static bool addCourse(Course course)
        {
            try
            {
                rw.EnterWriteLock();
                Course c = _dbContext.Courses.FirstOrDefault(co=>co.CourseName==course.CourseName);
                rw.ExitWriteLock();
                if (c != null)
                {
                    Display.Message("There is a course with the same name.");
                    return false;
                }
                _dbContext.Add(course);
                rw.EnterWriteLock();
                _dbContext.SaveChanges();
                rw.ExitWriteLock();
                return true;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }

        }

        public static Course ViewCourse(int courseID)
        {
            try
            {
                rw.EnterWriteLock();
                Course c = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c.CourseID==courseID);
                rw.ExitWriteLock();
                return c;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }

        public static bool UpdateCourseName(int CourseID, string New)
        {
            try
            {
                rw.EnterWriteLock();
                Course c = _dbContext.Courses.Find(CourseID);
                rw.ExitWriteLock();
                if (c == null)
                {
                    Display.Message("This course does not exist.");
                    return false;
                }
                else
                {
                    c.CourseName = New;
                    _dbContext.Update(c);
                    rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    rw.ExitWriteLock();
                    return true;
                }
            }
            catch (Exception ex)
            {

                Display.Exception(ex);
                return false;
            }
        }

        public static IEnumerable<Course> ViewAllCourses()
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
               Display.Exception(ex);
                return null;
            }

        }

        public static IEnumerable<Course> ViewCoursesListByUser(userLogin user)
        {
            try
            {
                var courses = new List<Course>();
                if (user.StudentID != null)
                {
                    rw.EnterReadLock();
                    courses.AddRange(_dbContext.Students.Include(s=>s.Course).Where(s=>s.StudentID==user.StudentID).Select(s=>s.Course).FirstOrDefault().ToList());
                    rw.ExitReadLock();
                }
                if (user.TeacherID != null)
                {
                    rw.EnterReadLock();
                    courses.AddRange(_dbContext.Teachers.Include(t => t.TeachCoursee).Where(t => t.TeacherID == user.TeacherID).Select(t => t.TeachCoursee).FirstOrDefault().ToList());
                    rw.ExitReadLock();
                }


                return courses;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }

        public static IEnumerable<Course> ViewOtherCuorsesListByUser(userLogin user)
        {
            var allCourses = ViewAllCuorses();
            var userCourse = ViewCuorsesListByUser(user);
            var otherCourse = allCourses.Except(userCourse);
            return otherCourse;
        }

        public static bool CheckStudentCourse(int StudentID, int CourseID)
        {
            try
            {
                rw.EnterReadLock();
                var check = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == CourseID).Students.FirstOrDefault(s => s.StudentID == StudentID);
                rw.ExitReadLock();
                return (check != null);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }        
        }

        //public static bool CheckCourseActive(int CourseID)
        //{
        //    try
        //    {
        //        rw.EnterReadLock();
        //        var check = _dbContext.Courses.Find(CourseID);
        //        rw.ExitReadLock();
        //        if (check != null)
        //        { return (check.CuorseStatus == 'O'); }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        Display.Exception(ex);
        //        return false;
        //    }
        //}

    }
}
