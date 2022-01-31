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

        public static bool ViewDetails(int ID, ref Student s)
        {
            Student student = new();
            try
            {
                _rw.EnterReadLock();
                student = _dbContext.Students.FirstOrDefault(s => s.StudentID == ID);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }
            finally
            {
                _rw.ExitReadLock();
            }

            if (student == null)
            {
                return false;
            }
            student.Payment = default(float);
            student.UserLogin = null;
            s = student;
            return true;

        }

        public static bool ViewMyDetails(UserLogin user, ref Student student, ref Teacher teacher)
        {
            try
            {
               _rw.EnterReadLock();
                student = _dbContext.Students.Find(user.StudentID);
                teacher = _dbContext.Teachers.Find(user.TeacherID);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
            }
            finally
            {
                _rw.ExitReadLock();
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
                _rw.EnterReadLock();
                student = _dbContext.Students.Find(StudentID);
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return result;
            }
            finally
            {
                _rw.ExitReadLock();
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
                student = _dbContext.Students.Include(s => s.Course).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Course.FirstOrDefault(c => c.CourseID == CourseID);
                }
                _rw.ExitReadLock();
                if (course != null)
                {
                    student.Course.Remove(course);
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
                student = _dbContext.Students.Include(s => s.Course).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Course.FirstOrDefault(c => c.CourseID == CourseID);
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
                    student.Course.Add(course);
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

        public static UserLogin CheckUser(UserLogin user)
        {
            try
            {
                _rw.EnterReadLock();
                UserLogin userLogin = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                _rw.ExitReadLock();
                return userLogin;
            
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }

        public static UserLogin ReturnUserLoginByUserID(int userID)
        {
            try
            {
                _rw.EnterReadLock();
                UserLogin userLogin = _dbContext.Users.Find(userID);
                _rw.ExitReadLock();
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
