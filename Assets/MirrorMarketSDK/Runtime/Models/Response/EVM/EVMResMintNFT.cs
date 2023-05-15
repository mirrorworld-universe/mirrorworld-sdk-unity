using UnityEngine;
using System.Collections;
using System;

namespace MirrorWorldResponses
{
    [Serializable]
    public class EVMResMintNFT
    {
        public int id;
        public string seed;
        public string hash;
        public string signature;
        public string creator_address;
        public string to_wallet_address;
        public string contract_address;
        public string contract_type;
        public int token_id;
        public int mint_amount;
        public string status;
        public string client_id;
        public string mint_id;
        public string url;
        public string updatedAt;
        public string createdAt;
        public string transaction_hash;
    }
}

