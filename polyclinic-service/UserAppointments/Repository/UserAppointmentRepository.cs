using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
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
            .Include(userAppointment => userAppointment.Patient)
            .Include(userAppointment => userAppointment.Doctor)
            .Include(userAppointment => userAppointment.Appointment)
            .ToListAsync();
    }

    public async Task<UserAppointment> GetByIdAsync(int id)
    {
        return (await _context.UserAppointments
            .Include(userAppointment => userAppointment.Patient)
            .Include(userAppointment => userAppointment.Doctor)
            .Include(userAppointment => userAppointment.Appointment)
            .FirstOrDefaultAsync(userAppointment => userAppointment.Id == id))!;
    }
}