using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Wallet
{
    public class TransferSolResponse
    {
        [JsonProperty("txSignature")] public string TxSignature;
    }
}