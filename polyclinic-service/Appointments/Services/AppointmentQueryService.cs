using polyclinic_service.Appointments.DTOs;
using polyclinic_service.Appointments.Models;
using polyclinic_service.Appointments.Repository.Interfaces;
using polyclinic_service.Appointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service.Appointments.Services;

public class AppointmentQueryService : IAppointmentQueryService
{
    private IAppointmentRepository _repository;
    
    public AppointmentQueryService(IAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<Appointment>> GetAllAppointments()
    {
        IEnumerable<Appointment> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<Appointment> GetAppointmentById(int id)
    {
        Appointment result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        }

        return result;
    }

    public async Task<IEnumerable<FreeTimeSlotResponse>> GetFreeSlotsForInterval(int userId, DateTime startDate, DateTime endDate)
    {
        IEnumerable<FreeTimeSlotResponse> freeSlots = await _repository.GetFreeSlotsAsync(userId, startDate, endDate);

        if (freeSlots.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.NO_FREE_TIME_SLOTS);
        }

        return freeSlots;
    }
}