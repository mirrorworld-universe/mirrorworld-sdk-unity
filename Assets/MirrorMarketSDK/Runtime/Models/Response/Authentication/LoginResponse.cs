using System;
using Newtonsoft.Json;

namespace mirrorworld_sdk_unity.Runtime.Models.Response.Authentication
{
    public class LoginResponse
    {
        [JsonProperty("access_token")] public string AccessToken;

        [JsonProperty("refresh_token")] public string RefreshToken;

        [JsonProperty("user")] public User User;
    }

    public class User
    {
        [JsonProperty("id")] public long Id;

        [JsonProperty("eth_address")] public string EthAddress;

        [JsonProperty("sol_address")] public string SolAddress;

        [JsonProperty("email")] public string Email;

        [JsonProperty("email_verified")] public bool EmailVerified;

        [JsonProperty("username")] public string Username;

        [JsonProperty("main_user_id")] public string MainUserId;

        [JsonProperty("allow_spend")] public bool AllowSpend;

        [JsonProperty("createdAt")] public DateTime CreatedAt;

        [JsonProperty("updatedAt")] public DateTime UpdatedAt;

        [JsonProperty("is_subaccount")] public bool IsSubAccount;
    }
}