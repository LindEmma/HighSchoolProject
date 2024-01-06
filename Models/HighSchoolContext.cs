using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HighSchoolProject.Models;

public partial class HighSchoolContext : DbContext
{
    public HighSchoolContext()
    {
    }

    public HighSchoolContext(DbContextOptions<HighSchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AverageSalary> AverageSalaries { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<TeacherInfo> TeacherInfos { get; set; }

    public virtual DbSet<TotalSalaryPerSectionView> TotalSalaryPerSectionViews { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=HighSchool;Integrated Security=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AverageSalary>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AverageSalary");

            entity.Property(e => e.Avdelning).HasMaxLength(50);
            entity.Property(e => e.Medellönen).HasColumnType("decimal(38, 6)");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(10);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK_Subjects");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseName).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(150);
            entity.Property(e => e.FkPersonnelId).HasColumnName("fk_PersonnelID");

            entity.HasOne(d => d.FkPersonnel).WithMany(p => p.Courses)
                .HasForeignKey(d => d.FkPersonnelId)
                .HasConstraintName("FK_Courses_Personnel");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId);

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.FkCourseId).HasColumnName("fk_CourseID");
            entity.Property(e => e.FkPersonnelId).HasColumnName("fk_PersonnelID");
            entity.Property(e => e.FkStudentId).HasColumnName("fk_StudentID");
            entity.Property(e => e.Grade1).HasColumnName("Grade");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkCourseId)
                .HasConstraintName("FK_Grades_Courses");

            entity.HasOne(d => d.FkPersonnel).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkPersonnelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Personnel");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.Grades)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Grades_Students");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.Property(e => e.PersonnelId).HasColumnName("PersonnelID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkRoleId).HasColumnName("fk_RoleID");
            entity.Property(e => e.FkSectionId).HasColumnName("fk_SectionID");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.SalaryKrPerMonth).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.FkRole).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.FkRoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personnel_Roles");

            entity.HasOne(d => d.FkSection).WithMany(p => p.Personnel)
                .HasForeignKey(d => d.FkSectionId)
                .HasConstraintName("FK_Personnel_Sections");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Section>(entity =>
        {
            entity.Property(e => e.SectionId).HasColumnName("SectionID");
            entity.Property(e => e.SectionName).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("SetStudentGender"));

            entity.Property(e => e.StudentId).HasColumnName("StudentID");
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkClassId).HasColumnName("fk_ClassID");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PersonalNumber)
                .HasMaxLength(15)
                .IsFixedLength();
            entity.Property(e => e.StudentGender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Classes");
        });

        modelBuilder.Entity<TeacherInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TeacherInfo");

            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.SalaryKrPerMonth).HasColumnType("decimal(7, 2)");
            entity.Property(e => e.SectionName).HasMaxLength(50);
        });

        modelBuilder.Entity<TotalSalaryPerSectionView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TotalSalaryPerSectionView");

            entity.Property(e => e.Avdelning).HasMaxLength(50);
            entity.Property(e => e.TotaltUtbetaladLönPerMånadKr)
                .HasColumnType("decimal(38, 2)")
                .HasColumnName("Totalt utbetalad lön per månad (kr)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
