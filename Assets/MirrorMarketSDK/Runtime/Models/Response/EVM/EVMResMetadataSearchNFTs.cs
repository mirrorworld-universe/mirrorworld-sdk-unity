using UnityEngine;
using System.Collections;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMResMetadataSearchNFTs
    {
        public string contract;
        public int token_id;
        public string name;
        public string image;
        public string owner_address;
        public bool listed;
    }
}