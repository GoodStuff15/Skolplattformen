using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Skolplattformen.Models;

public partial class Labb2Context : DbContext
{
    public Labb2Context()
    {
    }

    public Labb2Context(DbContextOptions<Labb2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<GradeScale> GradeScales { get; set; }

    public virtual DbSet<JobTitle> JobTitles { get; set; }

    public virtual DbSet<SchoolSubject> SchoolSubjects { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentGrade> StudentGrades { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-IGVAOCU;Database=Labb2; Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Class__3214EC073443CC72");

            entity.ToTable("Class");

            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");

            entity.HasOne(d => d.Staff).WithMany(p => p.Classes)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__Class__Staff_Id__403A8C7D");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Course__3214EC070DDB4DAA");

            entity.ToTable("Course");

            entity.Property(e => e.CourseName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CurrentClassId).HasColumnName("Current_Class_Id");
            entity.Property(e => e.SubjectId).HasColumnName("Subject_Id");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");

            entity.HasOne(d => d.CurrentClass).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CurrentClassId)
                .HasConstraintName("FK_Course_Class");

            entity.HasOne(d => d.Subject).WithMany(p => p.Courses)
                .HasForeignKey(d => d.SubjectId)
                .HasConstraintName("FK_Course_Subject");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK_Course_Teacher");
        });

        modelBuilder.Entity<GradeScale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__GradeSca__3214EC07E8F7E0D2");

            entity.ToTable("GradeScale");

            entity.Property(e => e.GradeName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<JobTitle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__JobTitle__3214EC0780AF6373");

            entity.ToTable("JobTitle");

            entity.Property(e => e.JobRole)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TitleName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<SchoolSubject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SchoolSu__3214EC0786C786DB");

            entity.ToTable("SchoolSubject");

            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Staff__3214EC07A5FE64F8");

            entity.Property(e => e.JobTitleId).HasColumnName("JobTitle_Id");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.StaffName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnitId).HasColumnName("Unit_Id");

            entity.HasOne(d => d.JobTitle).WithMany(p => p.Staff)
                .HasForeignKey(d => d.JobTitleId)
                .HasConstraintName("FK__Staff__JobTitle___398D8EEE");

            entity.HasOne(d => d.Unit).WithMany(p => p.Staff)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK_Staff_Unit");

            entity.HasMany(d => d.Subjects).WithMany(p => p.Teachers)
                .UsingEntity<Dictionary<string, object>>(
                    "TeachersSubject",
                    r => r.HasOne<SchoolSubject>().WithMany()
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TeachersS__Subje__6754599E"),
                    l => l.HasOne<Staff>().WithMany()
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__TeachersS__Teach__66603565"),
                    j =>
                    {
                        j.HasKey("TeacherId", "SubjectId").HasName("PK__Teachers__EF67B9A0E4E68CDE");
                        j.ToTable("TeachersSubjects");
                        j.IndexerProperty<int>("TeacherId").HasColumnName("Teacher_Id");
                        j.IndexerProperty<int>("SubjectId").HasColumnName("Subject_Id");
                    });
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Student__3214EC0739532AA4");

            entity.ToTable("Student");

            entity.Property(e => e.ClassId).HasColumnName("Class_Id");
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.StudentLname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("StudentLName");
            entity.Property(e => e.StudentName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .HasConstraintName("FK__Student__Class_I__440B1D61");
        });

        modelBuilder.Entity<StudentGrade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StudentG__3214EC0738BA6099");

            entity.ToTable("StudentGrade");

            entity.Property(e => e.CourseId).HasColumnName("Course_Id");
            entity.Property(e => e.GradeScaleId).HasColumnName("GradeScale_Id");
            entity.Property(e => e.StaffId).HasColumnName("Staff_Id");
            entity.Property(e => e.StudentId).HasColumnName("Student_Id");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Grade_Course");

            entity.HasOne(d => d.GradeScale).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.GradeScaleId)
                .HasConstraintName("FK__StudentGr__Grade__49C3F6B7");

            entity.HasOne(d => d.Staff).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.StaffId)
                .HasConstraintName("FK__StudentGr__Staff__48CFD27E");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentGrades)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__StudentGr__Stude__47DBAE45");
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Unit__3214EC07CD4837EC");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
