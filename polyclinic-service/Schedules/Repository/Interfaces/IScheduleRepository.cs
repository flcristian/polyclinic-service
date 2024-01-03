using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Repository.Interfaces;

public interface IScheduleRepository
{
    Task<IEnumerable<Schedule>> GetAllAsync();
    Task<Schedule> GetByDoctorIdAsync(int doctorId);
    Task<Schedule> CreateAsync(CreateScheduleRequest ScheduleRequest);
    Task<Schedule> UpdateAsync(UpdateScheduleRequest ScheduleRequest);
    Task DeleteAsync(int id);
}