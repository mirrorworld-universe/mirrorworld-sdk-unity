using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SUIReqTransferToken
    {
        public string to_publickey;
        public int amount;
        public string token;
    }
}
