using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Schedules.Models;

public class Schedule
{
    [Key]
    public int DoctorId { get; set; }
    
    [Required]
    public int Year { get; set; }

    [Required]
    public int WeekNumber { get; set; }
    
    [Required]
    [JsonIgnore]
    public int MondayScheduleId { get; set; }
    
    public virtual ScheduleSlot MondaySchedule { get; set; }
    
    [Required]
    [JsonIgnore]
    public int TuesdayScheduleId { get; set; }
    
    public virtual ScheduleSlot TuesdaySchedule { get; set; }
    
    [Required]
    [JsonIgnore]
    public int WednesdayScheduleId { get; set; }
    
    public virtual ScheduleSlot WednesdaySchedule { get; set; }
    
    [Required]
    [JsonIgnore]
    public int ThursdayScheduleId { get; set; }
    
    public virtual ScheduleSlot ThursdaySchedule { get; set; }
    
    [Required]
    [JsonIgnore]
    public int FridayScheduleId { get; set; }
    
    public virtual ScheduleSlot FridaySchedule { get; set; }
    
    [JsonIgnore]
    public virtual User Doctor { get; set; }
}