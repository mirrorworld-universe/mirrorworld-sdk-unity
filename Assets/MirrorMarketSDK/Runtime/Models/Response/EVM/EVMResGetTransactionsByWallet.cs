using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResGetTransactionsByWallet
    {
        public List<EVMTransactionItem> transactions;
        public int count;
    }

}
