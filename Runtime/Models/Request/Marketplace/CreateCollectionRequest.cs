using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class CreateCollectionRequest
    {
        [JsonProperty("name")] public string Name;

        [JsonProperty("symbol")] public string Symbol;

        [JsonProperty("url")] public string Url;
    }
}