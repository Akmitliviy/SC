﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScaffoldEF.Context;

#nullable disable

namespace ScaffoldEF.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ScaffoldEF.Migrations.academic_year_plan", b =>
                {
                    b.Property<int>("year_plan_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("year_plan_id"));

                    b.Property<int>("academic_year")
                        .HasColumnType("integer");

                    b.HasKey("year_plan_id");

                    b.ToTable("academic_year_plans");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.course", b =>
                {
                    b.Property<int>("course_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("course_id"));

                    b.Property<int?>("AcademicYearPlanyear_plan_id")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("course_id");

                    b.HasIndex(new[] { "AcademicYearPlanyear_plan_id" }, "IX_courses_AcademicYearPlanyear_plan_id");

                    b.ToTable("courses");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.enrollment", b =>
                {
                    b.Property<int>("enrollment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("enrollment_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer");

                    b.Property<int>("group_id")
                        .HasColumnType("integer");

                    b.Property<int>("student_id")
                        .HasColumnType("integer");

                    b.HasKey("enrollment_id");

                    b.HasIndex(new[] { "course_id" }, "IX_enrollments_course_id");

                    b.HasIndex(new[] { "group_id" }, "IX_enrollments_group_id");

                    b.HasIndex(new[] { "student_id" }, "IX_enrollments_student_id");

                    b.ToTable("enrollments");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.group", b =>
                {
                    b.Property<int>("group_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("group_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("group_id");

                    b.ToTable("groups");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.qr_session", b =>
                {
                    b.Property<int>("qr_session_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("qr_session_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer");

                    b.Property<DateTime>("expiration_time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("qr_code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("qr_session_id");

                    b.HasIndex(new[] { "course_id" }, "IX_qr_sessions_course_id");

                    b.ToTable("qr_sessions");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.specialty", b =>
                {
                    b.Property<int>("specialty_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("specialty_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.HasKey("specialty_id");

                    b.ToTable("specialties");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.specialty_course", b =>
                {
                    b.Property<int>("specialty_course_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("specialty_course_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer");

                    b.Property<int>("specialty_id")
                        .HasColumnType("integer");

                    b.HasKey("specialty_course_id");

                    b.HasIndex(new[] { "course_id" }, "IX_specialty_courses_course_id");

                    b.HasIndex(new[] { "specialty_id" }, "IX_specialty_courses_specialty_id");

                    b.ToTable("specialty_courses");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.student", b =>
                {
                    b.Property<int>("student_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("student_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int?>("group_id")
                        .HasColumnType("integer");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("student_id");

                    b.HasIndex(new[] { "group_id" }, "IX_students_group_id");

                    b.ToTable("students");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.course", b =>
                {
                    b.HasOne("ScaffoldEF.Migrations.academic_year_plan", "AcademicYearPlanyear_plan")
                        .WithMany("courses")
                        .HasForeignKey("AcademicYearPlanyear_plan_id");

                    b.Navigation("AcademicYearPlanyear_plan");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.enrollment", b =>
                {
                    b.HasOne("ScaffoldEF.Migrations.course", "course")
                        .WithMany("enrollments")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScaffoldEF.Migrations.group", "group")
                        .WithMany("enrollments")
                        .HasForeignKey("group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScaffoldEF.Migrations.student", "student")
                        .WithMany("enrollments")
                        .HasForeignKey("student_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("group");

                    b.Navigation("student");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.qr_session", b =>
                {
                    b.HasOne("ScaffoldEF.Migrations.course", "course")
                        .WithMany("qr_sessions")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.specialty_course", b =>
                {
                    b.HasOne("ScaffoldEF.Migrations.course", "course")
                        .WithMany("specialty_courses")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ScaffoldEF.Migrations.specialty", "specialty")
                        .WithMany("specialty_courses")
                        .HasForeignKey("specialty_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("specialty");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.student", b =>
                {
                    b.HasOne("ScaffoldEF.Migrations.group", "group")
                        .WithMany("students")
                        .HasForeignKey("group_id");

                    b.Navigation("group");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.academic_year_plan", b =>
                {
                    b.Navigation("courses");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.course", b =>
                {
                    b.Navigation("enrollments");

                    b.Navigation("qr_sessions");

                    b.Navigation("specialty_courses");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.group", b =>
                {
                    b.Navigation("enrollments");

                    b.Navigation("students");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.specialty", b =>
                {
                    b.Navigation("specialty_courses");
                });

            modelBuilder.Entity("ScaffoldEF.Migrations.student", b =>
                {
                    b.Navigation("enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
