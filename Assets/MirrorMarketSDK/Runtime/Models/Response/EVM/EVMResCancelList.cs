using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResCancelList
    {
        public NFTCancellation nft;
        public string receipt_type;
        public string order_status;
        public string seller;
        public string buyer;
        public string price;
        public string amount;
        public string payment_token;
        public string marketplace_address;
        public string order_hash;
        public string chain;
        public string network;
    }

    [System.Serializable]
    public class NFTCancellation
    {
        public string token_id;
        public string contract_address;
        public string url;
        public NFTMetadata metadata;
        public string contract_type;
    }
}
