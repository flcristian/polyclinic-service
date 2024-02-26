namespace polyclinic_service.Schedules.DTOs;

public class CreateScheduleSlotRequest
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}