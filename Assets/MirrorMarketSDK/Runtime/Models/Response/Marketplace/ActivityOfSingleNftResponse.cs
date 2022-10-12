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

        public decimal sellerFeeBasisPoints;

        public string updateAuthorityAddress;

        public string description;

        public string image;

        public string externalUrl;

        public List<NFTCreatorObj> creators;

        public NFTOwnerObj owner;

        public List<NFTAttributeObj> attributes;

        public List<string> listings;
    }

    [Serializable]
    public class NFTCreatorObj
    {
        public string address;

        public bool verified;

        public decimal share;
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
        public long id { get; set; }

        public string mintAddress { get; set; }

        public string txSignature { get; set; }

        public decimal amount { get; set; }

        public string receiptType { get; set; }

        public decimal tokenPrice { get; set; }

        public DateTime? blockTimeCreated { get; set; }

        public DateTime? blockTimeCanceled { get; set; }

        public string tradeState { get; set; }

        public string auctionHouseAddress { get; set; }

        public string sellerAddress { get; set; }

        public string buyerAddress { get; set; }

        public string metadata { get; set; }

        public DateTime? blockTime { get; set; }
    }

    [Serializable]
    public class TokenTransfer
    {
        public long id { get; set; }

        public string mintAddress { get; set; }

        public string txSignature { get; set; }

        public object fromWalletAddress { get; set; }

        public string toWalletAddress { get; set; }

        public long amount { get; set; }

        public DateTime blockTime { get; set; }

        public long slot { get; set; }
    }
}