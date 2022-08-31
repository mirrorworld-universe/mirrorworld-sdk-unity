using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CancelNftListOnMarketplaceRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;

        [JsonProperty("confirmation")] public string Confirmation;
    }
}