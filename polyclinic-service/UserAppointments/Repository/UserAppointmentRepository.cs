using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;

namespace polyclinic_service.UserAppointments.Repository;

public class UserAppointmentRepository : IUserAppointmentRepository
{
    private AppDbContext _context;
    private IMapper _mapper;

    public UserAppointmentRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserAppointment>> GetAllAsync()
    {
        return await _context.UserAppointments
            .Include(userAppointment => userAppointment.User)
            .Include(userAppointment => userAppointment.Appointment)
            .ToListAsync();
    }

    public async Task<UserAppointment> GetByIdAsync(int id)
    {
        return (await _context.UserAppointments
            .Include(userAppointment => userAppointment.User)
            .Include(userAppointment => userAppointment.Appointment)
            .FirstOrDefaultAsync(userAppointment => userAppointment.Id == id))!;
    }

    public async Task<IEnumerable<UserAppointment>> GetUserAppointmentsByUserId(int userId)
    {
        return await _context.UserAppointments
            .Include(userAppointment => userAppointment.User)
            .Include(userAppointment => userAppointment.Appointment)
            .Where(userAppointment => userAppointment.UserId == userId)
            .ToListAsync();
    }
    
    public async Task<UserAppointment> CreateAsync(CreateUserAppointmentRequest userAppointmentRequest)
    {
        UserAppointment userAppointment = _mapper.Map<UserAppointment>(userAppointmentRequest);
        _context.UserAppointments.Add(userAppointment);
        await _context.SaveChangesAsync();
        return userAppointment;
    }

    public async Task<UserAppointment> UpdateAsync(UpdateUserAppointmentRequest userAppointmentRequest)
    {
        UserAppointment userAppointment = (await _context.UserAppointments.FindAsync(userAppointmentRequest.Id))!;

        userAppointment.UserId = userAppointmentRequest.UserId;
        userAppointment.AppointmentId = userAppointmentRequest.AppointmentId;

        _context.UserAppointments.Update(userAppointment);
        await _context.SaveChangesAsync();
        return userAppointment;
    }
    
    public async Task DeleteAsync(int id)
    {
        UserAppointment userAppointment = (await _context.UserAppointments.FindAsync(id))!;
        _context.UserAppointments.Remove(userAppointment);
        await _context.SaveChangesAsync();
    }
}