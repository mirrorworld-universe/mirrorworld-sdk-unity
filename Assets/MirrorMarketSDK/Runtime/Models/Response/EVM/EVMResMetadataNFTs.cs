using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResMetadataNFTs
    {
        public int total_page;
        public int page_size;
        public List<EVMMetadataNFT> nfts;
    }
    [System.Serializable]
    public class EVMMetadataNFT
    {
        public string contract;
        public int token_id;
        public string name;
        public string image;
        public string price;
        public string owner_address;
        public bool listed;
    }
}