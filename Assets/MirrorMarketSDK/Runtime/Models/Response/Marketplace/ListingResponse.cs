using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class ListingResponse
    {
        public long id;

        public string type;

        public string wallet_address;

        public string mint_address;

        public string price;

        public string seller_address;

        public string signature;

        public string status;

        public DateTime? updatedAt;

        public DateTime? createdAt;

        public string to_wallet_address;
    }
}