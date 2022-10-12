
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class UpdateNftListOnMarketplaceRequest
    {
        public string mint_address;

        public decimal price;
    }
}