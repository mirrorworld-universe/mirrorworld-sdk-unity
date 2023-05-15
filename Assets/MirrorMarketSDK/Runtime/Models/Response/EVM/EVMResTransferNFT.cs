using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResTransferNFT
    {
        public string from ;
        public string to ;
        public string contract_address ;
        public string contract_type ;
        public string token_id ;
        public int amount ;
        public string transaction_hash ;
        public string chain ;
        public string network ;
    }
    public class NftTransferData
    {
        public string from ;
        public string to ;
        public string contract_address ;
        public string contract_type ;
        public string token_id ;
        public string amount ;
        public string transaction_hash ;
    }

}