using System.ComponentModel.DataAnnotations;

namespace polyclinic_service.UserAppointments.DTOs;

public class UpdateUserAppointmentRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public int PatientId { get; set; }
 
    [Required]
    public int DoctorId { get; set; }

    [Required]
    public int AppointmentId { get; set; }
}