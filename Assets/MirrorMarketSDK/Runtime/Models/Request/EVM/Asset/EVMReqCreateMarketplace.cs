using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MWEVMResponses
{
    [System.Serializable]
    public class EVMReqCreateMarketplace
    {
        public int seller_fee_basis_points;
        public string payment_token;
        public MWEVMResponses.EVMReqStorefrontObj storefront;
        public List<string> collections;
        public string confirmation;
    }
}

