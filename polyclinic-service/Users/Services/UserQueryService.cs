using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
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
        IEnumerable<User> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.USERS_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<User> GetUserById(int id)
    {
        User result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.USER_DOES_NOT_EXIST);
        }

        return result;
    }

    public async Task<User> GetDoctorWithMostAppointments()
    {
        User result = await _repository.GetDoctorWithMostAppointmentsAsync();

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.NO_DOCTORS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }
    
    public async Task<User> GetPatientWithMostAppointments()
    {
        User result = await _repository.GetPatientWithMostAppointmentsAsync();

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.NO_PATIENTS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }

    public async Task<IEnumerable<User>> GetDoctorsByAppointmentsDecreasing()
    {
        List<User> result = (await _repository.GetDoctorsByAppointmentsDecreasingAsync()).ToList();

        if (result.Count <= 0)
        {
            throw new ItemsDoNotExist(Constants.NO_DOCTORS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }

    public async Task<IEnumerable<User>> GetDoctorsByAppointmentsIncreasing()
    {
        List<User> result = (await _repository.GetDoctorsByAppointmentsIncreasingAsync()).ToList();

        if (result.Count <= 0)
        {
            throw new ItemsDoNotExist(Constants.NO_DOCTORS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }
    
    public async Task<IEnumerable<User>> GetPatientsByAppointmentsDecreasing()
    {
        List<User> result = (await _repository.GetPatientsByAppointmentsDecreasingAsync()).ToList();

        if (result.Count <= 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PATIENTS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }

    public async Task<IEnumerable<User>> GetPatientsByAppointmentsIncreasing()
    {
        List<User> result = (await _repository.GetPatientsByAppointmentsIncreasingAsync()).ToList();

        if (result.Count <= 0)
        {
            throw new ItemsDoNotExist(Constants.NO_PATIENTS_HAVE_APPOINTMENTS);
        }
        
        return result;
    }
}