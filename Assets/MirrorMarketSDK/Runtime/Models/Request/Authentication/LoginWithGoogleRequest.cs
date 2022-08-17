using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class LoginWithGoogleRequest
    {
        [JsonProperty("identity_provider_token")] public string IdentityProviderToken;
    }
}