using UlukunShopAPI.Application.DTOs.Configuration;

namespace UlukunShopAPI.Application.Abstractions.Services.Configurations;

public interface IApplicationService
{
    List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
}