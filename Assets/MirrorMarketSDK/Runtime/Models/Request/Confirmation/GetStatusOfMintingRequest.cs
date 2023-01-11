using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetStatusOfMintingRequest
    {
        public List<string> mint_addresses;
    }
}
