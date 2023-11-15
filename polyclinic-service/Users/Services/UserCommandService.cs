using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
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
        User user = await _repository.CreateAsync(userRequest);

        return user;
    }

    public async Task<User> UpdateUser(UpdateUserRequest userRequest)
    {
        User user = await _repository.GetByIdAsync(userRequest.Id);

        if (user == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }

        user = await _repository.UpdateAsync(userRequest);

        return user;
    }

    public async Task DeleteUser(int id)
    {
        User user = await _repository.GetByIdAsync(id);

        if (user == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }
            
        await _repository.DeleteAsync(id);
    }
}