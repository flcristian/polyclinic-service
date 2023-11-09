using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Services.Interfaces;

public interface IUserAppointmentCommandService
{
    Task<UserAppointment> CreateUserAppointment(CreateUserAppointmentRequest userAppointmentRequest);
    Task<UserAppointment> UpdateUserAppointment(int id, UpdateUserAppointmentRequest userAppointmentRequest);
    Task DeleteUserAppointment(int id);
}