using AutoMapper;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service.System;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserRequest, User>();
        CreateMap<UpdateUserRequest, User>();
        CreateMap<CreateAppointmentRequest, Appointment>();
        CreateMap<UpdateAppointmentRequest, Appointment>();
        CreateMap<CreateScheduleSlotRequest, ScheduleSlot>()
            .ConvertUsing(dto => new ScheduleSlot
            {
                StartTime = dto.StartTime.Hours + ":" + dto.StartTime.Minutes,
                EndTime = dto.EndTime.Hours + ":" + dto.EndTime.Minutes
            });
        CreateMap<UpdateScheduleSlotRequest, ScheduleSlot>()
            .ConvertUsing(dto => new ScheduleSlot
            {
                StartTime = dto.StartTime.Hours + ":" + dto.StartTime.Minutes,
                EndTime = dto.EndTime.Hours + ":" + dto.EndTime.Minutes
            });
        CreateMap<ScheduleSlot, GetScheduleSlotRequest>()
            .ConvertUsing(slot => 
                new GetScheduleSlotRequest
                {
                    StartTime = Time.ConvertStringToTime(slot.StartTime),
                    EndTime = Time.ConvertStringToTime(slot.EndTime)
                });
        CreateMap<CreateUserAppointmentRequest, UserAppointment>();
        CreateMap<UpdateUserAppointmentRequest, UserAppointment>(); 
        CreateMap<Time, TimeSpan>().ConvertUsing(time => new TimeSpan(
                time.Hours, time.Minutes, 0
            ));
        CreateMap<Time, String>().ConvertUsing(time => 
            time.Hours + ":" + time.Minutes);
        CreateMap<String, Time>().ConvertUsing(time =>
            Time.ConvertStringToTime(time)
        );
    }
}