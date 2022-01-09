using CoursesManager.DAL;
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
    public class StudentAccess
    {
        private static CoursesDBcontext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();

        public static bool ViewDetails(int ID, ref Student s)
        {
            Student student = new();
            try
            {
                rw.EnterReadLock();
                student = _dbContext.Students.FirstOrDefault(s => s.StudentID == ID);
                rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }
            if (student == null)
            {
                return false;
            }
            student.Payment = default(float);
            student.userLogin = null;
            s = student;
            return true;

        }

        public static bool ViewMyDetails(userLogin user, ref Student student, ref Teacher teacher)
        {
            try
            {
               rw.EnterReadLock();
                student = _dbContext.Students.Find(user.StudentID);
                teacher = _dbContext.Teachers.Find(user.TeacherID);
                rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            if(student!= null || teacher != null) { return true; }
            else { return false; }
        }

        public static bool UpdateDetails(int StudentID, Student updated)
        {
            Student student = new();
            bool result = false;
            try
            {
                rw.EnterReadLock();
                student = _dbContext.Students.Find(StudentID);
                rw.ExitReadLock();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result;
            }
            student.FirstName = (updated.FirstName != "") ? updated.FirstName : student.FirstName;
            student.LastName = (updated.LastName != "") ? updated.LastName : student.LastName;
            student.email = (updated.email != "") ? updated.email : student.email;
            student.Payment = (updated.Payment != default(float)) ? updated.Payment : student.Payment;
            student.PhoneNumber = (updated.Payment != default(int)) ? updated.PhoneNumber : student.PhoneNumber;
            try
            {
                rw.EnterWriteLock();
                _dbContext.Update(student);
                _dbContext.SaveChanges();
                rw.ExitWriteLock();
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
                rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.Course).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Course.FirstOrDefault(c => c.CourseID == CourseID);
                }
                rw.ExitReadLock();
                if (course != null)
                {
                    student.Course.Remove(course);
                    rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    rw.ExitWriteLock();
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
                rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.Course).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Course.FirstOrDefault(c => c.CourseID == CourseID);
                    rw.ExitReadLock();

                }
                else
                {
                    rw.ExitReadLock();
                    Display.Message("Student not found");
                    return false; 
                }
                rw.ExitReadLock();
                if (course==null)
                {
                    student.Course.Add(course);
                    rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    rw.ExitWriteLock();
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

        public static userLogin CheckUser(userLogin user)
        {
            try
            {
                rw.EnterReadLock();
                userLogin userLogin = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                rw.ExitReadLock();
                return userLogin;
            
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }

        public static userLogin ReturnUserLoginByUserID(int userID)
        {
            try
            {
                rw.EnterReadLock();
                userLogin userLogin = _dbContext.Users.Find(userID);
                rw.ExitReadLock();
                if (userLogin == null)
                {
                    Display.Message("Try again");
                }
                return userLogin;

            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }

    }
}
