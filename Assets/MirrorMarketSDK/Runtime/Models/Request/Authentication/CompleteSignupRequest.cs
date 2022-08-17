using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CompleteSignupRequest
    {
        [JsonProperty("email")] public string Email;

        [JsonProperty("code")] public long Code;

        [JsonProperty("password")] public string Password;
    }
}