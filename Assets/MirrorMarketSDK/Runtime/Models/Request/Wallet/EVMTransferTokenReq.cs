using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class EVMTransferTokenReq
    {
        public string nonce;
        public string gasPrice;
        public string gasLimit;
        public string to;
        public string amount;
        public string contract;
    }
}