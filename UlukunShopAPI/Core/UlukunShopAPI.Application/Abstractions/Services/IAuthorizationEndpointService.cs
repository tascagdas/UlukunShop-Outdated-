namespace UlukunShopAPI.Application.Abstractions.Services;

public interface IAuthorizationEndpointService
{
    public Task AssignRoleEndpointAsync(string[] roles, string menu, string code, Type type);
    public Task<List<string>> GetRolesToEndpointAsync(string code, string menu);
}