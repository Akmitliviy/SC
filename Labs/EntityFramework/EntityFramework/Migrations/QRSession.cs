namespace EntityFramework.Migrations;

public class QRSession
{
    public int qr_session_id { get; set; }
    public string qr_code { get; set; }
    public DateTime expiration_time { get; set; }
    public int course_id { get; set; }

    public Course course { get; set; }
}

