using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class UserResponse
    {
        public long id;

        public string eth_address;

        public string sol_address;

        public string email;

        public bool email_verified;

        public string username;

        public long? main_user_id;

        public bool allow_spend;

        public DateTime? createdAt;

        public DateTime? updatedAt;

        public bool is_subaccount;

        public WalletResponse wallet;
    }


    [Serializable]
    public class WalletResponse
    {
        public long id;

        public long user_id;

        public string sol_address;
        public string sui_address;
        public string eth_address;

        public DateTime? createdAt;

        public DateTime? updatedAt;
    }
}