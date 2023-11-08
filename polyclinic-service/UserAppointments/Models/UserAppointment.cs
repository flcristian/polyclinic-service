using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Users.Models;

namespace polyclinic_service.UserAppointments.Models;

public class UserAppointment
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public int PatientId { get; set; }
    
    public virtual User Patient { get; set; }
    
    [Required]
    public int DoctorId { get; set; }
    
    public virtual User Doctor { get; set; }
    
    [Required]
    public int AppointmentId { get; set; }
    
    public virtual Appointment Appointment { get; set; }
}