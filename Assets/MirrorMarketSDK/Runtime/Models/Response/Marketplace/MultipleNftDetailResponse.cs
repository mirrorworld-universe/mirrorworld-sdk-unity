using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace
{
    public class MultipleNftDetailResponse
    {
        [JsonProperty("nfts")] public List<NftDetails> Nfts;
    }
}