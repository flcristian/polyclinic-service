namespace polyclinic_service.UserActions.DTOs;

public class CreateAppointmentActionRequest
{
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime AppointmentDate { get; set; }
    public int Minutes { get; set; }
}