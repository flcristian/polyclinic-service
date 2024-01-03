﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Schedules.Models;

public class Schedule
{
    [Key]
    public int DoctorId { get; set; }
    
    [Required]
    public int MondayScheduleId { get; set; }
    
    public virtual ScheduleSlot MondaySchedule { get; set; }
    
    [Required]
    public int TuesdayScheduleId { get; set; }
    
    public virtual ScheduleSlot TuesdaySchedule { get; set; }
    
    [Required]
    public int WednesdayScheduleId { get; set; }
    
    public virtual ScheduleSlot WednesdaySchedule { get; set; }
    
    [Required]
    public int ThursdayScheduleId { get; set; }
    
    public virtual ScheduleSlot ThursdaySchedule { get; set; }
    
    [Required]
    public int FridayScheduleId { get; set; }
    
    public virtual ScheduleSlot FridaySchedule { get; set; }
    
    [JsonIgnore]
    public virtual User Doctor { get; set; }
}