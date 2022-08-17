using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Authentication
{
    public class CompleteSignupRequest
    {
        [JsonProperty("email")] public string Email;

        [JsonProperty("code")] public long Code;

        [JsonProperty("password")] public string Password;
    }
}