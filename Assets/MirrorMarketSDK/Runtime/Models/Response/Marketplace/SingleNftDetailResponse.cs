using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class SingleNftDetailResponse
    {
        [JsonProperty("nft")] public NftDetails Nft;
    }
}