using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetStatusOfTransactionsResponse
    {
        public List<GetStatusOfTransactionsResponseObj> txs;
    }

    [Serializable]
    public class GetStatusOfTransactionsResponseObj
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
