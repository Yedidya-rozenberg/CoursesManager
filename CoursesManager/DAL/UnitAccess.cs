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

        public static bool AddUnit(Unit unit, int CourseID)
        {
            try
            {
                var Course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c => c.CourseID == CourseID);
                if (Course == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                Course.Units.Add(unit);
                _dbContext.SaveChanges();
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
                var _course = _dbContext.Courses.Include(c=>c.Units).FirstOrDefault(c=>c==Course);
                var unit = _dbContext.Units.Find(unitID);
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
                _dbContext.SaveChanges();
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
                var _unit = _dbContext.Units.Find(unitID);
                if (_unit == null)
                {
                    Display.Exception(new NullReferenceException());
                    return false;
                }
                _unit.UnitName = unit.UnitName ?? unit.UnitName;
                _unit.StudyContent = unit.StudyContent ?? _unit.StudyContent;
                _unit.Questions = unit.Questions ?? _unit.Questions;
                _dbContext.SaveChanges();
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
                var Unit = _dbContext.Units.Find(unitID);
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
