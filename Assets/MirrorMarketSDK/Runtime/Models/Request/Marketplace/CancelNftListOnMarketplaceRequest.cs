using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class CancelNftListOnMarketplaceRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;
    }
}