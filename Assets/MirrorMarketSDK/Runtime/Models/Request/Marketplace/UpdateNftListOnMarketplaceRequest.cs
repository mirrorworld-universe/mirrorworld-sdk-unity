using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class UpdateNftListOnMarketplaceRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;
    }
}