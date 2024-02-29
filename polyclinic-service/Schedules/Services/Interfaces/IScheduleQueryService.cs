using polyclinic_service.Schedules.DTOs;
namespace polyclinic_service.Schedules.Services.Interfaces;

public interface IScheduleQueryService
{
    Task<IEnumerable<GetScheduleRequest>> GetAllSchedules();
    Task<IEnumerable<GetScheduleRequest>> GetSchedulesByDoctorId(int doctorId);
    Task<GetScheduleRequest> GetScheduleByDoctorIdAndWeekIdentity(GetByDoctorIdAndWeekIdentityRequest scheduleRequest);

    Task<bool> CheckIfAppointmentInDoctorSchedule(int doctorId, int year, int weekNumber, DateTime appointmentStartDate, DateTime appointmentEndDate);
}