using polyclinic_service.Appointments.Models;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.UserAppointments.Services.Interfaces;

public interface IUserAppointmentQueryService
{
    Task<IEnumerable<UserAppointment>> GetAllUserAppointments();
    Task<UserAppointment> GetUserAppointmentById(int id);
    Task<IEnumerable<Appointment>> GetAppointmentsByUserId(int userId);
    Task<IEnumerable<Appointment>> GetAppointmentHistoryByUserId(int userId);
}