using UnityEngine;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class CommonApprove<T>
    {
        public string type;
        public string message;
        public string value;
        public T paramsPlaceHolder;
    }
}
