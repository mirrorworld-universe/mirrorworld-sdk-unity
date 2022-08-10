using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Authentication
{
    public class BasicEmailLogin
    {
        [JsonProperty("email")]
        public string Email;
    
        [JsonProperty("password")]
        public string Password;
    }
}