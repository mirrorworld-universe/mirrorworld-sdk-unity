using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMReqCreateMarketplace
    {
        public int seller_fee_basis_points;
        public string payment_token;
        public MirrorWorldResponses.EVMReqStorefrontObj storefront;
        public List<string> collections;
        public string confirmation;
    }
}

