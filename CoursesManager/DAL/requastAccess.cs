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
    public static class requestAccess //: Irequast
    {
        private static CoursesDBcontext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();
        //return the new requestID
        public static int? AddRequest(Request request)
        {
            try
            {
                _dbContext.Add(request);
                rw.EnterWriteLock();
                _dbContext.SaveChanges();
                rw.ExitWriteLock();
                rw.EnterReadLock();
                var _request = _dbContext.requests.FirstOrDefault(r => r == request);
                rw.ExitReadLock();
                if (_request != null)
                { return _request.RequestID; }
                else
                { return null; }
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
        }
        public static List<Request> GetNewRequests(DateTime time)
        {
            try
            {
                rw.EnterReadLock();
                var reqests = _dbContext.requests.Where(r => r.RequestTime > time).Select(r => r).ToList();
                rw.ExitReadLock();
                return reqests;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }

        //public static List<Request> GetRequestByParameter(string parameterName, string Parameter)
        //{
        //    throw new NotImplementedException();
        //}

        public static bool UpdateRequestStatus(int requestID, string newStatus)
        {
            try
            {
                rw.EnterReadLock();
                var request = _dbContext.requests.Find(requestID);
                rw.ExitReadLock();
                request.RequestStatus = newStatus;
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

        public static bool MakeRequest(Request request)
        {
            try
            {
                if (request.RequestID==default(int))
                { Display.Exception(new NullReferenceException()); return false; }

                bool success;
                switch (request.RequestCode)
                {
                    case 'A':
                        success = activeCourse(request);
                        break;
                    case 'D':
                        success = DeleteCourse(request);
                        break;
                    case 'G':
                        success = registerStudent(request);
                        break;
                    case 'M':
                        success = RemoveStudentFromCourse(request);
                        break;
                    default:
                        Display.Exception(new ArgumentException("The request code not match."));
                        success = false;
                        break;
                }
                return success;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }

        }

        private static bool DeleteCourse(Request request)
        {
            try
            {
                rw.EnterReadLock();
                var course = _dbContext.Courses.Find(request.CourseID);
                rw.ExitReadLock();
                course.CourseStatus = 'C';
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

        private static bool RemoveStudentFromCourse(Request request)
        {
            try
            {
                rw.EnterReadLock();
                var course = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == request.CourseID);
                var student = _dbContext.Students.Find(request.StudentID);
                rw.ExitReadLock();
                course.Students.Remove(student);
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

        private static bool registerStudent(Request request)
        {
            try
            {
                rw.EnterReadLock();
                var course = _dbContext.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.CourseID == request.CourseID);
                var student = _dbContext.Students.Find(request.StudentID);
                rw.ExitReadLock();
                course.Students.Add(student);
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

        private static bool activeCourse(Request request)
        {
            try
            {
                rw.EnterReadLock();
                var course = _dbContext.Courses.Find(request.CourseID);
                rw.ExitReadLock();
                course.CourseStatus = 'O';
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
    }
}
