using polyclinic_service.Appointments.Models;

namespace polyclinic_service.Appointments.Services.Interfaces;

public interface IAppointmentQueryService
{
    Task<IEnumerable<Appointment>> GetAllAppointments();
    Task<Appointment> GetAppointmentById(int id);
}