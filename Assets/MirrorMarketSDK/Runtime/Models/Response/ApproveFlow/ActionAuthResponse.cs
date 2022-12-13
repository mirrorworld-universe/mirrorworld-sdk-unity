using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper {
    [Serializable]
    public class ActionAuthResponse
    {
        public int id;
        public string uuid;
        public string client_id;
        public long user_id;
        public string status;
        public string type;
        public string signature;
        public string message;
        public int value;
        public string origin;
    }

}
