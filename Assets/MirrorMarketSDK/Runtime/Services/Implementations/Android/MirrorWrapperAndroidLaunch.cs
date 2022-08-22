#if UNITY_ANDROID && !(UNITY_EDITOR)
using System;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private AndroidJavaObject javaSDKInstance;

        public void InitSDK()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDKJava");
                //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
                //javaObject.Call("InitSDK");

                AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdkjava.MirrorSDKJava");
                javaSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
                javaSDKInstance.Call("InitSDK", jo);
            }
            else
            {
                LogFlow("This is not ANDROID platform,inner logic error!");
            }
        }

        public void SetAPIKey(string key)
        {
            if (javaSDKInstance != null) javaSDKInstance.Call("SetAppID", key);
        }

        public void SetDebug(bool useDebug)
        {
            if (javaSDKInstance != null) javaSDKInstance.Call("SetDebug", useDebug);
        }

        public void StartLogin()
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }
            javaSDKInstance.Call("StartLogin");
        }

        public void StartLoginWithCallback(Action<string> callback)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }
            javaSDKInstance.Call("StartLoginWithCallback", new MirrorCallback((result) => {
                callback(result);
            }));
        }

        public void GetWalletAddress(Action<string> callback)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }


            javaSDKInstance.Call("APIGetWalletAddress", new MirrorCallback((result) => {
                LogFlow("GetWalletAddress result is:" + result);
                callback(result);
            }));
        }

        public void LogFlow(string content)
        {
            if (javaSDKInstance != null) javaSDKInstance.Call("LogFlow", content);
        }

        class MirrorCallback : AndroidJavaProxy
        {
            private Action<string> action;
            //android接口包名不能出错：com.example.android.PluginCallback
            public MirrorCallback(Action<string> action) : base("com.mirror.sdkjava.MirrorCallback")
            {
                this.action = action;
            }

            public void callback(string result)
            {
                this.action(result);
            }
        }
    }

}
#endif