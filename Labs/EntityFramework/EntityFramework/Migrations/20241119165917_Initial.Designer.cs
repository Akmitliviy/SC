﻿// <auto-generated />
using System;
using EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EntityFramework.Migrations
{
    [DbContext(typeof(UniversityContext))]
    [Migration("20241119165917_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EntityFramework.Migrations.AcademicYearPlan", b =>
                {
                    b.Property<int>("year_plan_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("year_plan_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("year_plan_id"));

                    b.Property<int>("academic_year")
                        .HasColumnType("integer")
                        .HasColumnName("academic_year");

                    b.HasKey("year_plan_id");

                    b.ToTable("academic_year_plans", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Course", b =>
                {
                    b.Property<int>("course_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("course_id"));

                    b.Property<int?>("AcademicYearPlanyear_plan_id")
                        .HasColumnType("integer");

                    b.Property<string>("description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("description");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("course_id");

                    b.HasIndex("AcademicYearPlanyear_plan_id");

                    b.ToTable("courses", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Enrollment", b =>
                {
                    b.Property<int>("enrollment_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("enrollment_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("enrollment_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<int>("group_id")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<int>("student_id")
                        .HasColumnType("integer")
                        .HasColumnName("student_id");

                    b.HasKey("enrollment_id");

                    b.HasIndex("course_id");

                    b.HasIndex("group_id");

                    b.HasIndex("student_id");

                    b.ToTable("enrollments", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Group", b =>
                {
                    b.Property<int>("group_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("group_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.HasKey("group_id");

                    b.ToTable("groups", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.QRSession", b =>
                {
                    b.Property<int>("qr_session_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("qr_session_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("qr_session_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<DateTime>("expiration_time")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_time");

                    b.Property<string>("qr_code")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("qr_code");

                    b.HasKey("qr_session_id");

                    b.HasIndex("course_id");

                    b.ToTable("qr_sessions", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Specialty", b =>
                {
                    b.Property<int>("specialty_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("specialty_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("specialty_id"));

                    b.Property<string>("name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("name");

                    b.HasKey("specialty_id");

                    b.ToTable("specialties", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.SpecialtyCourse", b =>
                {
                    b.Property<int>("specialty_course_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("specialty_course_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("specialty_course_id"));

                    b.Property<int>("course_id")
                        .HasColumnType("integer")
                        .HasColumnName("course_id");

                    b.Property<int>("specialty_id")
                        .HasColumnType("integer")
                        .HasColumnName("specialty_id");

                    b.HasKey("specialty_course_id");

                    b.HasIndex("course_id");

                    b.HasIndex("specialty_id");

                    b.ToTable("specialty_courses", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Student", b =>
                {
                    b.Property<int>("student_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("student_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("student_id"));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)")
                        .HasColumnName("email");

                    b.Property<string>("first_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("first_name");

                    b.Property<int?>("group_id")
                        .HasColumnType("integer");

                    b.Property<string>("last_name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("last_name");

                    b.HasKey("student_id");

                    b.HasIndex("group_id");

                    b.ToTable("students", (string)null);
                });

            modelBuilder.Entity("EntityFramework.Migrations.Course", b =>
                {
                    b.HasOne("EntityFramework.Migrations.AcademicYearPlan", null)
                        .WithMany("courses")
                        .HasForeignKey("AcademicYearPlanyear_plan_id");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Enrollment", b =>
                {
                    b.HasOne("EntityFramework.Migrations.Course", "course")
                        .WithMany("enrollments")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Migrations.Group", "group")
                        .WithMany("enrollments")
                        .HasForeignKey("group_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Migrations.Student", "student")
                        .WithMany("enrollments")
                        .HasForeignKey("student_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("group");

                    b.Navigation("student");
                });

            modelBuilder.Entity("EntityFramework.Migrations.QRSession", b =>
                {
                    b.HasOne("EntityFramework.Migrations.Course", "course")
                        .WithMany()
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");
                });

            modelBuilder.Entity("EntityFramework.Migrations.SpecialtyCourse", b =>
                {
                    b.HasOne("EntityFramework.Migrations.Course", "course")
                        .WithMany("specialty_courses")
                        .HasForeignKey("course_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EntityFramework.Migrations.Specialty", "specialty")
                        .WithMany("specialty_courses")
                        .HasForeignKey("specialty_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("course");

                    b.Navigation("specialty");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Student", b =>
                {
                    b.HasOne("EntityFramework.Migrations.Group", null)
                        .WithMany("students")
                        .HasForeignKey("group_id");
                });

            modelBuilder.Entity("EntityFramework.Migrations.AcademicYearPlan", b =>
                {
                    b.Navigation("courses");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Course", b =>
                {
                    b.Navigation("enrollments");

                    b.Navigation("specialty_courses");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Group", b =>
                {
                    b.Navigation("enrollments");

                    b.Navigation("students");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Specialty", b =>
                {
                    b.Navigation("specialty_courses");
                });

            modelBuilder.Entity("EntityFramework.Migrations.Student", b =>
                {
                    b.Navigation("enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}
