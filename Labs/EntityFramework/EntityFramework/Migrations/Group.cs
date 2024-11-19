namespace EntityFramework.Migrations;

public class Group
{
    public int group_id { get; set; }
    public string name { get; set; }

    public ICollection<Enrollment> enrollments { get; set; }
    public ICollection<Student> students { get; set; }
}

