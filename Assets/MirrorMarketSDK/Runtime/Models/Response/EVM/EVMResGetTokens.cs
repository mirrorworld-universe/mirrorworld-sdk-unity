using UnityEngine;
using System.Collections;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMResGetTokens
    {
        public string matic;
        public EVMToken[] tokens;
    }

    [System.Serializable]
    public class EVMToken
    {

    }
}
