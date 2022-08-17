using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class CreateSubCollectionRequest
    {
        [JsonProperty("collection_mint")] public string CollectionMint;
        
        [JsonProperty("name")] public string Name;

        [JsonProperty("symbol")] public string Symbol;

        [JsonProperty("url")] public string Url;
    }
}