using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMReqSignTransactionAndSend
    {
        public string nonce;
        public string gasPrice;
        public string gasLimit;
        public string to;
        public string value;
        public string data;
    }
}
