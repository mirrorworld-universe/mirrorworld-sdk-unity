

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CancelNftListOnMarketplaceRequest:BaseWeb3Request
    {
        public string mint_address;

        public float price;

        public string auction_house;
    }
}