using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResMetadataNFTInfo
    {
        public string contract;
        public int token_id;
        public string name;
        public string[] owner_address;
        public string image;
        public string price;
        public bool listed;
        public string[] attributes;
        public string[] off_chain_attribute;
        public string[] skill_attributes;
    }
}