namespace EntityFramework.Migrations;

public class Specialty
{
    public int specialty_id { get; set; }
    public string name { get; set; }

    public ICollection<SpecialtyCourse> specialty_courses { get; set; }
}

