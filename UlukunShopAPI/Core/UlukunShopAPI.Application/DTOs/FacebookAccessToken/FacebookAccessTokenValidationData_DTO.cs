using System.Text.Json.Serialization;

namespace UlukunShopAPI.Application.DTOs.FacebookAccessToken;

public class FacebookAccessTokenValidationData_DTO
{
    [JsonPropertyName("is_valid")]
    public bool IsValid { get; set; }
    [JsonPropertyName("user_id")]
    public string UserId { get; set; }
}