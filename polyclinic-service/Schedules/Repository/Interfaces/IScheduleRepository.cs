using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Repository.Interfaces;

public interface IScheduleRepository
{
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<IEnumerable<Schedule>> GetSchedulesByDoctorIdAsync(int doctorId);
    Task<Schedule> GetByDoctorIdAndWeekIdentityAsync(GetByDoctorIdAndWeekIdentityRequest scheduleRequest);
    Task<Schedule> CreateAsync(CreateScheduleRequest ScheduleRequest);
    Task<Schedule> UpdateAsync(UpdateScheduleRequest ScheduleRequest);
    Task<Schedule> DeleteAsync(DeleteScheduleRequest scheduleRequest);
}