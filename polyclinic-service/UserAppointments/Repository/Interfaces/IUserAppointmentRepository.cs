using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Repository.Interfaces;

public interface IUserAppointmentRepository
{
    Task<IEnumerable<UserAppointment>> GetAllAsync();
    Task<UserAppointment> GetByIdAsync(int id);
}