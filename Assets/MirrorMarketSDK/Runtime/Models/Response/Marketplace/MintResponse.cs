using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class MintResponse
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("url")] public string Url;

        [JsonProperty("update_authority")] public string UpdateAuthority;

        [JsonProperty("creator_address")] public string CreatorAddress;

        [JsonProperty("name")] public string Name;

        [JsonProperty("symbol")] public string Symbol;

        [JsonProperty("collection")] public string Collection;

        [JsonProperty("signature")] public string Signature;

        [JsonProperty("status")] public string Status;
    }
}