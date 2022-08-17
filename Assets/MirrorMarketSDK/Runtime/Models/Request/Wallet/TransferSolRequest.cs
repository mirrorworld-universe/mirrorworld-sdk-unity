using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Wallet
{
    public class TransferSolRequest
    {
        [JsonProperty("to_publickey")] public string ToPublicKey;

        [JsonProperty("amount")] public ulong Amount;
    }
}