using polyclinic_service.Emails.DTOs;

namespace polyclinic_service.Emails.Services.Interfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(SendEmailRequest request);
}