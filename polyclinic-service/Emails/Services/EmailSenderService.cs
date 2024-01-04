using System.Net;
using System.Net.Mail;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Emails.DTOs;
using polyclinic_service.Emails.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;

namespace polyclinic_service.Emails.Services;

public class EmailSenderService : IEmailSenderService
{
    private SmtpClient _client;
    private IAppointmentRepository _appointmentRepository;
    private IUserRepository _userRepository;

    public EmailSenderService(IAppointmentRepository appointmentRepository, IUserRepository userRepository)
    {
        _client = new SmtpClient(Constants.EMAIL_SMTP_SERVER, Constants.EMAIL_SMTP_PORT)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(Constants.EMAIL_SENDER_ADDRESS, Constants.EMAIL_SENDER_PASSWORD)
        };

        _appointmentRepository = appointmentRepository;
        _userRepository = userRepository;
    }
    
    public Task SendEmailAsync(SendEmailRequest request)
    {
        return _client.SendMailAsync(
            new MailMessage(from: Constants.EMAIL_SENDER_ADDRESS,
                to: request.Email, request.Subject, request.Message));
    }

    public async Task SendAppointmentDetailsAsync(SendAppointmentDetailsRequest request)
    {
        string subject = "Appointment Details";

        Appointment appointment = await _appointmentRepository.GetByIdAsync(request.AppointmentId);
        if (appointment == null) throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);

        User user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null) throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);

        string message = await GenerateMessageForAppointmentDetails(appointment, user);

        _client.SendMailAsync(
            new MailMessage(from: Constants.EMAIL_SENDER_ADDRESS,
                to: user.Email, subject, message));
    }

    private async Task<string> GenerateMessageForAppointmentDetails(Appointment appointment, User user)
    {
        string message = "";
        message += $"Appointment with id {appointment.Id}\n";
        message += "==--==--==--==\n";
        message += $"Appointment date : {appointment.StartDate.Date.ToString(Constants.STANDARD_DATE_CALENDAR_DATE_ONLY)}\n";

        TimeSpan duration = appointment.EndDate - appointment.StartDate;
        message += $"Appointment duration : {duration.Hours.ToString("D2")}:{duration.Minutes.ToString("D2")}\n";
        
        message += "\n";
        if (user.Type == UserType.Patient) message += "Doctor information :\n";
        else message += "Patient information :\n";

        int counterpartyId = appointment.UserAppointments.FirstOrDefault(userAppointment => userAppointment.UserId != user.Id).Id;
        User counterparty = await _userRepository.GetByIdAsync(counterpartyId);

        message += $"Full name : {counterparty.Name}\n";
        message += $"Email : {counterparty.Email}\n";
        message += $"Phone number : {counterparty.Phone}\n";

        return message;
    }
}