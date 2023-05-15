using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class SolResTransferNFT
    {
        public int id;
        public string type;
        public string wallet_address;
        public string mint_address;
        public string to_wallet_address;
        public string price;
        public string auction_house;
        public string signature;
        public string status;
        public string updatedAt;
        public string createdAt;
        public object seller_address;
        public SolResTransferNftInfo nft;
    }

    [System.Serializable]
    public class SolResTransferNftInfo
    {
        public int id;
        public string url;
        public string mint_address;
        public object token_id;
        public string update_authority;
        public string creator_address;
        public string name;
        public string symbol;
        public bool primary_sale_happened;
        public bool is_mutable;
        public object collection;
        public bool collection_verified;
        public int seller_fee_basis_points;
        public TransferNftMetadata metadata;
        public List<string> creators;
        public string createdAt;
        public string updatedAt;
    }

    [System.Serializable]
    public class TransferNftMetadata
    {
        public string name;
        public string image;
        public string symbol;
        public List<TransferNftAttribute> attributes;
        public TransferNftCollection collection;
        public string description;
        public int seller_fee_basis_points;
    }

    [System.Serializable]
    public class TransferNftAttribute
    {
        public string value;
        public string trait_type;
    }

    [System.Serializable]
    public class TransferNftCollection
    {
        public string name;
        public string family;
    }
}