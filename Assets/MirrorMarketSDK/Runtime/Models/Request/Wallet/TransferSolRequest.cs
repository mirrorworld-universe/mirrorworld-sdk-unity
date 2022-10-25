
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class TransferSolRequest : BaseWeb3Request
    {
        public string to_publickey;

        public ulong amount;
    }
}