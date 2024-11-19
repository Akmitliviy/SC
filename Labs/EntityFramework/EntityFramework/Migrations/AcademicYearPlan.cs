namespace EntityFramework.Migrations;

public class AcademicYearPlan
{
    public int year_plan_id { get; set; }
    public int academic_year { get; set; }

    public ICollection<Course> courses { get; set; }
}
