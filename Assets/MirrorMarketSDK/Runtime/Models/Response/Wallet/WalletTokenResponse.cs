using System.Collections.Generic;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class WalletTokenResponse
    {
        [JsonProperty("tokens")] public List<Token> Tokens;

        [JsonProperty("sol")] public ulong Sol;
    }
    
    
    public class Token
    {
        [JsonProperty("ata")] public string ATA;

        [JsonProperty("mint")] public string Mint;

        [JsonProperty("amount")] public ulong Amount;
    }
}