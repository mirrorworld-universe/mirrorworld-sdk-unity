using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIResGetTokensToken
    {
        public string coinType;
        public int coinObjectCount;
        public string totalBalance;
        public object lockedBalance;
    }

    [System.Serializable]
    public class SUIResGetTokens
    {
        public string sui;
        public SUIResGetTokensToken[] tokens;
    }
}