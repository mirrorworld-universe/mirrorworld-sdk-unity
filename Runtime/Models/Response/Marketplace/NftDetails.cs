using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace
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

    public partial class Attribute
    {
        [JsonProperty("trait_type")] public string TraitType;

        [JsonProperty("value")] public object Value;
    }

    public partial class Creator
    {
        [JsonProperty("address")] public string Address;

        [JsonProperty("verified")] public bool Verified;

        [JsonProperty("share")] public long Share;
    }

    public partial class Listing
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("tradeState")] public string TradeState;

        [JsonProperty("seller")] public string Seller;

        [JsonProperty("metadata")] public string Metadata;

        [JsonProperty("purchaseId")] public object PurchaseId;

        [JsonProperty("price")] public string Price;

        [JsonProperty("tokenSize")] public long TokenSize;

        [JsonProperty("createdAt")] public DateTime CreatedAt;

        [JsonProperty("canceledAt")] public DateTime? CanceledAt;
    }

    public partial class Owner
    {
        [JsonProperty("address")] public string Address;
    }
}