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
    
    public virtual User Patient { get; set; }
    
    public virtual User Doctor { get; set; }
    
    public virtual Appointment Appointment { get; set; }
}