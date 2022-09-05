using UnityEngine;
using System.Collections;
using Newtonsoft.Json;

public class BaseWeb3Request
{
    [JsonProperty("confirmation")] public string Confirmation;
}
