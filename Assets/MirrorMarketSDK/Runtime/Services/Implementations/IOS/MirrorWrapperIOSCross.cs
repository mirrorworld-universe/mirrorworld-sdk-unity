using UnityEngine;
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
        public static extern void IOSInitSDK(int env, string apikey);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void IOSLoginCallback(string resultString);

        [MonoPInvokeCallback(typeof(IOSLoginCallback))]
        public static void iOSloginCallback(string resultStr)
        {
            MirrorWrapper.Instance.LogFlow("iOSloginCallback received:" + resultStr);
            if (iOSLoginAction != null)
            {
                LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultStr);
                MirrorWrapper.Instance.LogFlow("iOSloginCallback parse result:" + responseBody.access_token);
                MirrorWrapper.Instance.LogFlow("iOSloginCallback parse result:" + responseBody.refresh_token);
                MirrorWrapper.Instance.SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);
                if (iOSLoginAction != null)
                {
                    iOSLoginAction(responseBody);
                    iOSLoginAction = null;
                }
            }
        }

        [DllImport("__Internal")]
        public static extern void IOSStartLogin(IntPtr iOSloginCallback);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void iOSWalletLogOutCallback(string resultString);

        [MonoPInvokeCallback(typeof(iOSWalletLogOutCallback))]
        public static void iOSWalletCallBack(string resultStr)
        {
            MirrorWrapper.Instance.LogFlow("iOS Wallet Logout:" + resultStr);
            if (MirrorWrapper.Instance.walletLogoutAction != null)
            {
                MirrorWrapper.Instance.LogFlow("iOS Wallet Logout runs");

                MirrorWrapper.Instance.walletLogoutAction();

                MirrorWrapper.Instance.walletLogoutAction = null;
            }
        }

        [DllImport("__Internal")]
        public static extern void IOSOpenWallet(IntPtr iOSWalletCallBack);
    }
}
        
