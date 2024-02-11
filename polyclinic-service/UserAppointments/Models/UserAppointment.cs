using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Users.Models;

namespace polyclinic_service.UserAppointments.Models;

public class UserAppointment
{
    [Key] 
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [ForeignKey("UserId")]
    public int UserId { get; set; }
    
    [JsonIgnore]
    public virtual User User { get; set; }
    
    [ForeignKey("AppointmentId")]
    public int AppointmentId { get; set; }

    [JsonIgnore]
    public virtual Appointment Appointment { get; set; }
}