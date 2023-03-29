using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMCancelNFTReq
    {
        public string collection_address;
        public int token_id;
        public string marketplace_address;
    }
}
