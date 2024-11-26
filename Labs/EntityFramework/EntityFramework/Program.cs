using Bogus;
using EntityFramework.Context;
using EntityFramework.Migrations;
using UniversityDataGenerator;

namespace EntityFramework;

public class Program
{
    public static void Main(string[] args)
    {
        var context = new UniversityContext();
        //PopulateDatabase(context);

    }

    private static void PopulateDatabase(UniversityContext context)
    {
        var generator = new DataGenerator();

        context.courses.AddRange(generator.GenerateCourses(100));
        context.SaveChanges();

        context.groups.AddRange(generator.GenerateGroups(200));
        context.SaveChanges();
        
        context.students.AddRange(generator.GenerateStudents(2000));
        context.SaveChanges();
        
        context.specialties.AddRange(generator.GenerateSpecialties(20));
        context.SaveChanges();

        context.specialty_courses.AddRange(generator.GenerateSpecialtyCourses(100));
        context.SaveChanges();

        context.academic_year_plans.AddRange(generator.GenerateAcademicYearPlans(5));
        context.SaveChanges();

        context.qr_sessions.AddRange(generator.GenerateQRSessions(100));
        context.SaveChanges();

        context.enrollments.AddRange(generator.GenerateEnrollments(2000));
        context.SaveChanges();
    }
}