using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolReqCreateMarketplace
    {
        public int seller_fee_basis_points;
        public MirrorWorldResponses.EVMReqStorefrontObj storefront;
        public List<string> collections;
    }
}

