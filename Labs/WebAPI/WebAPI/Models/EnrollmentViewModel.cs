namespace WebAPI.Models;

public class EnrollmentViewModel
{
    public Guid enrollment_id { get; set; }
    public Guid student_id { get; set; }
    public Guid course_id { get; set; }
    public Guid group_id { get; set; }
}