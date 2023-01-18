using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class WalletTokenResponse
    {
        public List<Token> tokens;

        public ulong sol;
    }


    [Serializable]
    public class Token
    {
        public string ata;

        public string mint;

        public ulong amount;

        public int decimals;
    }
}