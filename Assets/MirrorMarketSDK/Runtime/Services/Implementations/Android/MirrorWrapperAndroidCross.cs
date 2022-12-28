using System;
using System.Collections.Generic;
using MirrorworldSDK.Models;
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

        AndroidJavaClass javaMirrorWorld;
        AndroidJavaObject mirrorSDKInstance;

        public void AndroidInitSDK(string apiKey,MirrorEnv env)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");

                javaMirrorWorld = new AndroidJavaClass("com.mirror.sdk.MirrorWorld");
                javaMirrorWorld.CallStatic("initMirrorWorld", jo, apiKey,(int)env);

                AndroidSetAuthTokenCallback();
            }
            else
            {
                LogFlow("This is not ANDROID platform,inner logic error!");
            }
        }

        public void AndroidSetAuthTokenCallback()
        {
            AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
            //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDK");
            //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
            //javaObject.Call("InitSDK");

            AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk.MirrorSDK");
            mirrorSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
            mirrorSDKInstance.Call("setAuthTokenCallback", new MirrorCallback((xAuthToken) => {
                LogFlow("Android update xAuthToken to:"+xAuthToken);
                authToken = xAuthToken;
                if(approveFinalAction != null)
                {
                    approveFinalAction();
                    approveFinalAction = null;
                }
            }));
        }

        public void AndroidSetDebug(bool useDebug)
        {
            if (javaMirrorWorld != null) javaMirrorWorld.CallStatic("setDebug", useDebug);
        }

        public void AndroidSetLogoutCallback(Action logoutAction)
        {
            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");

                return;
            }
            javaMirrorWorld.CallStatic("setLogoutCallback",new MSimpleCallback(()=> {

                ClearUnitySDKCache();

                logoutAction();
            }));
        }

        public void AndroidStartLogin(Action<LoginResponse> callback)
        {
            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaMirrorWorld.CallStatic("startLogin", new MirrorCallback((resultString)=> {

                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultString);

                SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

                callback(responseBody);
            }));
        }

        public void AndroidOpenWallet(Action walletLogoutAction)
        {
            //AndroidSetLogoutCallback(walletLogoutAction);

            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaMirrorWorld.CallStatic("openWallet", new MirrorCallback((resultString) => {

                //LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultString);

                //SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

                walletLogoutAction();
            }));
        }

        public void AndroidOpenMarket(string url)
        {
            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            LogFlow("Unity try to open market with url:" + url);

            javaMirrorWorld.CallStatic("openMarketWithWholeUrl", url);
        }
    }
}