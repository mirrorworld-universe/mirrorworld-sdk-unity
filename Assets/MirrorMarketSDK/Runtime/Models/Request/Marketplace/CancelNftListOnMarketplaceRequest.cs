

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CancelNftListOnMarketplaceRequest
    {
        public string mint_address;

        public float price;

        public string confirmation;
    }
}