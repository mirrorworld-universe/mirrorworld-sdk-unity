using System;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class CompleteLoginWithSessionResponse
    {
        [JsonProperty("session_token")] public string sessionToken;
    }
}
