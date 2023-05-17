using UnityEngine;
using System.Collections;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResBuyNFTNft
    {
        public string url;
        public string mint_address;
        public string update_authority;
        public string creator_address;
        public string name;
        public string symbol;
        public string collection;
        public bool collection_verified;
        public int seller_fee_basis_points;
    }

    [System.Serializable]
    public class SolResBuyNFT
    {
        public string type;
        public string wallet_address;
        public string mint_address;
        public string price;
        public string auction_house;
        public string signature;
        public string seller_address;
        public string to_wallet_address;
        public string status;
        public SolResBuyNFTNft nft;
    }
}
