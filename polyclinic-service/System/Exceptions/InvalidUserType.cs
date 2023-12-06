namespace polyclinic_service.System.Exceptions;

public class InvalidUserType : Exception
{
    public InvalidUserType(string? message) : base(message)
    {
    }
}