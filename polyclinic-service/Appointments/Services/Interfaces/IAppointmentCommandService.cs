using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Services.Interfaces;

public interface IAppointmentCommandService
{
    Task<Appointment> CreateAppointment(CreateAppointmentRequest appointmentRequest);
    Task<Appointment> UpdateAppointment(UpdateAppointmentRequest appointmentRequest);
    Task DeleteAppointment(int id);
}