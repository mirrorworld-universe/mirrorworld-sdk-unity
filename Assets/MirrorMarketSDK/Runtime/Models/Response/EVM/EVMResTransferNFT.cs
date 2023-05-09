using UnityEngine;
using System.Collections;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMResTransferNFT
    {
        public string from { get; set; }
        public string to { get; set; }
        public string contract_address { get; set; }
        public string contract_type { get; set; }
        public string token_id { get; set; }
        public int amount { get; set; }
        public string transaction_hash { get; set; }
        public string chain { get; set; }
        public string network { get; set; }
    }

}