using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace
{
    public class SingleNftDetailResponse
    {
        [JsonProperty("nft")] public NftDetails Nft;
    }
}