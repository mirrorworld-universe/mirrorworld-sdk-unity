using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Authentication
{
    public class SignupResponse
    {
        [JsonProperty("message")]
        public string Message;
    }
}