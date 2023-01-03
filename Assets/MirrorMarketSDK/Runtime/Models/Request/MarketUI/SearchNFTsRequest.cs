using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class SearchNFTsRequest
    {
        public List<string> collections;

        public string search;
    }
}
