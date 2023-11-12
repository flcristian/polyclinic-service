using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Repository.Interfaces;

namespace polyclinic_service.Users.Repository;

public class UserRepository : IUserRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> CreateAsync(CreateUserRequest userRequest)
    {
        User user = _mapper.Map<User>(userRequest);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateAsync(UpdateUserRequest userRequest)
    {
        User user = await _context.Users.FindAsync(userRequest.Id);

        user.Name = userRequest.Name;
        user.Email = userRequest.Email;
        user.Password = userRequest.Password;
        user.Gender = userRequest.Gender;
        user.Age = userRequest.Age;
        user.Phone = userRequest.Phone;
        user.Type = userRequest.Type;

        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        User user = await _context.Users.FindAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}