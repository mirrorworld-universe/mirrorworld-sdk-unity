using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResMetadataGetNFTInfoAttribute
    {
        public string trait_type;
        public string value;
    }

    [System.Serializable]
    public class SolResMetadataGetNFTInfoSkillAttribute
    {
        public string trait_type;
        public string value;
        public string image;
    }

    [System.Serializable]
    public class SolResMetadataGetNFTInfoListStatus
    {
        public string marketplace_address;
        public string list_price;
        public string list_time;
        public int list_count;
        public string list_operator;
    }

    [System.Serializable]
    public class SolResMetadataGetNFTInfo
    {
        public string contract;
        public int token_id;
        public string name;
        public string owner_address;
        public string image;
        public string price;
        public bool listed;
        public List<SolResMetadataGetNFTInfoAttribute> attributes;
        public List<SolResMetadataGetNFTInfoAttribute> off_chain_attribute;
        public List<SolResMetadataGetNFTInfoSkillAttribute> skill_attributes;
        public List<SolResMetadataGetNFTInfoListStatus> list_status;
    }

}