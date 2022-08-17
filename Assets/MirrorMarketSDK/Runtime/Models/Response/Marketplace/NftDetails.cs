using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class NftDetails
    {
        [JsonProperty("name")] public string Name;

        [JsonProperty("sellerFeeBasisPoints")] public long SellerFeeBasisPoints;

        [JsonProperty("updateAuthorityAddress")]
        public string UpdateAuthorityAddress;

        [JsonProperty("description")] public string Description;

        [JsonProperty("image")] public string Image;

        [JsonProperty("externalUrl")] public string ExternalUrl;

        [JsonProperty("creators")] public List<Creator> Creators;

        [JsonProperty("owner")] public Owner Owner;

        [JsonProperty("attributes")] public List<Attribute> Attributes;

        [JsonProperty("listings")] public List<Listing> Listings;
    }

    public class Attribute
    {
        [JsonProperty("trait_type")] public string TraitType;

        [JsonProperty("value")] public object Value;
    }

    public class Creator
    {
        [JsonProperty("address")] public string Address;

        [JsonProperty("verified")] public bool Verified;

        [JsonProperty("share")] public long Share;
    }

    public class Listing
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("tradeState")] public string TradeState;

        [JsonProperty("seller")] public string Seller;

        [JsonProperty("metadata")] public string Metadata;

        [JsonProperty("purchaseId")] public long? PurchaseId;

        [JsonProperty("price")] public decimal Price;

        [JsonProperty("tokenSize")] public decimal TokenSize;

        [JsonProperty("createdAt")] public DateTime? CreatedAt;

        [JsonProperty("canceledAt")] public DateTime? CanceledAt;
    }

    public class Owner
    {
        [JsonProperty("address")] public string Address;
    }
}