namespace polyclinic_service.Users.Models.Comparers;

public class UserEqualityComparer : IEqualityComparer<User>
{
    public bool Equals(User? x, User? y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.Name == y.Name && x.Email == y.Email && x.Password == y.Password && x.Gender == y.Gender && x.Age == y.Age && x.Phone == y.Phone && x.Type == y.Type;
    }

    public int GetHashCode(User obj)
    {
        return HashCode.Combine(obj.Id, obj.Name, obj.Email, obj.Password, obj.Gender, obj.Age, obj.Phone, (int)obj.Type);
    }
}