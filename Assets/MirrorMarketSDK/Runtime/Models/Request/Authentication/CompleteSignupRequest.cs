

using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CompleteSignupRequest
    {
        public string email;

        public long code;

        public string password;
    }
}