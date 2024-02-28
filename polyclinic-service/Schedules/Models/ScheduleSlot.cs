using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace polyclinic_service.Schedules.Models;

public class ScheduleSlot
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonIgnore]
    public int Id { get; set; }
    
    [Required]
    public String StartTime { get; set; }
    
    [Required]
    public String EndTime { get; set; }
}