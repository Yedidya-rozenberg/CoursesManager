using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CoursesManager.DAL
{
    public static class CourseAccess //: ICourse 
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();

        public static bool AddCourse(Course course)
        {
            try
            {
                _rw.EnterWriteLock();
                Course c = _dbContext.Courses.FirstOrDefault(co=>co.CourseName==course.CourseName);
                _rw.ExitWriteLock();
                if (c != null)
                {
                    Display.Message("There is a course with the same name.");
                    return false;
                }
                _dbContext.Add(course);
                _rw.EnterWriteLock();
                _dbContext.SaveChanges();
                _rw.ExitWriteLock();
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
                _rw.EnterWriteLock();
                Course c = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c.CourseID==courseID);
                _rw.ExitWriteLock();
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
                _rw.EnterWriteLock();
                Course c = _dbContext.Courses.Find(CourseID);
                _rw.ExitWriteLock();
                if (c == null)
                {
                    Display.Message("This course does not exist.");
                    return false;
                }
                else
                {
                    c.CourseName = New;
                    _dbContext.Update(c);
                    _rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    _rw.ExitWriteLock();
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
                _rw.EnterReadLock();
                var courses = _dbContext.Courses.Include(c=>c.Teacher);
                _rw.ExitReadLock();
                return courses;
            }
            catch (Exception ex)
            {
               Display.Exception(ex);
                return null;
            }

        }

        public static IEnumerable<Course> ViewCoursesListByUser(UserLogin user)
        {
            try
            {
                var courses = new List<Course>();
                if (user.StudentID != null)
                {
                    _rw.EnterReadLock();
                    courses.AddRange(_dbContext.Students.Include(s=>s.Course).Where(s=>s.StudentID==user.StudentID).Select(s=>s.Course).FirstOrDefault().ToList());
                    _rw.ExitReadLock();
                }
                if (user.TeacherID != null)
                {
                    _rw.EnterReadLock();
                    courses.AddRange(_dbContext.Teachers.Include(t => t.TeachCourses).Where(t => t.TeacherID == user.TeacherID).Select(t => t.TeachCourses).FirstOrDefault().ToList());
                    _rw.ExitReadLock();
                }


                return courses;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }

        public static IEnumerable<Course> ViewOtherCuorsesListByUser(UserLogin user)
        {
            var allCourses = ViewAllCourses();
             var userCourse = ViewCoursesListByUser(user);
            var otherCourse = allCourses.Except(userCourse);
            return otherCourse;
        }

        public static bool CheckStudentCourse(int StudentID, int CourseID)
        {
            try
            {
                _rw.EnterReadLock();
                var check = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == CourseID).Students.FirstOrDefault(s => s.StudentID == StudentID);
                _rw.ExitReadLock();
                return (check != null);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }        
        }
    }
}
