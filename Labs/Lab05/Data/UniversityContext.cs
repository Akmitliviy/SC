using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Lab05SC.Models.University;

namespace Lab05SC.Data
{
    public partial class UniversityContext : DbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options) : base(options)
        {
        }

        partial void OnModelBuilding(ModelBuilder builder);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Lab05SC.Models.University.enrollment>()
              .HasOne(i => i.course)
              .WithMany(i => i.enrollments)
              .HasForeignKey(i => i.course_id)
              .HasPrincipalKey(i => i.course_id);

            builder.Entity<Lab05SC.Models.University.enrollment>()
              .HasOne(i => i.group1)
              .WithMany(i => i.enrollments)
              .HasForeignKey(i => i.group_id)
              .HasPrincipalKey(i => i.group_id);

            builder.Entity<Lab05SC.Models.University.enrollment>()
              .HasOne(i => i.student)
              .WithMany(i => i.enrollments)
              .HasForeignKey(i => i.student_id)
              .HasPrincipalKey(i => i.student_id);

            builder.Entity<Lab05SC.Models.University.qr_session>()
              .HasOne(i => i.course)
              .WithMany(i => i.qr_sessions)
              .HasForeignKey(i => i.course_id)
              .HasPrincipalKey(i => i.course_id);

            builder.Entity<Lab05SC.Models.University.specialty_course>()
              .HasOne(i => i.course)
              .WithMany(i => i.specialty_courses)
              .HasForeignKey(i => i.course_id)
              .HasPrincipalKey(i => i.course_id);

            builder.Entity<Lab05SC.Models.University.specialty_course>()
              .HasOne(i => i.specialty)
              .WithMany(i => i.specialty_courses)
              .HasForeignKey(i => i.specialty_id)
              .HasPrincipalKey(i => i.specialty_id);

            builder.Entity<Lab05SC.Models.University.student>()
              .HasOne(i => i.group1)
              .WithMany(i => i.students)
              .HasForeignKey(i => i.group_id)
              .HasPrincipalKey(i => i.group_id);
            this.OnModelBuilding(builder);
        }

        public DbSet<Lab05SC.Models.University.academic_year_plan> academic_year_plans { get; set; }

        public DbSet<Lab05SC.Models.University.course> courses { get; set; }

        public DbSet<Lab05SC.Models.University.enrollment> enrollments { get; set; }

        public DbSet<Lab05SC.Models.University.group> groups { get; set; }

        public DbSet<Lab05SC.Models.University.qr_session> qr_sessions { get; set; }

        public DbSet<Lab05SC.Models.University.specialty> specialties { get; set; }

        public DbSet<Lab05SC.Models.University.specialty_course> specialty_courses { get; set; }

        public DbSet<Lab05SC.Models.University.student> students { get; set; }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }
    }
}