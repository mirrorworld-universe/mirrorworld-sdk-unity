using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace
{
    public class FetchMultipleNftsByUpdateAuthoritiesRequest
    {
        [JsonProperty("update_authorities")] public List<string> UpdateAuthorities;

        [JsonProperty("limit")] public long Limit;

        [JsonProperty("offset")] public long Offset;
    }
}