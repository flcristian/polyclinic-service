using System.ComponentModel.DataAnnotations;

namespace polyclinic_service.Appointments.DTOs;

public class UpdateAppointmentRequest
{
    [Required] 
    public DateTime StartDate { get; set; }

    [Required] 
    public DateTime EndDate { get; set; }
}