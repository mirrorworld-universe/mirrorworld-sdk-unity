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

        AndroidJavaClass javaClass;

        public void AndroidInitSDK(string apiKey,MirrorEnv env)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
                //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDK");
                //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
                //javaObject.Call("InitSDK");

                //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk.MirrorSDK");
                //javaSDKInstance = javaClass.CallStatic<AndroidJavaObject>("getInstance");
                //javaSDKInstance.Call("InitSDK", jo, bridgeUtils.GetAndroidMirrorEnv(env));

                javaClass = new AndroidJavaClass("com.mirror.sdk.MirrorWorld");
                javaClass.CallStatic("initMirrorWorld", jo, apiKey,bridgeUtils.GetAndroidMirrorEnv(env));
            }
            else
            {
                LogFlow("This is not ANDROID platform,inner logic error!");
            }
        }

        public void AndroidSetDebug(bool useDebug)
        {
            if (javaClass != null) javaClass.CallStatic("setDebug", useDebug);
        }

        public void AndroidSetLogoutCallback(Action logoutAction)
        {
            if (javaClass == null)
            {
                LogFlow("Must call InitSDK function first.");

                return;
            }
            javaClass.CallStatic("setLogoutCallback",new MSimpleCallback(()=> {

                ClearUnitySDKCache();

                logoutAction();
            }));
        }

        public void AndroidStartLogin(Action<LoginResponse> callback)
        {
            if (javaClass == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaClass.CallStatic("startLogin", new MirrorCallback((resultString)=> {

                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultString);

                SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

                callback(responseBody);
            }));
        }

        public void AndroidOpenWallet(Action walletLogoutAction)
        {
            //AndroidSetLogoutCallback(walletLogoutAction);

            if (javaClass == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaClass.CallStatic("openWallet", new MirrorCallback((resultString) => {

                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultString);

                SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

                walletLogoutAction();
            }));
        }

        public void AndroidOpenMarket()
        {
            if (javaClass == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaClass.CallStatic("openMarket");
        }
    }
}