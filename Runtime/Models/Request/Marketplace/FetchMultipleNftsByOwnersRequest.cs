using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class FetchMultipleNftsByOwnersRequest
    {
        [JsonProperty("owners")] public List<string> Owners;

        [JsonProperty("limit")] public long Limit;

        [JsonProperty("offset")] public long Offset;
    }
}