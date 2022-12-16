
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class ListNftOnMarketplaceRequest : BaseWeb3Request
    {
        public string mint_address;

        public string price;

        public string auction_house;
    }
}