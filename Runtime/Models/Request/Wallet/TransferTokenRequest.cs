using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Wallet
{
    public class TransferTokenRequest
    {
        [JsonProperty("to_publickey")] public string ToPublicKey;

        [JsonProperty("amount")] public ulong Amount;

        [JsonProperty("token_mint")] public string TokenMint;

        [JsonProperty("decimals")] public int Decimals;
    }
}