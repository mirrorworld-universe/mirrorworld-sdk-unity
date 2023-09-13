using UnityEngine;
using System.Collections;
using MirrorworldSDK;
using System;
using MirrorworldSDK.Models;
using MirrorworldSDK.Wrapper;

namespace MirrorWorld
{
    public class MWSDK
    {
        public static MirrorWorldSolana Solana = new MirrorWorldSolana();
        public static MirrorWorldEthereum Ethereum = new MirrorWorldEthereum();
        public static MirrorWorldPolygon Polygon = new MirrorWorldPolygon();
        public static MirrorWorldBNB BNB = new MirrorWorldBNB();
        public static MirrorWorldSUI SUI = new MirrorWorldSUI();

        //Authentication APIs
        public static void StartLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.StartLogin(action);
        }

        public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            MWClientWrapper.LoginWithEmail(emailAddress, password, callBack);
        }

        public static void IsLogged(Action<bool> action)
        {
            MWClientWrapper.IsLoggedIn(action);
        }

        public static void GuestLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.GuestLogin(action);
        }

        public static void Logout(Action logoutAction)
        {
            MWClientWrapper.Logout(logoutAction);
        }

        public static string GetAccessToken()
        {
            return MWClientWrapper.GetAccessToken();
        }

        public static void SetSchemeName(string schemeName)
        {
            MWClientWrapper.SetSchemeName(schemeName);
        }

        //Client APIs
        public static void InitSDK(string apiKey, GameObject gameObject, MirrorChain chain, bool useDebug, MirrorEnv environment)
        {
            MirrorSDK.InitSDK(apiKey,gameObject,chain,useDebug,environment);
        }

        public static void OpenWallet(Action walletLogoutAction)
        {
            MWClientWrapper.OpenWalletPage(walletLogoutAction);
        }

        public static void OpenMarket(string marketUrl)
        {
            MWClientWrapper.OpenMarketPage(marketUrl);
        }

        public static void QueryUser(string email, Action<CommonResponse<UserResponse>> callback)
        {
            MWClientWrapper.QueryUser(email, callback);
        }

        public static void DebugLog(string content)
        {
            MirrorWrapper.Instance.LogFlow(content);
        }

        public static MirrorEnv GetEnvironment()
        {
            return MirrorWrapper.Instance.GetEnvironment();
        }

        public static MirrorChain GetChain()
        {
            return MirrorWrapper.Instance.GetChain();
        }
    }
}
    