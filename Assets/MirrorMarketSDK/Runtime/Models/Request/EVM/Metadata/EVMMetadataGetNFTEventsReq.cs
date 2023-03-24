using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMMetadataGetNFTEventsReq
    {
        public string contract;
        public int token_id;
        public int page;
        public int page_size;
        //optional
        public string marketplace_address;
    }
}
