﻿// <auto-generated />
using System;
using CoursesManager.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoursesManager.Migrations
{
    [DbContext(typeof(CoursesDBContext))]
    [Migration("20220131170815_CoursStatus")]
    partial class CoursStatus
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<int>("StudentsStudentID")
                        .HasColumnType("int");

                    b.HasKey("CourseID", "StudentsStudentID");

                    b.HasIndex("StudentsStudentID");

                    b.ToTable("CourseStudent");
                });

            modelBuilder.Entity("CoursesManager.Models.Course", b =>
                {
                    b.Property<int>("CourseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("CourseStatus")
                        .HasColumnType("bit");

                    b.Property<int>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("CourseID");

                    b.HasIndex("TeacherID");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CoursesManager.Models.Request", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("RequestCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("RequestDetails")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RequestTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StudentID")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("CourseID");

                    b.HasIndex("StudentID");

                    b.HasIndex("TeacherID");

                    b.ToTable("requests");
                });

            modelBuilder.Entity("CoursesManager.Models.Student", b =>
                {
                    b.Property<int>("StudentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Payment")
                        .HasColumnType("real");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.HasKey("StudentID");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CoursesManager.Models.Teacher", b =>
                {
                    b.Property<int>("TeacherID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("int");

                    b.Property<float>("Salary")
                        .HasColumnType("real");

                    b.HasKey("TeacherID");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("CoursesManager.Models.Unit", b =>
                {
                    b.Property<int>("UnitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("Questions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudyContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitID");

                    b.HasIndex("CourseID");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("CoursesManager.Models.UserLogin", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("StudentID")
                        .HasColumnType("int");

                    b.Property<int?>("TeacherID")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("UserID");

                    b.HasIndex("StudentID")
                        .IsUnique()
                        .HasFilter("[StudentID] IS NOT NULL");

                    b.HasIndex("TeacherID")
                        .IsUnique()
                        .HasFilter("[TeacherID] IS NOT NULL");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CourseStudent", b =>
                {
                    b.HasOne("CoursesManager.Models.Course", null)
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursesManager.Models.Student", null)
                        .WithMany()
                        .HasForeignKey("StudentsStudentID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CoursesManager.Models.Course", b =>
                {
                    b.HasOne("CoursesManager.Models.Teacher", "Teacher")
                        .WithMany("TeachCourses")
                        .HasForeignKey("TeacherID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoursesManager.Models.Request", b =>
                {
                    b.HasOne("CoursesManager.Models.Course", "Course")
                        .WithMany()
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoursesManager.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentID");

                    b.HasOne("CoursesManager.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherID");

                    b.Navigation("Course");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoursesManager.Models.Unit", b =>
                {
                    b.HasOne("CoursesManager.Models.Course", "Course")
                        .WithMany("Units")
                        .HasForeignKey("CourseID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("CoursesManager.Models.UserLogin", b =>
                {
                    b.HasOne("CoursesManager.Models.Student", "Student")
                        .WithOne("UserLogin")
                        .HasForeignKey("CoursesManager.Models.UserLogin", "StudentID");

                    b.HasOne("CoursesManager.Models.Teacher", "Teacher")
                        .WithOne("UserLogin")
                        .HasForeignKey("CoursesManager.Models.UserLogin", "TeacherID");

                    b.Navigation("Student");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("CoursesManager.Models.Course", b =>
                {
                    b.Navigation("Units");
                });

            modelBuilder.Entity("CoursesManager.Models.Student", b =>
                {
                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("CoursesManager.Models.Teacher", b =>
                {
                    b.Navigation("TeachCourses");

                    b.Navigation("UserLogin");
                });
#pragma warning restore 612, 618
        }
    }
}
