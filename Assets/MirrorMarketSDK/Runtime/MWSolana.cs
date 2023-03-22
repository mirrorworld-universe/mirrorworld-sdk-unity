using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;

namespace MirrorworldSDK
{
    public class MWSolana
    {
        public static void StartLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.StartLogin(action);
        }

        public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            MWClientWrapper.LoginWithEmail(emailAddress,password,callBack);
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

        public static void QueryUser(string email, Action<CommonResponse<UserResponse>> callback)
        {
            MWClientWrapper.QueryUser(email,callback);
        }

        public static void OpenWallet(Action walletLogoutAction)
        {
            MWClientWrapper.OpenWalletPage(walletLogoutAction);
        }

        public static void OpenMarket(string marketUrl)
        {
            MWClientWrapper.OpenMarketPage(marketUrl);
        }


    }
}
