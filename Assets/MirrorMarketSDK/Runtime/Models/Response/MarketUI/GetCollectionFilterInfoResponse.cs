using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetCollectionFilterInfoResponse
    {
        public string collection;

        public List<FilterInfo> filter_info;
    }
}