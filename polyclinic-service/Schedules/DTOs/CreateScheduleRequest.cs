using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.DTOs;

public class CreateScheduleRequest
{
    public int DoctorId { get; set; }
    public int Year { get; set; }
    public int WeekNumber { get; set; }
    public CreateScheduleSlotRequest MondaySchedule { get; set; }
    public CreateScheduleSlotRequest TuesdaySchedule { get; set; }
    public CreateScheduleSlotRequest WednesdaySchedule { get; set; }
    public CreateScheduleSlotRequest ThursdaySchedule { get; set; }
    public CreateScheduleSlotRequest FridaySchedule { get; set; }
}