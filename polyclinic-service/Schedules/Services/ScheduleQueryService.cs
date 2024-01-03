using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.Schedules.Models;
using polyclinic_service.Schedules.Repository.Interfaces;
using polyclinic_service.Schedules.Services.Interfaces;

namespace polyclinic_service.Schedules.Services
{
    public class ScheduleQueryService : IScheduleQueryService
    {
        private IScheduleRepository _repository;

        public ScheduleQueryService(IScheduleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedules()
        {
            IEnumerable<Schedule> result = await _repository.GetAllAsync();

            if (!result.Any())
            {
                throw new ItemsDoNotExist(Constants.SCHEDULES_DO_NOT_EXIST);
            }

            return result;
        }

        public async Task<Schedule> GetScheduleByDoctorId(int doctorId)
        {
            Schedule result = await _repository.GetByDoctorIdAsync(doctorId);

            if (result == null)
            {
                throw new ItemDoesNotExist(Constants.SCHEDULE_DOES_NOT_EXIST);
            }

            return result;
        }
    }
}