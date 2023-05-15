using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResBuyNFTNft
    {
        public string token_id;
        public string contract_address;
        public string url;
        public string metadata;
        public string contract_type;
    }

    [System.Serializable]
    public class EVMResBuyNFT
    {
        public EVMResBuyNFTNft nft;
        public string receipt_type;
        public string order_status;
        public string seller;
        public string buyer;
        public string price;
        public string amount;
        public string marketplace_address;
        public string order_hash;
        public string chain;
        public string network;
    }
}