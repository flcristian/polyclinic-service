namespace polyclinic_service.Appointments.DTOs;

public class DateResponse
{
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
    public int Count { get; set; }

    public override string ToString()
    {
        string desc = "";
        desc += $"Date : {Day}.{Month}.{Year}\n";
        desc += $"Count : {Count}";
        return desc;
    }
}