namespace UlukunShopAPI.Application.Exceptions;

public class AuthenticationErrorException:Exception
{
    public AuthenticationErrorException():base("Kimlik dogrulama hatasi")
    {
    }

    public AuthenticationErrorException(string? message) : base(message)
    {
    }

    public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}