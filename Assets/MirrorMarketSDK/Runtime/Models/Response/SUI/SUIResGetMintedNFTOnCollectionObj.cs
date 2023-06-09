using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResGetMintedNFTOnCollectionObj
    {
        public List<string> attributes;
        public string client_id;
        public string contract_config_address;
        public string description;
        public string digest;
        public string fee_payer;
        public string image_url;
        public string name;
        public string nft_object_id;
        public string nft_object_owner;
        public string to_wallet_address;
        public long user_id;
    }
}
