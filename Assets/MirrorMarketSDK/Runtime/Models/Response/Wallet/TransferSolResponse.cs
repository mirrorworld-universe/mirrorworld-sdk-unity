using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferSolResponse
    {
        [JsonProperty("txSignature")] public string TxSignature;
    }
}