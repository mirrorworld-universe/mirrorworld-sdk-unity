using System;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public class LoginListener : AndroidJavaProxy
    {
        private Action onSuccess, onFailed;

        //android接口包名不能出错：com.example.android.PluginCallback
        public LoginListener(Action onSuccess, Action onFailed) : base("com.mirror.sdk.listener.LoginListener")
        {
            this.onFailed = onFailed;
            this.onSuccess = onSuccess;
        }

        public void onLoginSuccess()
        {
            if (this.onSuccess != null) onSuccess();
        }

        public void onLoginFail()
        {
            if (this.onFailed != null) onFailed();
        }
    }
}