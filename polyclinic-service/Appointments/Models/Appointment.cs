using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.Appointments.Models;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public DateTime StartDate { get; set; }

    [Required] 
    public DateTime EndDate { get; set; }

    public virtual List<UserAppointment> UserAppointments { get; set; }
}