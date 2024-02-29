namespace polyclinic_service.Schedules.Models;

public class Time
{
    public int Hours { get; set; }
    
    public int Minutes { get; set; }

    public static Time ConvertStringToTime(String time)
    {
        string[] values = time.Split(':');

        return new Time
        {
            Hours = Int32.Parse(values[0]),
            Minutes = Int32.Parse(values[1])
        };
    }
}