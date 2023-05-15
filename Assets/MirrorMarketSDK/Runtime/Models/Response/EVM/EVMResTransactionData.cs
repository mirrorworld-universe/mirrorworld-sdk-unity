using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResTransactionData
    {
        public string blockHash;
        public int blockNumber;
        public string from;
        public int gas;
        public string gasPrice;
        public string maxFeePerGas;
        public string maxPriorityFeePerGas;
        public string hash;
        public string input;
        public int nonce;
        public string to;
        public int transactionIndex;
        public string value;
        public int type;
        public List<string> accessList;
        public string chainId;
        public string v;
        public string r;
        public string s;
    }
}