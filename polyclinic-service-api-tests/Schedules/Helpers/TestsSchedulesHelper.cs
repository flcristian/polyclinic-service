using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;

namespace polyclinic_service_api_tests.Schedules.Helpers
{
    public static class TestsSchedulesHelper
    {
        public static Time CreateTime(int hours, int minutes)
        {
            return new Time
            {
                Hours = hours,
                Minutes = minutes
            };
        }

        public static ScheduleSlot CreateScheduleSlot()
        {
            Time startTime = new Time
            {
                Hours = 12,
                Minutes = 0
            };
            
            Time endTime = new Time
            {
                Hours = 13,
                Minutes = 0
            };
            
            return new ScheduleSlot
            {
                StartTime = startTime.ToString(),
                EndTime = endTime.ToString()
            };
        }

        public static Schedule CreateSchedule(int doctorId, int year, int weekNumber)
        {
            return new Schedule
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber,
                MondaySchedule = CreateScheduleSlot(),
                TuesdaySchedule = CreateScheduleSlot(),
                WednesdaySchedule = CreateScheduleSlot(),
                ThursdaySchedule = CreateScheduleSlot(),
                FridaySchedule = CreateScheduleSlot()
            };
        }

        public static UpdateScheduleSlotRequest CreateUpdateScheduleSlotRequest()
        {
            Time startTime = new Time
            {
                Hours = 12,
                Minutes = 0
            };
            
            Time endTime = new Time
            {
                Hours = 13,
                Minutes = 0
            };
            
            return new UpdateScheduleSlotRequest
            {
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public static UpdateScheduleRequest CreateUpdateScheduleRequest(int doctorId, int year, int weekNumber)
        {
            return new UpdateScheduleRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber,
                MondaySchedule = CreateUpdateScheduleSlotRequest(),
                TuesdaySchedule = CreateUpdateScheduleSlotRequest(),
                WednesdaySchedule = CreateUpdateScheduleSlotRequest(),
                ThursdaySchedule = CreateUpdateScheduleSlotRequest(),
                FridaySchedule = CreateUpdateScheduleSlotRequest()
            };
        }

        public static GetScheduleSlotRequest CreateGetScheduleSlotRequest()
        {
            Time startTime = new Time
            {
                Hours = 12,
                Minutes = 0
            };
            
            Time endTime = new Time
            {
                Hours = 13,
                Minutes = 0
            };
            
            return new GetScheduleSlotRequest
            {
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public static GetScheduleRequest CreateGetScheduleRequest(int doctorId, int year, int weekNumber)
        {
            return new GetScheduleRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber,
                MondaySchedule = CreateGetScheduleSlotRequest(),
                TuesdaySchedule = CreateGetScheduleSlotRequest(),
                WednesdaySchedule = CreateGetScheduleSlotRequest(),
                ThursdaySchedule = CreateGetScheduleSlotRequest(),
                FridaySchedule = CreateGetScheduleSlotRequest()
            };
        }

        public static GetByDoctorIdAndWeekIdentityRequest CreateGetByDoctorIdAndWeekIdentityRequest(int doctorId, int year, int weekNumber)
        {
            return new GetByDoctorIdAndWeekIdentityRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber
            };
        }

        public static DeleteScheduleRequest CreateDeleteScheduleRequest(int doctorId, int year, int weekNumber)
        {
            return new DeleteScheduleRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber
            };
        }

        public static CreateScheduleSlotRequest CreateCreateScheduleSlotRequest()
        {
            Time startTime = new Time
            {
                Hours = 12,
                Minutes = 0
            };
            
            Time endTime = new Time
            {
                Hours = 13,
                Minutes = 0
            };
            
            return new CreateScheduleSlotRequest
            {
                StartTime = startTime,
                EndTime = endTime
            };
        }

        public static CreateScheduleRequest CreateCreateScheduleRequest(int doctorId, int year, int weekNumber)
        {
            return new CreateScheduleRequest
            {
                DoctorId = doctorId,
                Year = year,
                WeekNumber = weekNumber,
                MondaySchedule = CreateCreateScheduleSlotRequest(),
                TuesdaySchedule = CreateCreateScheduleSlotRequest(),
                WednesdaySchedule = CreateCreateScheduleSlotRequest(),
                ThursdaySchedule = CreateCreateScheduleSlotRequest(),
                FridaySchedule = CreateCreateScheduleSlotRequest()
            };
        }
    }
}
