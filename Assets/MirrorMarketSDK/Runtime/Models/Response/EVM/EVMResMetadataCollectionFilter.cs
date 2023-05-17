using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResMetadataCollectionFilter
    {
        public string collection;
        public List<EVMMetadataCollectionFilterInfo> filter_info;
    }

    public class EVMMetadataCollectionFilterInfo
    {
        public string filter_name;
        public string filter_type;
        public List<int> filter_value;
    }
}