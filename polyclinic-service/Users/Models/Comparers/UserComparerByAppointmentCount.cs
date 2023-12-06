namespace polyclinic_service.Users.Models.Comparers;

public class UserComparerByAppointmentCountIncreasing : IComparer<User>
{
    public int Compare(User? x, User? y)
    {
        if (x.UserAppointments.Count > y.UserAppointments.Count)
        {
            return 1;
        }

        if (x.UserAppointments.Count == y.UserAppointments.Count)
        {
            return 0;
        }

        return -1;
    }
}