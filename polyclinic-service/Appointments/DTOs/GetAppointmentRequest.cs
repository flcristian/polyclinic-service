using polyclinic_service.UserAppointments.DTOs;
using polyclinic_service.UserAppointments.Models;

namespace polyclinic_service.Appointments.DTOs;

public class GetAppointmentRequest
{
    public int Id { get; set; }
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public List<GetUserAppointmentRequest> UserAppointments { get; set; }
}