using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MWEVMResponses
{
    [System.Serializable]
    public class SolReqCreateMarketplace
    {
        public int seller_fee_basis_points;
        public MWEVMResponses.EVMReqStorefrontObj storefront;
        public List<string> collections;
    }
}

