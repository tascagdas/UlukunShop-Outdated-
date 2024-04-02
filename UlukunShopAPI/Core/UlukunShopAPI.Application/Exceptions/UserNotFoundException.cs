using System.Runtime.Serialization;

namespace UlukunShopAPI.Application.Exceptions;

public class UserNotFoundException:Exception
{
    public UserNotFoundException() :base("oturum acilirken bir sorun ile karsilasildi")
    {
    }
    

    public UserNotFoundException(string? message) : base(message)
    {
    }

    public UserNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}