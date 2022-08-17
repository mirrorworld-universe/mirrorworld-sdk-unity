using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferSolRequest
    {
        [JsonProperty("to_publickey")] public string ToPublicKey;

        [JsonProperty("amount")] public ulong Amount;
    }
}