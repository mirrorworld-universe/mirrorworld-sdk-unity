using System;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine;

/**
 * Unity SDK's API are mostly realized by c#
 * When run on Android or IOS platform,Unity SDK would use target platform's login and tokens.
 * Some API can't realized by Unity,so call them on target platform as well.
 */
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
            if (javaSDKInstance != null) javaSDKInstance.Call("SetApiKey", key);
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

        public void AndroidOpenWallet()
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("OpenWallet");
        }

        public void AndroidOpenTransferPage(string minAddress, string image, string name)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("OpenTransferPage", minAddress, image, name);
        }

        public void AndroidOpenNFTManagePage(string minAddress, string image, string name, double price)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("OpenNFTManagePage", minAddress, image,name,price);
        }

        public void AndroidOpenSellPage(string mintAddress,string image,string name)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("OpenSellPage", mintAddress,image,name);
        }

        public void AndroidOpenMarket(List<string> collections)
        {
            if (javaSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaSDKInstance.Call("OpenMarket", collections);
        }
    }
}