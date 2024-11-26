namespace WebAPI.Models;

public class QRSessionViewModel
{
    public Guid qr_session_id { get; set; }
    public string qr_code { get; set; }
    public DateOnly expiration_time { get; set; }
    public Guid course_id { get; set; }
}