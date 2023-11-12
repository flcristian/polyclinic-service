using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Appointments.Services.Interfaces;

namespace polyclinic_service.Appointments.Services;

public class AppointmentCommandService : IAppointmentCommandService
{
    private IAppointmentRepository _repository;
    
    public AppointmentCommandService(IAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Appointment> CreateAppointment(CreateAppointmentRequest appointmentRequest)
    {
    }

    public async Task<Appointment> UpdateAppointment(int id, UpdateAppointmentRequest appointmentRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAppointment(int id)
    {
        throw new NotImplementedException();
    }
}