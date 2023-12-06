using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.Services.Interfaces;

public interface IUserQueryService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> GetDoctorWithMostAppointments();
    Task<User> GetPatientWithMostAppointments();
    Task<IEnumerable<User>> GetDoctorsByAppointmentsDecreasing();
    Task<IEnumerable<User>> GetDoctorsByAppointmentsIncreasing();
    Task<IEnumerable<User>> GetPatientsByAppointmentsDecreasing();
    Task<IEnumerable<User>> GetPatientsByAppointmentsIncreasing();
}