using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class GetLoginSessionRequest
    {
        [JsonProperty("email")] public string emailAddress;
    }
}