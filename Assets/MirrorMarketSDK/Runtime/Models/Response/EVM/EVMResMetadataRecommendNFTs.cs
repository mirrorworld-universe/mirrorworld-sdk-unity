using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    //[System.Serializable]
    //public class EVMResMetadataRecommendNFTs
    //{
    //    public int code;
    //    public string status;
    //    public string message;
    //    public List<EVMRecommendNFT> data;
    //    public string trace_id;
    //}

    [System.Serializable]
    public class EVMResMetadataRecommendNFTs
    {
        public string contract;
        public int token_id;
        public string name;
        public string image;
        public string owner_address;
        public bool listed;
    }

}