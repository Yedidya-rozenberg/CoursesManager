using Microsoft.EntityFrameworkCore;
using CoursesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoursesManager.DAL
{
   public class CoursesDBcontext : DbContext
    {
        //private CoursesDBcontext()
        //{

        //}
        //private static CoursesDBcontext instance;
        //private static readonly object key = new object();
        //public static CoursesDBcontext GetInstance()
        //{
        //    if (instance == null)
        //    {
        //        lock (key)
        //        {
        //            if (instance == null)
        //            {
        //                instance = new CoursesDBcontext();
        //            }
        //        }
        //    }
        //    return instance;
        //}
        public DbSet<UserLoggin> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Cours> Courses { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Request> requests { get; set; }
        //public DbSet<StudentCours> studentCours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ScoolDB;Trusted_Connection=True;MultipleActiveResultSets=true;");

                //optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=BlogDB;Trusted_Connection=True;MultipleActiveResultSets=true;")
                //    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<UserLoggin>().Property("UserID").UseIdentityColumn(); //.HasKey(nameof(UserLoggin.UserID));
           // modelBuilder.Entity<StudentCours>().HasKey(nameof(StudentCours.StudentID), nameof(StudentCours.CoursID));
        }


    }
    public class GetDB
    {
        private GetDB()
        {

        }
        private static CoursesDBcontext instance;
        private static readonly object key = new object();
        public static CoursesDBcontext GetInstance()
        {
            if (instance == null)
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new CoursesDBcontext();
                    }
                }
            }
            return instance;
        }

    }
}
