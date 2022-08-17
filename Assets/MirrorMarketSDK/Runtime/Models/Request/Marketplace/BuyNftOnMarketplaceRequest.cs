using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class BuyNftOnMarketplaceRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;
    }
}