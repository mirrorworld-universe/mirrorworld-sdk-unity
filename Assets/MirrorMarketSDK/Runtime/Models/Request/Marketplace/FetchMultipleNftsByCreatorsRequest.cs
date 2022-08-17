using System.Collections.Generic;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class FetchMultipleNftsByCreatorsRequest
    {
        [JsonProperty("creators")] public List<string> Creators;

        [JsonProperty("limit")] public long Limit;

        [JsonProperty("offset")] public long Offset;
    }
}