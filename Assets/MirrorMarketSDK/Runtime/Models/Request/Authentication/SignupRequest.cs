using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Authentication
{
    public class SignupRequest
    {
        [JsonProperty("email")] public string Email;
    }
}