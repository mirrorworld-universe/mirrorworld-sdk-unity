﻿using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using AOT;
using System;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        public static Action<LoginResponse> iOSLoginAction;

        [DllImport("__Internal")]
        public static extern void initSDK(string apikey);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void LoginCallback(string resultString);

        [MonoPInvokeCallback(typeof(LoginCallback))]
        public static void iOSloginCallback(string resultStr)
        {
            MirrorWrapper.Instance.LogFlow("iOSloginCallback:" + resultStr);
            if(iOSLoginAction != null)
            {
                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultStr);
                MirrorWrapper.Instance.SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);
                if(iOSLoginAction != null)
                {
                    iOSLoginAction(responseBody);
                    iOSLoginAction = null;
                }
            }
        }

        [DllImport("__Internal")]
        public static extern void StartLogin(IntPtr iOSloginCallback);

        [DllImport("__Internal")]
        public static extern void OpenWallet();

        //wallet logout callback

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void iOSWalletLoginCallback(string resultString);

        [MonoPInvokeCallback(typeof(iOSWalletLoginCallback))]
        static void iOSWalletLogoutCallback()
        {
            MirrorWrapper.Instance.LogFlow("iOSWalletLogoutCallback");
        }

        [DllImport("__Internal")]
        static extern void SetWalletLogoutCallback(IntPtr iOSWalletLogoutCallback);
    }
}
    