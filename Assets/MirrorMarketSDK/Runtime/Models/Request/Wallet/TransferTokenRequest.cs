

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class TransferTokenRequest : BaseWeb3Request
    {
        public string to_publickey;

        public ulong amount;

        public string token_mint;

        public int decimals;
    }
}