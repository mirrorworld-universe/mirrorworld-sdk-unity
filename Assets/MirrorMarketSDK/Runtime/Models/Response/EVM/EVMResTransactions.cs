using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResTransactions
    {
        public EVMTransactionItem[] transactions;
        public int count;
    }

    [System.Serializable]
    public class EVMTransactionItem
    {
        public string type;
        public string from;
        public string to;
        public string value;
        public string hash;
        public string blockTimestamp;
        public string contract;
        public string tokenId;
    }
}
