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

        public static bool AddUnit(Unit unit, int CoursID)
        {
            try
            {
                rw.EnterReadLock();
                var cours = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c => c.CoursID == CoursID);
                rw.ExitReadLock();
                if (cours == null)
                {
                    Display.Exeption(new NullReferenceException());
                    return false;
                }
                cours.Units.Add(unit);
                rw.EnterWriteLock();
                _dbContext.SaveChanges();
                rw.ExitWriteLock();
                return true;
            }
            catch (Exception ex)
            {

                Display.Exeption(ex);
                return false;
            }        
        }

        public static bool RemoveUnit(int unitID, int TeacherID, Cours cours)
        {
            try
            {
                rw.EnterReadLock();
                var _cours = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c==cours);
                var unit = _dbContext.Units.Find(unitID);
                rw.ExitReadLock();
                if (unit == null || _cours==null)
                {
                    Display.Exeption(new NullReferenceException());
                    return false;
                }
                if (!(cours.TeacherID == TeacherID && cours.Units.Contains(unit)))
                {
                    Display.Exeption(new Exception("Can't do this"));
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

                Display.Exeption(ex);
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
                    Display.Exeption(new NullReferenceException());
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

                Display.Exeption(ex);
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
                    Display.Exeption(new NullReferenceException());
                    return null;
                }
                return Unit;
            }
            catch (Exception ex)
            {

                Display.Exeption(ex);
                return null;
            }
        }
    }
 
}
