

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CreateNftRequest
    {
        public string collection_mint;

        public string name;

        public string symbol;

        public string url;

        public string confirmation;

        public string mint_id;

        public MintPayment payment;
    }

    [Serializable]
    public class CreateNftRequestWithoutPayment
    {
        public string collection_mint;

        public string name;

        public string symbol;

        public string url;

        public string confirmation;

        public string mint_id;
    }

    [Serializable]
    public class MintPayment
    {
        public string receiver_wallet;

        public double amount_sol;
    }

    [Serializable]
    public class CreateNftRequestNoMintID
    {
        public string collection_mint;

        public string name;

        public string symbol;

        public string url;

        public string confirmation;
    }
}