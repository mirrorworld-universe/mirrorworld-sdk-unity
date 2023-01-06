

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CreateCollectionRequest
    {
        public string name;

        public string symbol;

        public string url;

        public string comfirmation;

        public int seller_fee_basis_points;
    }
}