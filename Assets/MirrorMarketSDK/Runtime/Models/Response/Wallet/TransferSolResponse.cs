using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferSolResponse
    {
        [JsonProperty("tx_signature")] public string TxSignature;
    }
}