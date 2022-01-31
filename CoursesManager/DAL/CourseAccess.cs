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

        public static bool AddCourse(Course course)
        {
            try
            {
                Course c = _dbContext.Courses.FirstOrDefault(co=>co.CourseName==course.CourseName);
                if (c != null)
                {
                    Display.Message("There is a course with the same name.");
                    return false;
                }
                _dbContext.Add(course);
                _dbContext.SaveChanges();
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
                Course c = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c.CourseID==courseID);
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
                Course c = _dbContext.Courses.Find(CourseID);
                if (c == null)
                {
                    Display.Message("This course does not exist.");
                    return false;
                }
                else
                {
                    c.CourseName = New;
                    _dbContext.Update(c);
                    _dbContext.SaveChanges();
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
                var courses = _dbContext.Courses.Include(c=>c.Teacher).ToList();
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
                    courses.AddRange(_dbContext.Students.Include(s=>s.Course).Where(s=>s.StudentID==user.StudentID).Select(s=>s.Course).FirstOrDefault().ToList());
                }
                if (user.TeacherID != null)
                {
                    courses.AddRange(_dbContext.Teachers.Include(t => t.TeachCourses).Where(t => t.TeacherID == user.TeacherID).Select(t => t.TeachCourses).FirstOrDefault().ToList());
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
                var check = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == CourseID).Students.FirstOrDefault(s => s.StudentID == StudentID);
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
