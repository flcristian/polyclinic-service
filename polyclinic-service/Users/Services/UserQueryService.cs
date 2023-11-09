using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;
using polyclinic_service.Users.Services.Interfaces;

namespace polyclinic_service.Users.Services;

public class UserQueryService : IUserQueryService
{
    private IUserRepository _repository;
    
    public UserQueryService(IUserRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<User>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserById(int id)
    {
        throw new NotImplementedException();
    }
}