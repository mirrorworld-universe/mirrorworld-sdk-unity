
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class LoginResponse
    {
        public string access_token;

        public string refresh_token;

        public UserResponse user;
    }

    [Serializable]
    public class SolResLoginWithEmail
    {
        public string access_token;
        public string refresh_token;
        public AccessTokenUser user;
        public string type;
    }

    [Serializable]
    public class AccessTokenUser
    {
        public int id;
        public string eth_address;
        public string sol_address;
        public string sui_address;
        public string email;
        public bool email_verified;
        public string username;
        public object main_user_id;
        public bool allow_spend;
        public bool has_security;
        public string createdAt;
        public string updatedAt;
        public bool is_subaccount;
        public AccessTokenWallet wallet;
    }

    [Serializable]
    public class AccessTokenWallet
    {
        public string eth_address;
        public string sol_address;
        public string sui_address;
    }
}