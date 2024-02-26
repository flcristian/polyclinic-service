namespace polyclinic_service.Schedules.DTOs;

public class UpdateScheduleSlotRequest
{
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
}