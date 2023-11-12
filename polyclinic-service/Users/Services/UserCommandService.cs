using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.Users.Services;

public class UserCommandService : IUserCommandService
{
    private IUserRepository _repository;
    
    public UserCommandService(IUserRepository repository)
    {
        _repository = repository;
    }

    public async Task<User> CreateUser(CreateUserRequest userRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUser(UpdateUserRequest userRequest)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteUser(int id)
    {
        throw new NotImplementedException();
    }
}