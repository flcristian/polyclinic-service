using System.Net;
using System.Net.Mail;
using polyclinic_service.Emails.DTOs;
using polyclinic_service.Emails.Services.Interfaces;
using polyclinic_service.System.Constants;

namespace polyclinic_service.Emails.Services;

public class EmailSenderService : IEmailSenderService
{
    public Task SendEmailAsync(SendEmailRequest request)
    {
        SmtpClient client = new SmtpClient(Constants.EMAIL_SMTP_SERVER, Constants.EMAIL_SMTP_PORT)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(Constants.EMAIL_SENDER_ADDRESS, Constants.EMAIL_SENDER_PASSWORD)
        };

        return client.SendMailAsync(
            new MailMessage(from: Constants.EMAIL_SENDER_ADDRESS,
                to: request.Email, request.Subject, request.Message));
    }
}