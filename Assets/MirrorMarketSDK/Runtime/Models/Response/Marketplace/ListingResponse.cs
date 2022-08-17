using System;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class ListingResponse
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("type")] public string Type;

        [JsonProperty("wallet_address")] public string WalletAddress;

        [JsonProperty("mint_address")] public string MintAddress;

        [JsonProperty("price")] public decimal Price;

        [JsonProperty("seller_address")] public string SellerAddress;

        [JsonProperty("signature")] public string Signature;

        [JsonProperty("status")] public string Status;

        [JsonProperty("updatedAt")] public DateTime? UpdatedAt;

        [JsonProperty("createdAt")] public DateTime? CreatedAt;

        [JsonProperty("to_wallet_address")] public string ToWalletAddress;
    }
}