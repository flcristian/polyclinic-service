using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.Services.Interfaces;

public interface IUserCommandService
{
    Task<User> CreateUser(CreateUserRequest userRequest);
    Task<User> UpdateUser(int id, UpdateUserRequest userRequest);
    Task DeleteUser(int id);
}