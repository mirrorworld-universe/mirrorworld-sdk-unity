using System;
namespace MirrorworldSDK.Models
{
    [Serializable]
    public class SolResMintResponse
    {
        public string mint_address;

        public string url;

        public string update_authority;

        public string creator_address;

        public string name;

        public string symbol;

        public string collection;

        public int seller_fee_basis_points;

        public string signature;

        public string status;
    }
}