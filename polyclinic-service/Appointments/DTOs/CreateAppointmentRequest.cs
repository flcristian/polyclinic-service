using System.ComponentModel.DataAnnotations;

namespace polyclinic_service.Appointments.DTOs;

public class CreateAppointmentRequest
{
    [Required] 
    public DateTime StartDate { get; set; }

    [Required] 
    public DateTime EndDate { get; set; }

    public override string ToString()
    {
        String message = "";
        message += $"Start date: {StartDate}\n";
        message += $"End date: {EndDate}\n";
        return message;
    }
}