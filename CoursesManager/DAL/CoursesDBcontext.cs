using CoursesManager.Models;
using Microsoft.EntityFrameworkCore;

namespace CoursesManager.DAL
{
    public class CoursesDBContext : DbContext
    {

        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserDetails>  Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Request> Requests { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ScoolDB;Trusted_Connection=True;MultipleActiveResultSets=true;");
                //    .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            }
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDetails>().ToTable("Users");
            modelBuilder.Entity<Student>().ToTable("Students");
            modelBuilder.Entity<Teacher>().ToTable("Teachers");

        }


    }
    public class GetDB
    {
        private GetDB()
        {

        }
        private static CoursesDBContext instance;
        private static readonly object key = new object();
        public static CoursesDBContext GetInstance()
        {
            if (instance == null)
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new CoursesDBContext();
                    }
                }
            }
            return instance;
        }

    }
}
