using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResMetadataCollectionInfo
    {
        public string collection;
        public string collection_name;
        public string collection_type;
        public List<EVMCollectionOrder> collection_orders;
    }

    [System.Serializable]
    public class EVMCollectionOrder
    {
        public string order_field;
        public string order_desc;
        public bool desc;
    }
}
