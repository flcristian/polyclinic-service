using AutoMapper;
using Microsoft.EntityFrameworkCore;
using polyclinic_service.Data;
using polyclinic_service.Users.DTOs;
using polyclinic_service.Users.Models;
using polyclinic_service.Users.Models.Comparers;
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
        return await _context.Users
            .Include(user => user.UserAppointments)
            .ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return (await _context.Users
            .Include(user => user.UserAppointments)
            .FirstOrDefaultAsync(user => user.Id == id))!;
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
        User user = (await _context.Users.FindAsync(userRequest.Id))!;

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
        User user = (await _context.Users.FindAsync(id))!;
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetDoctorWithMostAppointmentsAsync()
    {
        List<User> doctors = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Doctor).ToListAsync();

        int max = 0;
        User maxDoctor = null!;
        doctors.ForEach(doctor =>
        {
            if (doctor.UserAppointments.Count > max)
            {
                max = doctor.UserAppointments.Count;
                maxDoctor = doctor;
            }
        });

        return maxDoctor;
    }

    public async Task<User> GetPatientWithMostAppointmentsAsync()
    {
        List<User> patients = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Patient).ToListAsync();

        int max = 0;
        User maxPatient = null!;
        patients.ForEach(patient =>
        {
            if (patient.UserAppointments.Count > max)
            {
                max = patient.UserAppointments.Count;
                maxPatient = patient;
            }
        });

        return maxPatient;
    }

    public async Task<IEnumerable<User>> GetDoctorsByAppointmentsDecreasingAsync()
    {
        List<User> doctors = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Doctor).ToListAsync();

        doctors.Sort(new UserComparerByAppointmentCountDecreasing());

        return doctors;
    }

    public async Task<IEnumerable<User>> GetDoctorsByAppointmentsIncreasingAsync()
    {
        List<User> doctors = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Doctor).ToListAsync();

        doctors.Sort(new UserComparerByAppointmentCountIncreasing());

        return doctors;
    }
    
    public async Task<IEnumerable<User>> GetPatientsByAppointmentsDecreasingAsync()
    {
        List<User> patients = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Patient).ToListAsync();

        patients.Sort(new UserComparerByAppointmentCountDecreasing());

        return patients;
    }

    public async Task<IEnumerable<User>> GetPatientsByAppointmentsIncreasingAsync()
    {
        List<User> patients = await _context.Users.Include(user => user.UserAppointments)
            .Where(user => user.Type == UserType.Patient).ToListAsync();

        patients.Sort(new UserComparerByAppointmentCountIncreasing());

        return patients;
    }
}