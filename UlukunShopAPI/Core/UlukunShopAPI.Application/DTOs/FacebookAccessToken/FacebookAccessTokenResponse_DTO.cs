using System.Text.Json.Serialization;

namespace UlukunShopAPI.Application.DTOs.FacebookAccessToken;

public class FacebookAccessTokenResponse_DTO
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }
    
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }
}