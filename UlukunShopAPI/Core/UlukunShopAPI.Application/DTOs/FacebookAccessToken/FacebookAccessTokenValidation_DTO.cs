using System.Text.Json.Serialization;

namespace UlukunShopAPI.Application.DTOs.FacebookAccessToken;

public class FacebookAccessTokenValidation_DTO
{
    [JsonPropertyName("data")]
    public FacebookAccessTokenValidationData_DTO Data { get; set; }
}
