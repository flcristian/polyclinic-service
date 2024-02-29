using AutoMapper;
using polyclinic_service.Schedules.DTOs;
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
        private IMapper _mapper;

        public ScheduleQueryService(IScheduleRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetScheduleRequest>> GetAllSchedules()
        {
            List<Schedule> schedules = (await _repository.GetAllAsync()).ToList();

            if (!schedules.Any())
            {
                throw new ItemsDoNotExist(Constants.SCHEDULES_DO_NOT_EXIST);
            }

            List<GetScheduleRequest> result = new List<GetScheduleRequest>();
            schedules.ForEach(schedule =>
            {
                result.Add(new GetScheduleRequest
                {
                    DoctorId = schedule.DoctorId,
                    Year = schedule.Year,
                    WeekNumber = schedule.WeekNumber,
                    MondaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.MondaySchedule),
                    TuesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.TuesdaySchedule),
                    WednesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.WednesdaySchedule),
                    ThursdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.ThursdaySchedule),
                    FridaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.FridaySchedule)
                });
            });

            return result;
        }

        public async Task<IEnumerable<GetScheduleRequest>> GetSchedulesByDoctorId(int doctorId)
        {
            List<Schedule> schedules = (await _repository.GetSchedulesByDoctorIdAsync(doctorId)).ToList();

            if (!schedules.Any())
            {
                throw new ItemsDoNotExist(Constants.SCHEDULES_DO_NOT_EXIST);
            }

            List<GetScheduleRequest> result = new List<GetScheduleRequest>();
            schedules.ForEach(schedule =>
            {
                result.Add(new GetScheduleRequest
                {
                    DoctorId = schedule.DoctorId,
                    Year = schedule.Year,
                    WeekNumber = schedule.WeekNumber,
                    MondaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.MondaySchedule),
                    TuesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.TuesdaySchedule),
                    WednesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.WednesdaySchedule),
                    ThursdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.ThursdaySchedule),
                    FridaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.FridaySchedule)
                });
            });

            return result;
        }

        public async Task<GetScheduleRequest> GetScheduleByDoctorIdAndWeekIdentity(GetByDoctorIdAndWeekIdentityRequest scheduleRequest)
        {
            Schedule schedule = await _repository.GetByDoctorIdAndWeekIdentityAsync(scheduleRequest);

            if (schedule == null)
            {
                throw new ItemDoesNotExist(Constants.SCHEDULE_DOES_NOT_EXIST);
            }
            
            return new GetScheduleRequest
            {
                DoctorId = schedule.DoctorId,
                Year = schedule.Year,
                WeekNumber = schedule.WeekNumber,
                MondaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.MondaySchedule),
                TuesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.TuesdaySchedule),
                WednesdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.WednesdaySchedule),
                ThursdaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.ThursdaySchedule),
                FridaySchedule = _mapper.Map<GetScheduleSlotRequest>(schedule.FridaySchedule)
            };
        }

        public async Task<bool> CheckIfAppointmentInDoctorSchedule(int doctorId, int year, int weekNumber, DateTime appointmentStartDate, DateTime appointmentEndDate)
        {
            Schedule schedule = await _repository.GetByDoctorIdAndWeekIdentityAsync(new GetByDoctorIdAndWeekIdentityRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber
            });
            
            if (schedule == null)
            {
                throw new ItemDoesNotExist(Constants.SCHEDULE_DOES_NOT_EXIST);
            }

            ScheduleSlot scheduleSlot = ChooseScheduleSlotDayFromDate(schedule, appointmentStartDate);

            TimeSpan appointmentStartTime = new TimeSpan(appointmentStartDate.Hour, appointmentStartDate.Minute, 0);
            TimeSpan appointmentEndTime = new TimeSpan(appointmentEndDate.Hour, appointmentEndDate.Minute, 0);

            Time scheduleSlotStartTime = _mapper.Map<Time>(scheduleSlot.StartTime);
            Time scheduleSlotEndTime = _mapper.Map<Time>(scheduleSlot.EndTime);

            return appointmentStartTime >= _mapper.Map<TimeSpan>(scheduleSlotStartTime)
                   && appointmentEndTime <= _mapper.Map<TimeSpan>(scheduleSlotEndTime);
        }

        private ScheduleSlot ChooseScheduleSlotDayFromDate(Schedule schedule, DateTime date)
        {
            DayOfWeek dayOfWeek = date.DayOfWeek;

            switch (dayOfWeek)
            {
                case DayOfWeek.Monday:
                    return schedule.MondaySchedule;
                case DayOfWeek.Tuesday:
                    return schedule.TuesdaySchedule;
                case DayOfWeek.Wednesday:
                    return schedule.WednesdaySchedule;
                case DayOfWeek.Thursday:
                    return schedule.ThursdaySchedule;
                case DayOfWeek.Friday:
                    return schedule.FridaySchedule;
            }

            return null!;
        }
    }
}