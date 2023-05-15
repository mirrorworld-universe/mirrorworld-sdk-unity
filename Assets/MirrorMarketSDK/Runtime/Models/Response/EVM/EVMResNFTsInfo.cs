using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class NFTDataResponse
    {
        public int? total;
        public int page;
        public int page_size;
        public string cursor;
        public NFTResult[] result;
        public string status;
    }

    [System.Serializable]
    public class NFTResult
    {
        public string token_address;
        public string token_id;
        public string owner_of;
        public string block_number;
        public string block_number_minted;
        public string token_hash;
        public string amount;
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