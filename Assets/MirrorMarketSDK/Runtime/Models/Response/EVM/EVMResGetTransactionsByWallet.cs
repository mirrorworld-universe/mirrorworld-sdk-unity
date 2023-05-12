using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMResGetTransactionsByWallet
    {
        public List<EVMTransactionItem> transactions;
        public int count;
    }

}
