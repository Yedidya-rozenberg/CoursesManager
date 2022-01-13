using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;

namespace CoursesManager.DAL
{
    public static class UnitAccess //: Iunit
    {
        private static CoursesDBContext _dbContext = GetDB.GetInstance();
        private static ReaderWriterLockSlim _rw = new ReaderWriterLockSlim();

        public static bool AddUnit(Unit unit, int CourseID)
        {
            try
            {
                _rw.EnterReadLock();
                var Course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c => c.CourseID == CourseID);
                _rw.ExitReadLock();
                if (Course == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                Course.Units.Add(unit);
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

        public static bool RemoveUnit(int unitID, int TeacherID, Course Course)
        {
            try
            {
                _rw.EnterReadLock();
                var _course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c==Course);
                var unit = _dbContext.Units.Find(unitID);
                _rw.ExitReadLock();
                if (unit == null || _course==null)
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

        public static bool UpdateUnit(int unitID, Unit unit)
        {
            try
            {
                _rw.EnterReadLock();
                var _unit = _dbContext.Units.Find(unitID);
                _rw.ExitReadLock();
                if (_unit == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                _unit.UnitName = unit.UnitName ?? unit.UnitName;
                _unit.StudyContent = unit.StudyContent ?? _unit.StudyContent;
                _unit.Questions = unit.Questions ?? _unit.Questions;
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

        public static Unit ViewUnit(int unitID)
        {
            try
            {
                _rw.EnterReadLock();
                var Unit = _dbContext.Units.Find(unitID);
                _rw.ExitReadLock();
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
