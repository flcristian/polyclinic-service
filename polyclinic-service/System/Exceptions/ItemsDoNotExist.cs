namespace polyclinic_service.System.Exceptions;

public class ItemsDoNotExist : Exception
{
    public ItemsDoNotExist(string? message) : base(message)
    {
    }
}