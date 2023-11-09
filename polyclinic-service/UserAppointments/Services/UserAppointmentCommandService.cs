using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services.Interfaces;

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
        throw new NotImplementedException();
    }

    public async Task<UserAppointment> UpdateUserAppointment(int id, UpdateUserAppointmentRequest userAppointmentRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUserAppointment(int id)
    {
        throw new NotImplementedException();
    }
}