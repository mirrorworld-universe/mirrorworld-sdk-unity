using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class FetchMultipleNftsByMintAddressesRequest
    {
        [JsonProperty("mint_addresses")] public List<string> MintAddresses;
    }
}