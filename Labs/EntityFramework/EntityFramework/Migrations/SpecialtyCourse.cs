namespace EntityFramework.Migrations;

public class SpecialtyCourse
{
    public Guid specialty_course_id { get; set; }
    public Guid specialty_id { get; set; }
    public Guid course_id { get; set; }

    public Specialty specialty { get; set; }
    public Course course { get; set; }
}

