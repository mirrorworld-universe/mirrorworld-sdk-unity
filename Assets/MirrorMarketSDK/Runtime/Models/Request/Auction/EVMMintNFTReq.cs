using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMMintNFTReq
    {
        public string collection_address;
        public int token_id;
        //optional
        public string url;
        public string to_wallet_address;
        public int mint_amount;
        public string confirmation;
    }
}