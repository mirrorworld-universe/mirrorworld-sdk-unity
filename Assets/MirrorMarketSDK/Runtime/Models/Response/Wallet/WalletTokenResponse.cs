using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Wallet
{
    public class WalletTokenResponse
    {
        [JsonProperty("tokens")] public List<Token> Tokens;

        [JsonProperty("sol")] public ulong Sol;
    }
    
    
    public partial class Token
    {
        [JsonProperty("ata")] public string ATA;

        [JsonProperty("mint")] public string Mint;

        [JsonProperty("amount")] public ulong Amount;
    }
}