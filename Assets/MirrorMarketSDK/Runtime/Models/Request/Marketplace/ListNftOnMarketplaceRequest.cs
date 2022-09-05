using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class ListNftOnMarketplaceRequest : BaseWeb3Request
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;
    }
}