using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResNFTInfo
    {
        public string token_address;
        public string token_id;
        public int[] transfer_index;
        public string owner_of;
        public string block_number;
        public string block_number_minted;
        public string token_hash;
        public string amount;
        public string updated_at;
        public string contract_type;
        public string name;
        public string symbol;
        public string token_uri;
        public string metadata;
        public string last_token_uri_sync;
        public string last_metadata_sync;
        public string minter_address;
        public bool possible_spam;
    }
}
