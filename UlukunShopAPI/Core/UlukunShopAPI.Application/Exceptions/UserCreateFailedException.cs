using System.Runtime.Serialization;

namespace UlukunShopAPI.Application.Exceptions;

public class UserCreateFailedException:Exception
{
    public UserCreateFailedException():base("Kullancl olugturulurken beklenmeyen bir hatayla kargilaçildi!")
    {
    }

    public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    public UserCreateFailedException(string? message) : base(message)
    {
    }
}