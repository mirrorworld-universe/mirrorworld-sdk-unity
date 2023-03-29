using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMTransferNFTReq
    {
        public string collection_address;
        public int token_id;
        public string to_wallet_address;
    }
}
