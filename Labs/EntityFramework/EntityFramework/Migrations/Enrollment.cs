namespace EntityFramework.Migrations;

public class Enrollment
{
    public int enrollment_id { get; set; }
    public int student_id { get; set; }
    public int course_id { get; set; }
    public int group_id { get; set; }

    public Student student { get; set; }
    public Course course { get; set; }
    public Group group { get; set; }
}

