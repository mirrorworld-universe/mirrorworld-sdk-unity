using System;
using Newtonsoft.Json;

namespace MirrorworldSDK.Models
{
    public class UserResponse
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("eth_address")] public string EthAddress;

        [JsonProperty("sol_address")] public string SolAddress;

        [JsonProperty("email")] public string Email;

        [JsonProperty("email_verified")] public bool EmailVerified;

        [JsonProperty("username")] public string Username;

        [JsonProperty("main_user_id")] public long? MainUserId;

        [JsonProperty("allow_spend")] public bool AllowSpend;

        [JsonProperty("createdAt")] public DateTime? CreatedAt;

        [JsonProperty("updatedAt")] public DateTime? UpdatedAt;

        [JsonProperty("is_subaccount")] public bool IsSubAccount;

        [JsonProperty("wallet")] public WalletResponse Wallet;
    }
    
    
    public class WalletResponse
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("user_id")] public long UserId;

        [JsonProperty("sol_address")] public string SolAddress;

        [JsonProperty("eth_address")] public string EthAddress;

        [JsonProperty("createdAt")] public DateTime? CreatedAt;

        [JsonProperty("updatedAt")] public DateTime? UpdatedAt;
    }
}