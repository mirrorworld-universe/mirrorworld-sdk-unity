using System;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private AndroidBridgeUtils bridgeUtils = new AndroidBridgeUtils();

        private AndroidJavaObject javaSDKInstance;

        public void AndroidInitSDK(MirrorEnv env)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDK");
                //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
                //javaObject.Call("InitSDK");

                AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk.MirrorSDK");
                javaSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
                javaSDKInstance.Call("InitSDK", jo, bridgeUtils.GetAndroidMirrorEnv(env));
            }
            else
            {
                LogFlow("This is not ANDROID platform,inner logic error!");
            }
        }

        public void AndroidSetAPIKey(string key)
        {
            if (javaSDKInstance != null) javaSDKInstance.Call("SetAppID", key);
        }

        public void AndroidSetDebug(bool useDebug)
        {
            if (javaSDKInstance != null) javaSDKInstance.Call("SetDebug", useDebug);
        }

        public void AndroidStartLogin()
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }
            javaSDKInstance.Call("StartLogin");
        }

        public void AndroidStartLogin(Action<bool> callback)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("StartLogin", new MirrorCallback((resultString)=> {

                LoginResponse responseBody = JsonConvert.DeserializeObject<LoginResponse>(resultString);

                saveKeyParams(responseBody.AccessToken, responseBody.RefreshToken, responseBody.UserResponse);

                callback(true);
            }));
        }

        public string AndroidAccessToken()
        {
            return accessToken;
        }

        public string AndroidGetRefreshToken()
        {
            return refreshToken;
        }
    }
}