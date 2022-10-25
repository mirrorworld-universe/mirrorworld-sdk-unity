using System;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public class MirrorCallback : AndroidJavaProxy
    {
        private Action<string> action;

        //android接口包名不能出错：com.example.android.PluginCallback
        public MirrorCallback(Action<string> action) : base("com.mirror.sdk.listener.universal.MirrorCallback")
        {
            this.action = action;
        }

        public void callback(string result)
        {
            this.action(result);
        }
    }
}
