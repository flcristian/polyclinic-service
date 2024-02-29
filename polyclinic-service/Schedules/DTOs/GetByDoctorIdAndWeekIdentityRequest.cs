namespace polyclinic_service.Schedules.DTOs;

public class GetByDoctorIdAndWeekIdentityRequest
{
    public int DoctorId { get; set; }
    public int Year { get; set; }
    public int WeekNumber { get; set; }
}