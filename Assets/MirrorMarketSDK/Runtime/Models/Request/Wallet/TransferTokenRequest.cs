using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferTokenRequest : BaseWeb3Request
    {
        [JsonProperty("to_publickey")] public string ToPublicKey;

        [JsonProperty("amount")] public ulong Amount;

        [JsonProperty("token_mint")] public string TokenMint;

        [JsonProperty("decimals")] public int Decimals;
    }
}