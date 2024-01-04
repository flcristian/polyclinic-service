namespace polyclinic_service.Emails.DTOs;

public class SendAppointmentDetailsRequest
{
     public int UserId { get; set; }
     public int AppointmentId { get; set; }
}