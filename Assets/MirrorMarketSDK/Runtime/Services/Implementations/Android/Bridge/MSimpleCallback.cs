using System;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public class MSimpleCallback : AndroidJavaProxy
    {
        private Action action;

        //android接口包名不能出错：com.example.android.PluginCallback
        public MSimpleCallback(Action action) : base("com.mirror.sdk.listener.universal.MSimpleCallback")
        {
            this.action = action;
        }

        public void callback()
        {
            this.action();
        }
    }
}
