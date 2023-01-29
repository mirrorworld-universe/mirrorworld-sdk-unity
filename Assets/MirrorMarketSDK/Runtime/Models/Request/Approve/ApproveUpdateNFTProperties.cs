using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class ApproveUpdateNFTProperties
    {
        public string mint_address;
        public string name;
        public string symbol;
        public string update_authority;
        public string url;
        public string confirmation;
        public int seller_fee_basis_points;
    }
}

