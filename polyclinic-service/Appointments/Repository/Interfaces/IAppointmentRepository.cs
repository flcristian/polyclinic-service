using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Repository.Interfaces;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync();
    Task<Appointment> GetByIdAsync(int id);
    Task<Appointment> CreateAsync(CreateAppointmentRequest appointmentRequest);
    Task<Appointment> UpdateAsync(int id, UpdateAppointmentRequest appointmentRequest);
    Task DeleteAsync(int id);
}