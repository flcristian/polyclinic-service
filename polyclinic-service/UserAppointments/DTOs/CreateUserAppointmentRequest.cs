using System.ComponentModel.DataAnnotations;

namespace polyclinic_service.UserAppointments.DTOs;

public class CreateUserAppointmentRequest
{
    [Required]
    public int PatientId { get; set; }
 
    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int AppointmentId { get; set; }
}