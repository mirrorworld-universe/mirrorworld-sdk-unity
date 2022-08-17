using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Wallet
{
    public class TransferTokenResponse
    {
        [JsonProperty("txSignature")] public string TxSignature;
    }
}