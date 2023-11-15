using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

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
        Appointment appointment = await _repository.CreateAsync(appointmentRequest);

        return appointment;
    }

    public async Task<Appointment> UpdateAppointment(UpdateAppointmentRequest appointmentRequest)
    {
        Appointment appointment = await _repository.GetByIdAsync(appointmentRequest.Id);

        if (appointment == null)
        {
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        }

        appointment = await _repository.UpdateAsync(appointmentRequest);

        return appointment;
    }

    public async Task DeleteAppointment(int id)
    {
        Appointment appointment = await _repository.GetByIdAsync(id);

        if (appointment == null)
        {
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(id);
    }
}