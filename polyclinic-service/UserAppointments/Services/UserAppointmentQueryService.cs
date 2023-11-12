using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service.UserAppointments.Services;

public class UserAppointmentQueryService : IUserAppointmentQueryService
{
    private IUserAppointmentRepository _repository;
    
    public UserAppointmentQueryService(IUserAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<UserAppointment>> GetAllUserAppointments()
    {
        IEnumerable<UserAppointment> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<UserAppointment> GetUserAppointmentById(int id)
    {
        UserAppointment result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        }

        return result;
    }
}