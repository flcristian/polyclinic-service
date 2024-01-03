using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Services.Interfaces;

public interface IScheduleQueryService
{
    Task<IEnumerable<Schedule>> GetAllSchedules();
    Task<Schedule> GetScheduleByDoctorId(int doctorId);
}