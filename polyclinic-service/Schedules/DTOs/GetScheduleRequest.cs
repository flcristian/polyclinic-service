namespace polyclinic_service.Schedules.DTOs;

public class GetScheduleRequest
{
    public int DoctorId { get; set; }
    public int Year { get; set; }
    public int WeekNumber { get; set; }
    public GetScheduleSlotRequest MondaySchedule { get; set; }
    public GetScheduleSlotRequest TuesdaySchedule { get; set; }
    public GetScheduleSlotRequest WednesdaySchedule { get; set; }
    public GetScheduleSlotRequest ThursdaySchedule { get; set; }
    public GetScheduleSlotRequest FridaySchedule { get; set; }
}