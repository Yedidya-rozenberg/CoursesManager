using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace CoursesManager.DAL
{
    public class StudentAccess
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();

        public static Student ViewStudentDetails(int UserDetilesID)
        {
            try
            {
               var student = _dbContext.Students.FirstOrDefault(s =>s.UserDetailsID == UserDetilesID);
                return student;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }



        public static bool ViewUserDetails(UserLogin user, ref UserDetails userDetails)
        {
            try
            {
               _rw.EnterReadLock();
                userDetails = _dbContext.Users.Find(user.UserDetailsesID);
                _rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            return (userDetails != null);
        }

        public static bool UpdateDetails(int StudentID, Student updated)
        {
            Student student = new();
            bool result = false;
            try
            {
                _rw.EnterReadLock();
                student = _dbContext.Students.Find(StudentID);
                _rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return result;
            }
            student.FirstName = (updated.FirstName != "") ? updated.FirstName : student.FirstName;
            student.LastName = (updated.LastName != "") ? updated.LastName : student.LastName;
            student.Email = (updated.Email != "") ? updated.Email : student.Email;
            student.Payment = (updated.Payment != default(float)) ? updated.Payment : student.Payment;
            student.PhoneNumber = (updated.Payment != default(int)) ? updated.PhoneNumber : student.PhoneNumber;
            try
            {
                _rw.EnterWriteLock();
                _dbContext.Update(student);
                _dbContext.SaveChanges();
                _rw.ExitWriteLock();
                result = true;
            }               
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            return result;
        }

        public static bool RemoveStudentFromCourse(int CourseID, int studentID)
        {
            bool result = false;
            Course course = null;
            Student student;
            try
            {
                _rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.StudyCourses).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.StudyCourses.FirstOrDefault(c => c.CourseID == CourseID);
                }
                _rw.ExitReadLock();
                if (course != null)
                {
                    student.StudyCourses.Remove(course);
                    _rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    _rw.ExitWriteLock();
                    result = true;
                }
                else
                {
                    Console.WriteLine("No records");
                }
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            return result;
        }

        public static bool AddStudentToCourse(int CourseID, int studentID)
        {
            Course course = null;
            Student student;
            try
            {
                _rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.StudyCourses).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.StudyCourses.FirstOrDefault(c => c.CourseID == CourseID);
                    _rw.ExitReadLock();

                }
                else
                {
                    _rw.ExitReadLock();
                    Display.Message("Student not found");
                    return false; 
                }
                _rw.ExitReadLock();
                if (course==null)
                {
                    student.StudyCourses.Add(course);
                    _rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    _rw.ExitWriteLock();
                    return true;
                }
                Display.Message("Record is exist");
            }
            catch(Exception ex)
            {
                Display.Exception(ex);
            }
            return false;
        }

 

        public static UserLogin ReturnUserLoginByUserID(int userID)
        {
            try
            {
                _rw.EnterReadLock();
                UserLogin user = _dbContext.UserLogins.Find(userID);
                _rw.ExitReadLock();
                if (user == null)
                {
                    Display.Message("Try again");
                }
                return user;

            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }

    }
}
