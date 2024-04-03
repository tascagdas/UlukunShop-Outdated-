using System.Text.Json.Serialization;

namespace UlukunShopAPI.Application.DTOs.FacebookAccessToken
{
    public class FacebookUserInfoResponse
    {
        [JsonPropertyName("id")] 
        public string Id { get; set; }

        [JsonPropertyName("email")] 
        public string Email { get; set; }

        [JsonPropertyName("name")] 
        public string FullName { get; set; }
    }
}