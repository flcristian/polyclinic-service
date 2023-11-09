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
        throw new NotImplementedException();
    }

    public async Task<UserAppointment> GetUserAppointmentById(int id)
    {
        throw new NotImplementedException();
    }
}