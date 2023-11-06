using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace polyclinic_service.Appointments.Model;

public class Appointment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required] 
    public DateTime StartDate { get; set; }

    [Required] 
    public DateTime EndDate { get; set; }
}