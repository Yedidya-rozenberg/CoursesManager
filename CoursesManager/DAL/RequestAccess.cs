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
        //return the new requestID
        public static int? AddRequest(Request request)
        {
            try
            {
                _dbContext.Add(request);
                _dbContext.SaveChanges();
                var _request = _dbContext.requests.FirstOrDefault(r => r == request);
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
                var reqests = _dbContext.requests.Where(r => r.RequestTime > time).Select(r => r).ToList();
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
                var request = _dbContext.requests.Find(requestID);
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
                var course = _dbContext.Courses.Find(request.CourseID);
                course.CourseStatus = false;
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
                var student = _dbContext.Students.Find(request.StudentID);
                course.Students.Remove(student);
                _dbContext.SaveChanges();
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
                var course = _dbContext.Courses.Include(c=>c.Students).FirstOrDefault(c=>c.CourseID == request.CourseID);
                var student = _dbContext.Students.Find(request.StudentID);
                course.Students.Add(student);
                _dbContext.SaveChanges();
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
                var course = _dbContext.Courses.Find(request.CourseID);
                course.CourseStatus = true;
                _dbContext.SaveChanges();
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
