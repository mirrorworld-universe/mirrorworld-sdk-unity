using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferNftRequest
    {
        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("to_wallet_address")] public string ToWalletAddress;
    }
}