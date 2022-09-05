using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class TransferTokenResponse
    {
        [JsonProperty("tx_signature")] public string TxSignature;
    }
}