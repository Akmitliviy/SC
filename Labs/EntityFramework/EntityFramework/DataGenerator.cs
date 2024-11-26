using Bogus;
using EntityFramework.Migrations;

namespace UniversityDataGenerator
{
    public class DataGenerator
    {
        private List<string> FirstNames { get; set; }
        private List<string> LastNames { get; set; }
        private List<string> Emails { get; set; }
        private List<string> PhoneNumbers { get; set; }
        private List<DateOnly> BirthDates { get; set; }
        private List<string> GroupNames { get; set; }
        private List<string> SpecialtiesNames { get; set; }
        private List<string> CourseTitles { get; set; }
        private List<int> AcademicYears { get; set; }
        private List<string> QRSessionDescriptions { get; set; }
        private List<Student> Students { get; set; }
        private List<Course> Courses { get; set; }
        private List<Group> Groups { get; set; }
        private List<Specialty> Specialties { get; set; }

        public DataGenerator()
        {
            var faker = new Faker();

            FirstNames = GenerateList(() => faker.Name.FirstName());
            LastNames = GenerateList(() => faker.Name.LastName());
            Emails = GenerateList(() => faker.Internet.Email());
            PhoneNumbers = GenerateList(() => faker.Phone.PhoneNumber("###-###-####"));
            BirthDates = GenerateList(() => DateOnly.FromDateTime(faker.Date.Past(30, DateTime.Now.AddYears(-18))));
            GroupNames = GenerateList(() => faker.Random.AlphaNumeric(5));
            SpecialtiesNames = GenerateList(() => faker.Name.JobArea());
            CourseTitles = GenerateList(() => faker.Company.CatchPhrase());
            AcademicYears = GenerateList(() => faker.Random.Int(1, 5));
            QRSessionDescriptions = GenerateList(() => faker.Lorem.Sentence());
        }

        private static List<T> GenerateList<T>(Func<T> generator, int count = 100) =>
            Enumerable.Range(1, count).Select(_ => generator()).ToList();

        public List<Student> GenerateStudents(int count)
        {
            var students = new List<Student>();
            for (int i = 0; i < count; i++)
            {
                students.Add(new Student
                {
                    student_id = Guid.NewGuid(),
                    first_name = FirstNames[i % FirstNames.Count],
                    last_name = LastNames[i % LastNames.Count],
                    email = Emails[i % Emails.Count],
                    birth_date = BirthDates[i % BirthDates.Count],
                    phone_number = PhoneNumbers[i % PhoneNumbers.Count],
                    group_id = Groups[i % Groups.Count].group_id,
                });
            }
            Students = students;
            return students;
        }

        public List<Group> GenerateGroups(int count)
        {
            var groups = new List<Group>();
            for (int i = 0; i < count; i++)
            {
                groups.Add(new Group
                {
                    group_id = Guid.NewGuid(), // Example specialty assignment
                    name = GroupNames[i % GroupNames.Count]
                });
            }
            Groups = groups;
            return groups;
        }

        public List<Specialty> GenerateSpecialties(int count)
        {
            var specialties = new List<Specialty>();
            for (int i = 0; i < count; i++)
            {
                specialties.Add(new Specialty
                {
                    specialty_id = Guid.NewGuid(),
                    name = SpecialtiesNames[i % SpecialtiesNames.Count]
                });
            }
            Specialties = specialties;
            return specialties;
        }

        public List<Course> GenerateCourses(int count)
        {
            var courses = new List<Course>();
            for (int i = 0; i < count; i++)
            {
                courses.Add(new Course
                {
                    course_id = Guid.NewGuid(),
                    name = CourseTitles[i % CourseTitles.Count],
                    description = QRSessionDescriptions[i % QRSessionDescriptions.Count]
                });
            }
            Courses = courses;
            return courses;
        }

        public List<AcademicYearPlan> GenerateAcademicYearPlans(int count)
        {
            var plans = new List<AcademicYearPlan>();
            for (int i = 0; i < count; i++)
            {
                plans.Add(new AcademicYearPlan
                {
                    year_plan_id = Guid.NewGuid(),
                    academic_year = AcademicYears[i % AcademicYears.Count]
                });
            }
            return plans;
        }

        public List<QRSession> GenerateQRSessions(int count)
        {
            var qrSessions = new List<QRSession>();
            for (int i = 0; i < count; i++)
            {
                var guid = Guid.NewGuid();
                qrSessions.Add(new QRSession
                {
                    qr_session_id = guid,
                    qr_code = guid.ToString(),
                    expiration_date = DateOnly.FromDateTime(DateTime.Now.AddMinutes(new Faker().Random.Int(10, 100))),
                    course_id = Courses[i % Courses.Count].course_id,
                });
            }
            return qrSessions;
        }

        public List<SpecialtyCourse> GenerateSpecialtyCourses(int count)
        {
            var specialtyCourses = new List<SpecialtyCourse>();
            for (int i = 0; i < count; i++)
            {
                specialtyCourses.Add(new SpecialtyCourse
                {
                    specialty_course_id = Guid.NewGuid(),
                    specialty_id = Specialties[i % Specialties.Count].specialty_id,
                    course_id = Courses[i % Courses.Count].course_id,
                });
            }
            return specialtyCourses;
        }

        public List<Enrollment> GenerateEnrollments(int count)
        {
            var enrollments = new List<Enrollment>();
            for (int i = 0; i < count; i++)
            {
                enrollments.Add(new Enrollment
                {
                    enrollment_id = Guid.NewGuid(),
                    student_id = Students[i].student_id,
                    course_id = Courses[i % Courses.Count].course_id,
                    group_id = Groups[i % Groups.Count].group_id
                });
            }
            return enrollments;
        }
        

        private static T GetRandomElementOf<T>(List<T> list)
        {
            var random = new Random();
            return list.ElementAt(random.Next(list.Count));
        }
    }
}
