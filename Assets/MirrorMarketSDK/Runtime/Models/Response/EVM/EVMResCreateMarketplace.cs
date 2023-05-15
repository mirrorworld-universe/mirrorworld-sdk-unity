using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResCreateMarketplace
    {
        public string name;
        public string marketplace_address;
        public string authority;
        public int seller_fee_basis_points;
        public List<string> collections;
        public string status;
    }
}
