using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.Repository.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(int id);
    Task<User> CreateAsync(CreateUserRequest userRequest);
    Task<User> UpdateAsync(UpdateUserRequest userRequest);
    Task DeleteAsync(int id);
    Task<User> GetDoctorWithMostAppointmentsAsync();
    Task<User> GetPatientWithMostAppointmentsAsync();
    Task<IEnumerable<User>> GetDoctorsByAppointmentsDecreasingAsync();
    Task<IEnumerable<User>> GetDoctorsByAppointmentsIncreasingAsync();
    Task<IEnumerable<User>> GetPatientsByAppointmentsDecreasingAsync();
    Task<IEnumerable<User>> GetPatientsByAppointmentsIncreasingAsync();
}