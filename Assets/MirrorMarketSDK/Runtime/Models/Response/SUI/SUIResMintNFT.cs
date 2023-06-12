using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResMintNFT
    {
        public string name;
        public string description;
        public string image_url;
        public List<SUIReqMintNFTAttribute> attributes;
        public string digest;
        public string contract_config_address;
        public string to_wallet_address;
        public string fee_payer;
        public string nft_object_id;
        public string nft_object_owner;
        public int user_id;
        public string client_id;
    }
}