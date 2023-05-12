﻿using UnityEngine;
using System.Collections;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMResMetadataCollectionsSummary
    {
        public string collection;
        public string collection_name;
        public string collection_owner;
        public int nft_amount;
        public int listed_amount;
        public string floor_price;
    }
}