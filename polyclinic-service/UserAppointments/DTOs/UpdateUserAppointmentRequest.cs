namespace polyclinic_service.UserAppointments.DTOs;

public class UpdateUserAppointmentRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int AppointmentId { get; set; }
}