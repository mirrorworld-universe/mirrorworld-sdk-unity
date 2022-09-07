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
                //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDKJava");
                //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
                //javaObject.Call("InitSDK");

                AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdkjava.MirrorSDKJava");
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

                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(resultString);

                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                if(responseBody.Code == (long)MirrorResponseCode.Success)
                {
                    callback(true);
                }
                else
                {
                    callback(false);
                }
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