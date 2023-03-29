using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMMetadataSearchNFTReq
    {
        public List<string> collections;

        public string search;
    }
}
