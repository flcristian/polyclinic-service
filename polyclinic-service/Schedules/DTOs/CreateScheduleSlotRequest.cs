namespace polyclinic_service.Schedules.DTOs;

public class CreateScheduleSlotRequest
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}