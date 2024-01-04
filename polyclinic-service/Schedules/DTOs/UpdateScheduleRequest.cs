namespace polyclinic_service.Schedules.DTOs;

public class UpdateScheduleRequest
{
    public int DoctorId { get; set; }
    public UpdateScheduleSlotRequest MondaySchedule { get; set; }
    public UpdateScheduleSlotRequest TuesdaySchedule { get; set; }
    public UpdateScheduleSlotRequest WednesdaySchedule { get; set; }
    public UpdateScheduleSlotRequest ThursdaySchedule { get; set; }
    public UpdateScheduleSlotRequest FridaySchedule { get; set; }
}