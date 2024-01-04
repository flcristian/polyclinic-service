using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Repository.Interfaces;
using polyclinic_service.Schedules.Services.Interfaces;

namespace polyclinic_service.Schedules.Services
{
    public class ScheduleCommandService : IScheduleCommandService
    {
        private IScheduleRepository _repository;

        public ScheduleCommandService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<Schedule> CreateSchedule(CreateScheduleRequest scheduleRequest)
        {
            Schedule schedule = await _repository.CreateAsync(scheduleRequest);

            return schedule;
        }

        public async Task<Schedule> UpdateSchedule(UpdateScheduleRequest scheduleRequest)
        {
            Schedule schedule = await _repository.GetByDoctorIdAsync(scheduleRequest.DoctorId);

            if (schedule == null)
            {
                throw new ItemDoesNotExist(Constants.SCHEDULE_DOES_NOT_EXIST);
            }

            schedule = await _repository.UpdateAsync(scheduleRequest);

            return schedule;
        }

        public async Task DeleteSchedule(int doctorId)
        {
            Schedule schedule = await _repository.GetByDoctorIdAsync(doctorId);

            if (schedule == null)
            {
                throw new ItemDoesNotExist(Constants.SCHEDULE_DOES_NOT_EXIST);
            }

            await _repository.DeleteAsync(doctorId);
        }
    }
}