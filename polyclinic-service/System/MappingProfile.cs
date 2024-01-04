using AutoMapper;
using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Schedules.DTOs;
using polyclinic_service.Schedules.Models;
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
        CreateMap<CreateScheduleSlotRequest, ScheduleSlot>();
        CreateMap<UpdateScheduleSlotRequest, ScheduleSlot>(); 
    }
}