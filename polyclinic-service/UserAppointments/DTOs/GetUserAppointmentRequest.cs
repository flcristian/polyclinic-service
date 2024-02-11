using polyclinic_service.Appointments.Models;
using polyclinic_service.Users.Models;

namespace polyclinic_service.UserAppointments.DTOs;

public class GetUserAppointmentRequest
{
    public int Id { get; set; }
    public User User { get; set; }
    public Appointment Appointment { get; set; }
}