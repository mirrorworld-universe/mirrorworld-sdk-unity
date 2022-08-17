using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Authentication
{
    public class LoginResponse
    {
        [JsonProperty("access_token")] public string AccessToken;

        [JsonProperty("refresh_token")] public string RefreshToken;

        [JsonProperty("user")] public UserResponse UserResponse;
    }
}