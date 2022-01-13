using CoursesManager.Models;
using System.Collections.Generic;

namespace CoursesManager.Logic
{
    public interface IMainScreen
    {
        T MyDetails<T>(UserLogin user);
        List<Course> MyCourses(UserLogin user);
        List<Course> AllCourses();
        Course ViewCourse(int CourseID);
    }
    public interface IUnitScreen
    {
        string GetStudy();
        string GetQuestions();
        void BackUnit();
        void NextUnit();
        void BackToCourse();
        void ConnectAsTeacher();
    }
    public interface ITeacherUnitScreen : IUnitScreen
    {
        bool UpdateName();
        bool UpdatStudy();
        bool UpdatQuestions();
    }
    public interface ILoginScreen
    {
        bool CheckLogin();
    }
}
