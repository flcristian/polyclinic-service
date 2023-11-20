using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;

namespace polyclinic_service.UserAppointments.Repository;

public class UserAppointmentRepository : IUserAppointmentRepository
{
    private AppDbContext _context;

    public UserAppointmentRepository(AppDbContext context)
    {
        _context = context;
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
}