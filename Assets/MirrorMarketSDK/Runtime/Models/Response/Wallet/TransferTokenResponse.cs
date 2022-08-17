using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferTokenResponse
    {
        [JsonProperty("txSignature")] public string TxSignature;
    }
}