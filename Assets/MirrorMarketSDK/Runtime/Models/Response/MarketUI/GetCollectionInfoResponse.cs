using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetCollectionInfoResponse
    {
        public string collection;

        public string collection_name;

        public string collection_type;

        public List<CollectionOrder> collection_orders;
    }
}
