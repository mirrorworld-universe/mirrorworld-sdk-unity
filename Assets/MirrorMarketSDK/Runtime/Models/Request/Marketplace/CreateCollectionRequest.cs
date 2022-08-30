using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CreateCollectionRequest
    {
        [JsonProperty("name")] public string Name;

        [JsonProperty("symbol")] public string Symbol;

        [JsonProperty("url")] public string Url;

        [JsonProperty("confirmation")] public string Comfirmation;
    }
}