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
                _dbContext.SaveChanges();
                var _request = _dbContext.Requests.FirstOrDefault(r => r == request);
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
                var reqests = _dbContext.Requests.Where(r => r.RequestTime > time).Select(r => r).ToList();
                return reqests;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return null;
            }
            finally
            {
                _rw.ExitReadLock();
            }

        }


        public static bool UpdateRequestStatus(int requestID, string newStatus)
        {
            try
            {
                var request = _dbContext.Requests.Find(requestID);
                request.RequestStatus = newStatus;
                _dbContext.SaveChanges();
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
                switch (request.Code)
                {
                    case RequestCode.ActiveCourse:
                        success = ActiveCourse(request);
                        break;
                    case RequestCode.DeleteCourse:
                        success = DeleteCourse(request);
                        break;
                    case RequestCode.Register:
                        success = RegisterStudent(request);
                        break;
                    case RequestCode.RemoveStudent:
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
                var course = _dbContext.Courses.Find(request.CourseID);
                course.CourseStatus = 'C';
                _dbContext.SaveChanges();
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
                var course = _dbContext.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseID == request.CourseID);
                var student = _dbContext.Students.FirstOrDefault(s=>s.UserDetailsID == request.UserDetailsID);
                var success =  course.Students.Remove(student);
                _dbContext.SaveChanges();
                return success;
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
                var course = _dbContext.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.CourseID == request.CourseID);
                var student = _dbContext.Students.FirstOrDefault(s => s.UserDetailsID == request.UserDetailsID);
                if (course != null && student != null)
                {
                    course.Students.Add(student);
                    _dbContext.SaveChanges();
                    return true;
                }
                    return false;
             
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
                var course = _dbContext.Courses.Find(request.CourseID);
                if (course != null)
                {
                    course.CourseStatus = 'O';
                    _dbContext.SaveChanges();
                    return true;
                }
                    return false;
            }
            catch (Exception ex)
            {
                Display.Exception(ex);
                return false;
            }

        }
    }
}
