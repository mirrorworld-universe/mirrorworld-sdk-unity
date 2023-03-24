using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMSearchNFTsByOwner
    {
        public string owner_address;
        //optional
        public int limit;
        public string cursor;//The cursor to be used for pagination
    }
}