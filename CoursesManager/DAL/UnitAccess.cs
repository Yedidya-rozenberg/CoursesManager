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
    public static class UnitAccess //: Iunit
    {
        private static CoursesDBcontext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim rw = new ReaderWriterLockSlim();

        public static bool AddUnit(Unit unit, int CourseID)
        {
            try
            {
                rw.EnterReadLock();
                var Course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c => c.CourseID == CourseID);
                rw.ExitReadLock();
                if (Course == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                Course.Units.Add(unit);
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

        public static bool RemoveUnit(int unitID, int TeacherID, Course Course)
        {
            try
            {
                rw.EnterReadLock();
                var _Course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c==Course);
                var unit = _dbContext.Units.Find(unitID);
                rw.ExitReadLock();
                if (unit == null || _cours==null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                if (!(Course.TeacherID == TeacherID && Course.Units.Contains(unit)))
                {
                    Display.Exception(new Exception("Can't do this"));
                    return false;
                }
                _dbContext.Remove(unit);
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

        public static bool UpdateUnit(int unitID, Unit unit)
        {
            try
            {
                rw.EnterReadLock();
                var _unit = _dbContext.Units.Find(unitID);
                rw.ExitReadLock();
                if (_unit == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                _unit.UnitName = unit.UnitName ?? unit.UnitName;
                _unit.StudyContent = unit.StudyContent ?? _unit.StudyContent;
                _unit.Questions = unit.Questions ?? _unit.Questions;
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

        public static Unit ViewUnit(int unitID)
        {
            try
            {
                rw.EnterReadLock();
                var Unit = _dbContext.Units.Find(unitID);
                rw.ExitReadLock();
                if (Unit == null)
                {
                    Display.Exception(new NullReferenceException());
                    return null;
                }
                return Unit;
            }
            catch (Exception ex)
            {

                Display.Exception(ex);
                return null;
            }
        }
    }
 
}
