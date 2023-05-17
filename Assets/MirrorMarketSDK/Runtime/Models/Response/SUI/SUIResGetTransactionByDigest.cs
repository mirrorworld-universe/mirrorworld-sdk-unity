using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResGetTransactionByDigest
    {
        public string digest;
        public object transaction;
        public object effects;
        public object[] events;
        public object[] objectChanges;
        public object[] balanceChanges;
        public string timestampMs;
        public string checkpoint;
    }
}
