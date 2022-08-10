using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Authentication
{
    public class LoginWithGoogleRequest
    {
        [JsonProperty("identity_provider_token")]
        public string IdentityProviderToken;
    }
}