using mirrorworld_sdk_unity.Runtime.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
{
    public class AndroidService : MonoBehaviour, IAndroidService
    {

        #region settings

        [Header("AppInfo")] [Tooltip("You can get it on your developer management.")]
        public string apiKey = "your api key";

        [Tooltip("Open debug mode")] public bool debugMode = false;

        private static string mApiKey = "";
        private static bool mDebugMode = false;

        #endregion settings

        #region logic

        private static AndroidJavaObject mSDKInstance;

        #endregion logic

        private void Awake()
        {
            mApiKey = apiKey;
            mDebugMode = debugMode;
            InitSDK();
        }

        public void InitSDKWithParams(string apiKey, bool isDebug)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                mApiKey = apiKey;
                mDebugMode = isDebug;

                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdkjava.MirrorSDKJava");
                mSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
                mSDKInstance.Call("SetAppID", apiKey);
                mSDKInstance.Call("SetDebug", isDebug);
                mSDKInstance.Call("InitSDK", jo);
            }
            else
            {
                Debug.LogWarning("Don't support platform");
            }
        }

        private void InitSDK()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

                AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdkjava.MirrorSDKJava");
                mSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
                mSDKInstance.Call("SetAppID", apiKey);
                mSDKInstance.Call("SetDebug", debugMode);
                mSDKInstance.Call("InitSDK", jo);
            }
            else
            {
                Debug.LogWarning("Don't support platform");
            }
        }

        public void SetAPIKey(string key)
        {
            mApiKey = key;
            if (mSDKInstance != null) mSDKInstance.Call("SetAppID", mApiKey);
        }

        public void SetDebugMode(bool useDebug)
        {
            mDebugMode = useDebug;
            if (mSDKInstance != null) mSDKInstance.Call("SetDebug", mDebugMode);
        }

        public void StartLogin()
        {
            if (mSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            mSDKInstance.Call("StartLogin");
        }

        public void StartLoginWithCallback(Action<string> callback)
        {
            if (mSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            mSDKInstance.Call("StartLoginWithCallback", new MirrorCallback((result) => { callback(result); }));
        }

        public void GetWalletAddress(Action<string> callback)
        {
            if (mSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }


            mSDKInstance.Call("APIGetWalletAddress", new MirrorCallback((result) =>
            {
                LogFlow("GetWalletAddress result is:" + result);
                callback(result);
            }));
        }

        private static void LogFlow(string content)
        {
            if (mDebugMode)
            {
                Debug.Log("MirrorSDKUnity:" + content);
            }
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