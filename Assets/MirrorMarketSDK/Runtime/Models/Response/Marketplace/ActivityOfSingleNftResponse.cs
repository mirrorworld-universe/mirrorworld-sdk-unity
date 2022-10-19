using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class SingleNFTResponse
    {
        public SingleNFTResponseObj nft;
    }

    [Serializable]
    public class MultipleNFTsResponse
    {
        public List<SingleNFTResponseObj> nfts;
    }

    [Serializable]
    public class SingleNFTResponseObj
    {
        public string name;

        public float sellerFeeBasisPoints;

        public string updateAuthorityAddress;

        public string description;

        public string image;

        public string externalUrl;

        public List<NFTCreatorObj> creators;

        public NFTOwnerObj owner;

        public List<NFTAttributeObj> attributes;

        public List<Listing> listings;

        public string mintAddress;
    }

    [Serializable]
    public class NFTCreatorObj
    {
        public string address;

        public bool verified;

        public float share;
    }

    [Serializable]
    public class NFTAttributeObj
    {
        public string trait_type;

        public string value;
    }

    [Serializable]
    public class NFTOwnerObj
    {
        public string address;
    }

    [Serializable]
    public class ActivityOfSingleNftResponse
    {
        public string mintAddress;

        public List<AuctionActivity> auctionActivities;

        public List<TokenTransfer> tokenTransfers { get; set; }
    }

    [Serializable]
    public class AuctionActivity
    {
        public long id;

        public string mintAddress;

        public string txSignature;

        public float amount;

        public string receiptType;

        public float tokenPrice;

        public DateTime? blockTimeCreated;

        public DateTime? blockTimeCanceled;

        public string tradeState;

        public string auctionHouseAddress;

        public string sellerAddress;

        public string buyerAddress;

        public string metadata;

        public DateTime? blockTime;
    }

    [Serializable]
    public class TokenTransfer
    {
        public long id;

        public string mintAddress;

        public string txSignature;

        public object fromWalletAddress;

        public string toWalletAddress;

        public long amount;

        public DateTime blockTime;

        public long slot;
    }

    [Serializable]
    public class Listing
    {

        public long id;

        public string tradeState;

        public string seller;

        public string metadata;

        public string purchaseId;

        public float price;

        public float tokenSize;

        public string createdAt;

        public string canceledAt;

        public AuctionHouse auctionHouse;
    }

    [Serializable]
    public class AuctionHouse
    {

        public string address;

        public string authority;

        public string treasuryMint;

        public float sellerFeeBasisPoints;
    }
}