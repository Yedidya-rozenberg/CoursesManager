using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CoursesManager.DAL
{
    public static class RequestAccess //: Irequast
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();
        //return the new requestID
        public static int? AddRequest(Request request)
        {
            try
            {
                _dbContext.Add(request);
                _rw.EnterWriteLock();
                _dbContext.SaveChanges();
                _rw.ExitWriteLock();
                _rw.EnterReadLock();
                var _request = _dbContext.requests.FirstOrDefault(r => r == request);
                _rw.ExitReadLock();
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
                _rw.EnterReadLock();
                var reqests = _dbContext.requests.Where(r => r.RequestTime > time).Select(r => r).ToList();
                _rw.ExitReadLock();
                return reqests;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }

        }


        public static bool UpdateRequestStatus(int requestID, string newStatus)
        {
            try
            {
                _rw.EnterReadLock();
                var request = _dbContext.requests.Find(requestID);
                _rw.ExitReadLock();
                request.RequestStatus = newStatus;
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
                        success = ActiveCourse(request);
                        break;
                    case 'D':
                        success = DeleteCourse(request);
                        break;
                    case 'G':
                        success = RegisterStudent(request);
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
                _rw.EnterReadLock();
                var course = _dbContext.Courses.Find(request.CourseID);
                _rw.ExitReadLock();
                course.CourseStatus = false;
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

        private static bool RemoveStudentFromCourse(Request request)
        {
            try
            {
                _rw.EnterReadLock();
                var course = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == request.CourseID);
                var student = _dbContext.Students.Find(request.StudentID);
                _rw.ExitReadLock();
                course.Students.Remove(student);
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

        private static bool RegisterStudent(Request request)
        {
            try
            {
                _rw.EnterReadLock();
                var course = _dbContext.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.CourseID == request.CourseID);
                var student = _dbContext.Students.Find(request.StudentID);
                _rw.ExitReadLock();
                course.Students.Add(student);
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

        private static bool ActiveCourse(Request request)
        {
            try
            {
                _rw.EnterReadLock();
                var course = _dbContext.Courses.Find(request.CourseID);
                _rw.ExitReadLock();
                course.CourseStatus = true;
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
    }
}
