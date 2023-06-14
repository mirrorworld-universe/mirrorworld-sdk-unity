using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResQueryNFT
    {
        public string id;
        public string url;
        public string name;
        public string description;
        public SUIReqMintNFTAttribute[] attributes;
        public string owner_address;
        public string package_module;
        public string package_module_class_name;
        public string collection_package_id;
    }
}
