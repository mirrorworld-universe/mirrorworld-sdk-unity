using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetNFTEventsRequest
    {
        public string mint_address;

        public int page;

        public int page_size;
    }
}
