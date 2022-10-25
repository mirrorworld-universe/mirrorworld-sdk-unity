

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CreateSubCollectionRequest
    {
        public string collection_mint;

        public string name;

        public string symbol;

        public string url;

        public string confirmation;
    }
}