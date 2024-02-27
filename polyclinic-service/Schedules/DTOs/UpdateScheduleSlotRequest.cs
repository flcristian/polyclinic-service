using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.DTOs;

public class UpdateScheduleSlotRequest
{
    public Time StartTime { get; set; }
    public Time EndTime { get; set; }
}