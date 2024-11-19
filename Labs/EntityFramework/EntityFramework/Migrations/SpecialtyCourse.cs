namespace EntityFramework.Migrations;

public class SpecialtyCourse
{
    public int specialty_course_id { get; set; }
    public int specialty_id { get; set; }
    public int course_id { get; set; }

    public Specialty specialty { get; set; }
    public Course course { get; set; }
}

