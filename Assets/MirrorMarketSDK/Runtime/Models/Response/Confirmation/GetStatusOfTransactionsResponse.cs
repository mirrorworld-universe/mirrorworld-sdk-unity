using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetStatusOfTransactionsResponse
    {
        public long id;

        public string signature;

        public string wallet_address;

        public string client_id;

        public string status;

        public string type;

        public string mint_address;

        public string auction_house;

        public string createdAt;

        public string updatedAt;
    }
}
