namespace EntityFramework.Migrations;

public class Course
{
    public Guid course_id { get; set; }
    public string name { get; set; }
    public string description { get; set; }

    public ICollection<SpecialtyCourse> specialty_courses { get; set; }
    public ICollection<Enrollment> enrollments { get; set; }
}

