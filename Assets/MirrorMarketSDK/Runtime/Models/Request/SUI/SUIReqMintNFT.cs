using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIReqMintNFT
    {
        public string collection_address;
        public string name;
        public string description;
        public string image_url;
        public List<SUIReqMintNFTAttribute> attributes;
        public string to_wallet_address;
    }

    [System.Serializable]
    public class SUIReqMintNFTAttribute
    {
        public string key;
        public string value;
    }
}