
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
}