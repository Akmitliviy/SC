namespace EntityFramework.Migrations;

public class QRSession
{
    public Guid qr_session_id { get; set; }
    public string qr_code { get; set; }
    public DateOnly expiration_date { get; set; }
    public Guid course_id { get; set; }

    public Course course { get; set; }
}

