

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CancelNftListOnMarketplaceRequest:BaseWeb3Request
    {
        public string mint_address;

        public double price;

        public string auction_house;
    }
}