namespace WebAPI.Models;

public class StudentViewModel
{
    public Guid student_id { get; set; }
    public string full_name { get; set; } // Поєднує first_name та last_name
    public string email { get; set; }
    public string phone_number { get; set; }
    public Guid? group_id { get; set; } // Ідентифікатор групи студента
}