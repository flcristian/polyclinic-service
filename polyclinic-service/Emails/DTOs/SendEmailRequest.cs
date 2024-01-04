namespace polyclinic_service.Emails.DTOs;

public class SendEmailRequest
{
    public string Email { get; set; }
    public string Subject { get; set; }
    public string Message { get; set; }
}