using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services.Interfaces;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;

namespace polyclinic_service.UserAppointments.Services;

public class UserAppointmentCommandService : IUserAppointmentCommandService
{
    private IUserAppointmentRepository _repository;
    
    public UserAppointmentCommandService(IUserAppointmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<UserAppointment> CreateUserAppointment(CreateUserAppointmentRequest userAppointmentRequest)
    {
        UserAppointment userAppointment = await _repository.CreateAsync(userAppointmentRequest);

        return userAppointment;
    }

    public async Task<UserAppointment> UpdateUserAppointment(UpdateUserAppointmentRequest userAppointmentRequest)
    {
        UserAppointment userAppointment = await _repository.GetByIdAsync(userAppointmentRequest.Id);

        if (userAppointment == null)
        {
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        }

        userAppointment = await _repository.UpdateAsync(userAppointmentRequest);

        return userAppointment;
    }

    public async Task DeleteUserAppointment(int id)
    {
        UserAppointment userAppointment = await _repository.GetByIdAsync(id);

        if (userAppointment == null)
        {
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(id);
    }
}