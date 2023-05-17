using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResUpdateNFT
    {
        public SolResUpdateNFTObj nft;
        public string signature;
        public string status;
        public int code;
        public string message;
    }
    [System.Serializable]
    public class SolResUpdateNFTObj
    {
        public string name;
        public string symbol;
        public string uri;
        public List<SolNFTCreator> creators;
        public string updateAuthority;
        public int sellerFeeBasisPoints;
        public SolUpdateNFTCollection collection;
        public object uses;
        public bool primarySaleHappened;
        public bool isMutable;
    }

    [System.Serializable]
    public class SolNFTCreator
    {
        public string address;
        public bool verified;
        public int share;
    }

    [System.Serializable]
    public class SolUpdateNFTCollection
    {
        public bool verified;
        public string key;
        public string address;
    }
}