using polyclinic_service.Appointments.Models;
using polyclinic_service.System.Constants;
using polyclinic_service.System.Exceptions;
using polyclinic_service.UserAppointments.Models;
using polyclinic_service.UserAppointments.Repository.Interfaces;
using polyclinic_service.UserAppointments.Services.Interfaces;

namespace polyclinic_service.UserAppointments.Services;

public class UserAppointmentQueryService : IUserAppointmentQueryService
{
    private IUserAppointmentRepository _repository;


    public UserAppointmentQueryService(IUserAppointmentRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<UserAppointment>> GetAllUserAppointments()
    {
        IEnumerable<UserAppointment> result = await _repository.GetAllAsync();

        if (result.Count() == 0)
        {
            throw new ItemsDoNotExist(Constants.USER_APPOINTMENTS_DO_NOT_EXIST);
        }

        return result;
    }

    public async Task<UserAppointment> GetUserAppointmentById(int id)
    {
        UserAppointment result = await _repository.GetByIdAsync(id);

        if (result == null)
        {
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        }

        return result;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentsByUserId(int userId)
    {
        IEnumerable<UserAppointment> userAppointments = await _repository.GetUserAppointmentsByUserId(userId);

        List<Appointment> appointments =
            userAppointments.Select(userAppointment => userAppointment.Appointment)
                .Where(appointment => appointment.StartDate >= DateTime.Now || appointment.EndDate >= DateTime.Now).ToList();

        if (appointments.Count == 0)
        {
            throw new ItemsDoNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST);
        }
        
        SortAppointmentList(appointments);

        return appointments;
    }

    public async Task<IEnumerable<Appointment>> GetAppointmentHistoryByUserId(int userId)
    {
        IEnumerable<UserAppointment> userAppointments = await _repository.GetUserAppointmentsByUserId(userId);

        List<Appointment> appointments =
            userAppointments.Select(userAppointment => userAppointment.Appointment).Where(appointment => appointment.EndDate < DateTime.Now).ToList();
        
        if (appointments.Count == 0)
        {
            throw new ItemsDoNotExist(Constants.APPOINTMENTS_DO_NOT_EXIST);
        }
        
        SortAppointmentList(appointments);

        return appointments;
    }
    
    private void SortAppointmentList(List<Appointment> appointments)
    {
        appointments.Sort((x, y) => {
            if (x.StartDate < y.StartDate) {
                return -1;
            }
            if (x.StartDate > y.StartDate) {
                return 1;
            } 
            if (x.EndDate < y.EndDate) {
                return -1;
            }
            if (x.EndDate > y.EndDate) {
                return 1;
            }
            return 0;
        });
    }
}