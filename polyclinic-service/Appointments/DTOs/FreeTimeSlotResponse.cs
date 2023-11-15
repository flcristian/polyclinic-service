namespace polyclinic_service.Appointments.DTOs;

public class FreeTimeSlotResponse
{
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }

    public override string ToString()
    {
        String desc = "";
        desc += $"Start date : {StartDate}\n";
        desc += $"End date : {EndDate}\n";
        return desc;
    }
}