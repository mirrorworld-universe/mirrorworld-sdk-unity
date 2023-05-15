using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
	[System.Serializable]
	public class EVMResMintCollection
	{
        public int id;
        public string seed;
        public string hash;
        public string signature;
        public string contract_address;
        public string contract_type;
        public string url;
        public string name;
        public string symbol;
        public string creator_address;
        public bool track_mint;
        public bool burn_mint;
        public bool mint_enabled;
        public int mint_start_id;
        public int mint_end_id;
        public int mint_amount;
        public string status;
        public string transaction_hash;
        public string client_id;
        public string mint_id;
        public string createdAt;
        public string updatedAt;
    }
}

