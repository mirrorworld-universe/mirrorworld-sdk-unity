using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetNFTsResponse
    {
        public int total_page;

        public int page_size;

        public List<MirrorMarketNFTObj> nfts;
    }
}
