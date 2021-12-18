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

        public static bool ViewDediles(int ID, ref Student s)
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
                Display.Exeption(ex);
                return false;
            }
            if ((student == null))
            {
                return false;
            }
            student.Payment = default(float);
            student.userLoggin = null;
            s = student;
            return true;

        }

        public static bool ViewMyDediles(UserLoggin user, ref Student student, ref Teacher teacher)
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
                Display.Exeption(ex);
            }
            if(student!= null || teacher != null) { return true; }
            else { return false; }
        }

        public static bool UpdateDetiles(int StudentID, Student updated)
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
            student.LestName = (updated.LestName != "") ? updated.LestName : student.LestName;
            student.email = (updated.email != "") ? updated.email : student.email;
            student.Payment = (updated.Payment != default(float)) ? updated.Payment : student.Payment;
            student.PhonNumber = (updated.Payment != default(int)) ? updated.PhonNumber : student.PhonNumber;
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
                Display.Exeption(ex);
            }
            return result;
        }

        public static bool RemoveStudentFromCuers(int coursID, int studentID)
        {
            bool result = false;
            Cours course = null;
            Student student;
            try
            {
                rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.Cours).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Cours.FirstOrDefault(c => c.CoursID == coursID);
                }
                rw.ExitReadLock();
                if (course != null)
                {
                    student.Cours.Remove(course);
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
                Display.Exeption(ex);
            }
            return result;
        }

        public static bool AddStudentToCuers(int coursID, int studentID)
        {
            Cours course = null;
            Student student;
            try
            {
                rw.EnterReadLock();
                student = _dbContext.Students.Include(s => s.Cours).FirstOrDefault(p => p.StudentID == studentID);
                if (student != null)
                {
                    course = student.Cours.FirstOrDefault(c => c.CoursID == coursID);
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
                    student.Cours.Add(course);
                    rw.EnterWriteLock();
                    _dbContext.SaveChanges();
                    rw.ExitWriteLock();
                    return true;
                }
                Display.Message("Record are exist");
            }
            catch(Exception ex)
            {
                Display.Exeption(ex);
            }
            return false;
        }

        public static UserLoggin ChackUser(UserLoggin user)
        {
            try
            {
                rw.EnterReadLock();
                UserLoggin userLoggin = _dbContext.Users.FirstOrDefault(u => u.UserName == user.UserName && u.Password == user.Password);
                rw.ExitReadLock();
                return userLoggin;
            
            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return null;
            }
        }

        public static UserLoggin ReturnUserLogginByUserID(int userID)
        {
            try
            {
                rw.EnterReadLock();
                UserLoggin userLoggin = _dbContext.Users.Find(userID);
                rw.ExitReadLock();
                if (userLoggin == null)
                {
                    Display.Message("Try again");
                }
                return userLoggin;

            }
            catch (Exception ex)
            {
                Display.Exeption(ex);
                return null;
            }
        }

    }
}
