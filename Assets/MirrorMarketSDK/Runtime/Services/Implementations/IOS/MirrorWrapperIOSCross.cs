#if (UNITY_IOS && !(UNITY_EDITOR))
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
        public static extern void IOSInitSDK(int env, int chain, string apikey);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void IOSLoginCallback(string resultString);

        [MonoPInvokeCallback(typeof(IOSLoginCallback))]
        public static void iOSloginCallback(string resultStr)
        {
            MirrorWrapper.Instance.LogFlow("iOSloginCallback received:" + resultStr);
            resultStr = MirrorUtils.GetNoSymbolString(resultStr);
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

        //--
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void iOSWalletLoginTokenCallback(string resultString);

        [MonoPInvokeCallback(typeof(iOSWalletLoginTokenCallback))]
        public static void iOSWalletLoginCallback(string resultStr)
        {
            MirrorWrapper.Instance.LogFlow("iOSWallet - loginCallback received:" + resultStr);
            LoginResponse responseBody = JsonUtility.FromJson<LoginResponse>(resultStr);
            MirrorWrapper.Instance.LogFlow("iOSWallet - loginCallback parse result:" + responseBody.access_token);
            MirrorWrapper.Instance.LogFlow("iOSWallet - loginCallback parse result:" + responseBody.refresh_token);

            MirrorWrapper.Instance.SaveKeyParams(responseBody.access_token, responseBody.refresh_token, responseBody.user);

        }
        //--

        [DllImport("__Internal")]
        public static extern void IOSOpenWallet(string walletUrl, IntPtr iOSWalletCallBack, IntPtr iOSWalletLoginCallback);


        [DllImport("__Internal")]
        public static extern void IOSOpenMarketPlace(string url);


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void IOSSecurityAuthCallback(string resultString);

        /// <summary>
        /// Pass the approve token and run callback.
        /// </summary>
        /// <param name="xAuthToken"></param>
        [MonoPInvokeCallback(typeof(IOSSecurityAuthCallback))]
        public static void IOSGetSecurityAuthToken(string xAuthToken)
        {
            MirrorWrapper.Instance.LogFlow("Android update xAuthToken to:" + xAuthToken);
            MirrorWrapper.Instance.authToken = xAuthToken;
            if (MirrorWrapper.Instance.approveFinalAction != null)
            {
                MirrorWrapper.Instance.approveFinalAction();
                MirrorWrapper.Instance.approveFinalAction = null;
            }
        }

        [DllImport("__Internal")]
        public static extern void IOSOpenUrl(string url);

        [DllImport("__Internal")]
        public static extern void IOSOpenUrlSetCallBack(IntPtr IOSGetSecurityAuthToken);

    }
}
        
#endif