using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class FetchMultipleNftsByMintAddressesRequest
    {
        public List<string> mint_addresses;
    }
}