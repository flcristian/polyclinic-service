using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service.Schedules.Services.Interfaces;

public interface IScheduleCommandService
{
    Task<Schedule> CreateSchedule(CreateScheduleRequest ScheduleRequest);
    Task<Schedule> UpdateSchedule(UpdateScheduleRequest ScheduleRequest);
    Task DeleteSchedule(int doctorId);
}