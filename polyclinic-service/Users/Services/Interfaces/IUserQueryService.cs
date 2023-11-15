using polyclinic_service.Users.Models;

namespace polyclinic_service.Users.Services.Interfaces;

public interface IUserQueryService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
}