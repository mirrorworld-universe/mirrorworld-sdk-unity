using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class LoginResponse
    {
        [JsonProperty("access_token")] public string AccessToken;

        [JsonProperty("refresh_token")] public string RefreshToken;

        [JsonProperty("user")] public UserResponse UserResponse;
    }
}