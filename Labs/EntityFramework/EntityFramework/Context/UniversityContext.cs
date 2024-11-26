using EntityFramework.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Context;

public sealed partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
        Database.EnsureCreated();
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<Student> students { get; set; }
    public DbSet<Course> courses { get; set; }
    public DbSet<Group> groups { get; set; }
    public DbSet<Specialty> specialties { get; set; }
    public DbSet<Enrollment> enrollments { get; set; }
    public DbSet<SpecialtyCourse> specialty_courses { get; set; }
    public DbSet<AcademicYearPlan> academic_year_plans { get; set; }
    public DbSet<QRSession> qr_sessions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=University;Username=postgres;Password=baest4rd");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");
        
        // Налаштування Student
        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("students");
            entity.HasKey(s => s.student_id);
            entity.Property(s => s.student_id).HasColumnName("student_id").ValueGeneratedNever();
            entity.Property(s => s.first_name).HasColumnName("first_name").IsRequired().HasMaxLength(100);
            entity.Property(s => s.last_name).HasColumnName("last_name").IsRequired().HasMaxLength(100);
            entity.Property(s => s.birth_date).HasColumnName("birth_date").IsRequired();
            entity.Property(s => s.email).HasColumnName("email").IsRequired().HasMaxLength(200);
            entity.Property(s => s.phone_number).HasColumnName("phone_number").IsRequired().HasMaxLength(15);
            entity.Property(s => s.group_id).HasColumnName("group_id").IsRequired();
            
            entity.HasOne(s => s.group).WithMany(g => g.students).HasForeignKey(s => s.group_id);
        });

        // Налаштування Course
        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("courses");
            entity.HasKey(c => c.course_id);
            entity.Property(c => c.course_id).HasColumnName("course_id").ValueGeneratedNever();
            entity.Property(c => c.name).HasColumnName("name").IsRequired().HasMaxLength(200);
            entity.Property(c => c.description).HasColumnName("description").HasMaxLength(500);
        });

        // Налаштування Group
        modelBuilder.Entity<Group>(entity =>
        {
            entity.ToTable("groups");
            entity.HasKey(g => g.group_id);
            entity.Property(g => g.group_id).HasColumnName("group_id").ValueGeneratedNever();
            entity.Property(g => g.name).HasColumnName("name").IsRequired().HasMaxLength(100);
        });

        // Налаштування Specialty
        modelBuilder.Entity<Specialty>(entity =>
        {
            entity.ToTable("specialties");
            entity.HasKey(sp => sp.specialty_id);
            entity.Property(sp => sp.specialty_id).HasColumnName("specialty_id").ValueGeneratedNever();
            entity.Property(sp => sp.name).HasColumnName("name").IsRequired().HasMaxLength(200);
        });

        // Налаштування Enrollment
        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.ToTable("enrollments");
            entity.HasKey(e => e.enrollment_id);
            entity.Property(e => e.enrollment_id).HasColumnName("enrollment_id").ValueGeneratedNever();
            entity.Property(e => e.student_id).HasColumnName("student_id");
            entity.Property(e => e.course_id).HasColumnName("course_id");
            entity.Property(e => e.group_id).HasColumnName("group_id");

            entity.HasOne(e => e.student).WithMany(s => s.enrollments).HasForeignKey(e => e.student_id);
            entity.HasOne(e => e.course).WithMany(c => c.enrollments).HasForeignKey(e => e.course_id);
            entity.HasOne(e => e.group).WithMany(g => g.enrollments).HasForeignKey(e => e.group_id);
        });

        // Налаштування SpecialtyCourse
        modelBuilder.Entity<SpecialtyCourse>(entity =>
        {
            entity.ToTable("specialty_courses");
            entity.HasKey(sc => sc.specialty_course_id);
            entity.Property(sc => sc.specialty_course_id).HasColumnName("specialty_course_id").ValueGeneratedNever();
            entity.Property(sc => sc.specialty_id).HasColumnName("specialty_id");
            entity.Property(sc => sc.course_id).HasColumnName("course_id");

            entity.HasOne(sc => sc.specialty).WithMany(s => s.specialty_courses).HasForeignKey(sc => sc.specialty_id);
            entity.HasOne(sc => sc.course).WithMany(c => c.specialty_courses).HasForeignKey(sc => sc.course_id);
        });

        // Налаштування AcademicYearPlan
        modelBuilder.Entity<AcademicYearPlan>(entity =>
        {
            entity.ToTable("academic_year_plans");
            entity.HasKey(ayp => ayp.year_plan_id);
            entity.Property(ayp => ayp.year_plan_id).HasColumnName("year_plan_id").ValueGeneratedNever();
            entity.Property(ayp => ayp.academic_year).HasColumnName("academic_year").IsRequired();
        });

        // Налаштування QRSession
        modelBuilder.Entity<QRSession>(entity =>
        {
            entity.ToTable("qr_sessions");
            entity.HasKey(qr => qr.qr_session_id);
            entity.Property(qr => qr.qr_session_id).HasColumnName("qr_session_id").ValueGeneratedNever();
            entity.Property(qr => qr.qr_code).HasColumnName("qr_code").IsRequired();
            entity.Property(qr => qr.expiration_date).HasColumnName("expiration_date").IsRequired();
            entity.Property(qr => qr.course_id).HasColumnName("course_id");

            entity.HasOne(qr => qr.course).WithMany().HasForeignKey(qr => qr.course_id);
        });
    }
}