using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMBuyNFTReq
    {
        public string collection_address;
        public int token_id;
        public double price;
        public string marketplace_address;
    }
}

