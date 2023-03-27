﻿using System;
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
        //private AndroidBridgeUtils bridgeUtils = new AndroidBridgeUtils();

        AndroidJavaClass javaMirrorWorld;
        AndroidJavaObject mirrorSDKInstance;
        AndroidJavaObject unitActivity;

        public void AndroidInitSDK(string apiKey,MirrorEnv env, MirrorChain chain)
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                unitActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");

                string packageName;
                if(chain == MirrorChain.Solana)
                {
                    packageName = "com.mirror.sdk.solana.MirrorWorld";
                }
                else
                {
                    packageName = "com.mirror.sdk.evm.MirrorWorld";
                }

                javaMirrorWorld = new AndroidJavaClass(packageName);

                string enumStrOnAndroid;
                if(env == MirrorEnv.Mainnet)
                {
                    enumStrOnAndroid = "MainNet";
                }
                else if (env == MirrorEnv.Devnet)
                {
                    enumStrOnAndroid = "DevNet";
                }
                else
                {
                    enumStrOnAndroid = "";
                    LogUtils.LogFlow("Unknown net:"+env);
                }
                AndroidJavaClass ajc = new AndroidJavaClass("com.mirror.sdk.constant.MirrorEnv");
                AndroidJavaObject loginP = ajc.GetStatic<AndroidJavaObject>(enumStrOnAndroid);

                javaMirrorWorld.CallStatic("initSDK", unitActivity, apiKey, loginP);

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

            AndroidJavaClass mirrorSDK = new AndroidJavaClass("com.mirror.sdk.MirrorSDK");
            mirrorSDKInstance = mirrorSDK.CallStatic<AndroidJavaObject>("getInstance");
            mirrorSDKInstance.Call("setAuthTokenCallback", new MirrorCallback((xAuthToken) => {
                LogFlow("Android update xAuthToken to:"+xAuthToken);
                authToken = xAuthToken;
                if(approveFinalAction != null)
                {
                    approveFinalAction();
                    approveFinalAction = null;
                }
            }));
            AndroidSetConstantLoginCb((loginResponse)=> {
                SaveKeyParams(loginResponse.access_token,loginResponse.refresh_token);
            });
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

            }), unitActivity);
        }

        public void AndroidOpenWallet(string url,Action walletLogoutAction)
        {
            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaMirrorWorld.CallStatic("openWallet", unitActivity, new MirrorCallback((resultString) => {
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

            AndroidOpenUrl(url);
        }

        public void AndroidOpenUrl(string url)
        {
            if (javaMirrorWorld == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            javaMirrorWorld.CallStatic("openUrl", url, unitActivity);
        }

        public void AndroidSetConstantLoginCb(Action<LoginResponse> action)
        {
            if (mirrorSDKInstance == null)
            {
                LogFlow("Must call InitSDK function first.");
                return;
            }

            mirrorSDKInstance.Call("setConstantLoginStringCallback", new MirrorCallback((resultString) => {

                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultString);

                SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

                if (action != null)
                {
                    LogFlow("Constant login callback runs.");

                    action(responseBody);
                }
            }));
        }
    }
}