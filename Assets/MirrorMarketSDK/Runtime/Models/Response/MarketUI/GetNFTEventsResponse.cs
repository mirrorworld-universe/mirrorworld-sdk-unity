using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetNFTEventsResponse
    {
        public int total_page;
        public int page_size;
        public List<MirrorMarketNFTEvent> events;
    }
}