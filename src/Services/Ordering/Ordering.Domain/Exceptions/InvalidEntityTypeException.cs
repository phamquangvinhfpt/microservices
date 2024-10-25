namespace Ordering.Domain.Exceptions;

public class InvalidEntityTypeException : ApplicationException
{
    public InvalidEntityTypeException(string name, object key)
        : base($"Entity \"{name}\" ({key}) has invalid type.")
    {
    }
}