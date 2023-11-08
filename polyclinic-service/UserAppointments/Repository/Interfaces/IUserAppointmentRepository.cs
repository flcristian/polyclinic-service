using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Repository.Interfaces;

public interface IUserAppointmentRepository
{
    Task<IEnumerable<UserAppointment>> GetAllAsync();
    Task<UserAppointment> GetByIdAsync(int id);
    Task<UserAppointment> CreateAsync(CreateUserAppointmentRequest userAppointmentRequest);
    Task<UserAppointment> UpdateAsync(int id, UpdateUserAppointmentRequest userAppointmentRequest);
    Task DeleteAsync(int id);
}