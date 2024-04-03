using UlukunShopAPI.Application.Abstractions.Services.Authentications;

namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IAuthService:IExternalAuthentication,IInternalAuthentication
{
}