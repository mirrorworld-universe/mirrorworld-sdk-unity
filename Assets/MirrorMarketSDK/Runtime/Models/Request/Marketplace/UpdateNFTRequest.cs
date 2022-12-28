using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class UpdateNFTRequest
    {
        public string mint_address;

        public string name;

        public string symbol;

        public string update_authority;

        public string url;

        public int seller_fee_basis_points;

        public string confirmation;
    }
}
