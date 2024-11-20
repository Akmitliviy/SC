namespace EntityFramework.Migrations;

public class Student
{
    public Guid student_id { get; set; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { get; set; }
    public string phone_number { get; set; }

    public ICollection<Enrollment> enrollments { get; set; }
}

