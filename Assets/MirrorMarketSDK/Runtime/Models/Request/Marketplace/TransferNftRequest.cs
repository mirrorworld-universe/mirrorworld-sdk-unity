using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class TransferNftRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("to_wallet_address")] public string ToWalletAddress;
    }
}