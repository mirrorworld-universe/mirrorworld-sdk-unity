using System;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class GetLoginSessionResponse
    {
        [JsonProperty("session_token")] public string sessionToken;
    }
}