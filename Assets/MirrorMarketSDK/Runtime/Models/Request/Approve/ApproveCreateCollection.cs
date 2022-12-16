using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class ApproveCreateCollection
    {
        public string name;
        public string symbol;
        public string url;
        public string confirmation;
        public int seller_fee_basis_points;
    }
}
    
