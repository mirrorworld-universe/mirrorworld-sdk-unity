

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CancelNftListOnMarketplaceRequest
    {
        public string mint_address;

        public decimal price;

        public string confirmation;
    }
}