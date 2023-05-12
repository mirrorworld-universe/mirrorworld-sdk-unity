using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMSearchNFTsByAddressesReq
    {
        public List<EVMSearchNFTsByAddressesReqToken> tokens;
    }

    [Serializable]
    public class EVMSearchNFTsByAddressesReqToken
    {
        public string token_address;
        public string token_id;
    }
}
