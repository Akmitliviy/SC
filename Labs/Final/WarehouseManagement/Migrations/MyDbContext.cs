using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WarehouseManagement.Migrations;

namespace WarehouseManagement.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<academic_year_plan> academic_year_plans { get; set; }

    public virtual DbSet<course> courses { get; set; }

    public virtual DbSet<enrollment> enrollments { get; set; }

    public virtual DbSet<group> groups { get; set; }

    public virtual DbSet<qr_session> qr_sessions { get; set; }

    public virtual DbSet<specialty> specialties { get; set; }

    public virtual DbSet<specialty_course> specialty_courses { get; set; }

    public virtual DbSet<student> students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=University;Username=postgres;Password=baest4rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<academic_year_plan>(entity =>
        {
            entity.HasKey(e => e.year_plan_id);

            entity.Property(e => e.year_plan_id).ValueGeneratedNever();
        });

        modelBuilder.Entity<course>(entity =>
        {
            entity.HasKey(e => e.course_id);

            entity.Property(e => e.course_id).ValueGeneratedNever();
            entity.Property(e => e.description).HasMaxLength(500);
            entity.Property(e => e.name).HasMaxLength(200);
        });

        modelBuilder.Entity<enrollment>(entity =>
        {
            entity.HasKey(e => e.enrollment_id);

            entity.HasIndex(e => e.course_id, "IX_enrollments_course_id");

            entity.HasIndex(e => e.group_id, "IX_enrollments_group_id");

            entity.HasIndex(e => e.student_id, "IX_enrollments_student_id");

            entity.Property(e => e.enrollment_id).ValueGeneratedNever();

            entity.HasOne(d => d.course).WithMany(p => p.enrollments).HasForeignKey(d => d.course_id);

            entity.HasOne(d => d.group).WithMany(p => p.enrollments).HasForeignKey(d => d.group_id);

            entity.HasOne(d => d.student).WithMany(p => p.enrollments).HasForeignKey(d => d.student_id);
        });

        modelBuilder.Entity<group>(entity =>
        {
            entity.HasKey(e => e.group_id);

            entity.Property(e => e.group_id).ValueGeneratedNever();
            entity.Property(e => e.name).HasMaxLength(100);
        });

        modelBuilder.Entity<qr_session>(entity =>
        {
            entity.HasKey(e => e.qr_session_id);

            entity.HasIndex(e => e.course_id, "IX_qr_sessions_course_id");

            entity.Property(e => e.qr_session_id).ValueGeneratedNever();

            entity.HasOne(d => d.course).WithMany(p => p.qr_sessions).HasForeignKey(d => d.course_id);
        });

        modelBuilder.Entity<specialty>(entity =>
        {
            entity.HasKey(e => e.specialty_id);

            entity.Property(e => e.specialty_id).ValueGeneratedNever();
            entity.Property(e => e.name).HasMaxLength(200);
        });

        modelBuilder.Entity<specialty_course>(entity =>
        {
            entity.HasKey(e => e.specialty_course_id);

            entity.HasIndex(e => e.course_id, "IX_specialty_courses_course_id");

            entity.HasIndex(e => e.specialty_id, "IX_specialty_courses_specialty_id");

            entity.Property(e => e.specialty_course_id).ValueGeneratedNever();

            entity.HasOne(d => d.course).WithMany(p => p.specialty_courses).HasForeignKey(d => d.course_id);

            entity.HasOne(d => d.specialty).WithMany(p => p.specialty_courses).HasForeignKey(d => d.specialty_id);
        });

        modelBuilder.Entity<student>(entity =>
        {
            entity.HasKey(e => e.student_id);

            entity.HasIndex(e => e.group_id, "IX_students_group_id");

            entity.Property(e => e.student_id).ValueGeneratedNever();
            entity.Property(e => e.email).HasMaxLength(200);
            entity.Property(e => e.first_name).HasMaxLength(100);
            entity.Property(e => e.last_name).HasMaxLength(100);
            entity.Property(e => e.phone_number).HasMaxLength(15);

            entity.HasOne(d => d.group).WithMany(p => p.students).HasForeignKey(d => d.group_id);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
