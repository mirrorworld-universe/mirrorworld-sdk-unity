using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIReqMintCollection
    {
        public string name;
        public string symbol;
        public List<string> creators;
        public string description;
    }
}