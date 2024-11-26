namespace EntityFramework.Migrations;

public class Enrollment
{
    public Guid enrollment_id { get; set; }
    public Guid student_id { get; set; }
    public Guid course_id { get; set; }
    public Guid group_id { get; set; }

    public Student student { get; set; }
    public Course course { get; set; }
    public Group group { get; set; }
}

