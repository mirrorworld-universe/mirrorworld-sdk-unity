using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace
{
    public class ActivityOfSingleNftResponse
    {
        [JsonProperty("mintAddress")] public string MintAddress;

        [JsonProperty("auctionActivities")] public List<AuctionActivity> AuctionActivities;

        [JsonProperty("tokenTransfers")]
        public List<TokenTransfer> TokenTransfers { get; set; }
    }

    public class AuctionActivity
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mintAddress")]
        public string MintAddress { get; set; }

        [JsonProperty("txSignature")]
        public string TxSignature { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("receiptType")]
        public string ReceiptType { get; set; }

        [JsonProperty("tokenPrice")]
        public decimal TokenPrice { get; set; }

        [JsonProperty("blockTimeCreated")]
        public DateTime? BlockTimeCreated { get; set; }

        [JsonProperty("blockTimeCanceled")]
        public DateTime? BlockTimeCanceled { get; set; }

        [JsonProperty("tradeState")]
        public string TradeState { get; set; }

        [JsonProperty("auctionHouseAddress")]
        public string AuctionHouseAddress { get; set; }

        [JsonProperty("sellerAddress")]
        public string SellerAddress { get; set; }

        [JsonProperty("buyerAddress")]
        public string BuyerAddress { get; set; }

        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        [JsonProperty("blockTime")]
        public DateTime? BlockTime { get; set; }
    }

    public class TokenTransfer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mintAddress")]
        public string MintAddress { get; set; }

        [JsonProperty("txSignature")]
        public string TxSignature { get; set; }

        [JsonProperty("fromWalletAddress")]
        public object FromWalletAddress { get; set; }

        [JsonProperty("toWalletAddress")]
        public string ToWalletAddress { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("blockTime")]
        public DateTime BlockTime { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }
    }
}