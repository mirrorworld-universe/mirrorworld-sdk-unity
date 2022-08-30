using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CreateNftRequest
    {
        [JsonProperty("collection_mint")] public string CollectionMint;
        
        [JsonProperty("name")] public string Name;

        [JsonProperty("symbol")] public string Symbol;

        [JsonProperty("url")] public string Url;

        [JsonProperty("confirmation")] public string Confirmation;
    }
}