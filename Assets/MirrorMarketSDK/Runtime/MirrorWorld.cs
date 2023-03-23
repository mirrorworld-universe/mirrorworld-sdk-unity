using UnityEngine;
using System.Collections;
using MirrorworldSDK;
using System;
using MirrorworldSDK.Models;

public class MirrorWorld
{
    public MWSolanaWrapper Solana;

    public MWEVMWrapper EVM;

    /// <summary>
    /// Open login page and let user to login SDK.
    /// </summary>
    /// <param name="action"></param>
    public static void StartLogin(Action<LoginResponse> action)
    {
        MWClientWrapper.StartLogin(action);
    }

    /// <summary>
    /// Set if use debug mode
    /// </summary>
    /// <param name="useDebug"></param>
    public static void SetDebugMode(bool useDebug)
    {
        MWClientWrapper.SetDebugMode(useDebug);
    }

    public static MirrorEnv GetEnv()
    {
        return MWClientWrapper.GetEnv();
    }

    public static MirrorChain GetChain()
    {
        return MWClientWrapper.GetChain();
    }

    /// <summary>
    /// Guest login
    /// </summary>
    /// <param name="action"></param>
    public static void GuestLogin(Action<LoginResponse> action)
    {
        MWClientWrapper.GuestLogin(action);
    }

    /// <summary>
    /// Login with email,this email must registed.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="callBack"></param>
    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MWClientWrapper.LoginWithEmail(emailAddress, password, callBack);
    }

    /// <summary>
    /// User logout
    /// </summary>
    /// <param name="logoutAction"></param>
    public static void Logout(Action logoutAction)
    {
        MWClientWrapper.Logout(logoutAction);
    }

    public static void QueryUser(string email, Action<CommonResponse<UserResponse>> callback)
    {
        MWClientWrapper.QueryUser(email, (response) =>
        {
            callback(response);
        });
    }

    public static void IsLogged(Action<bool> action)
    {
        MWClientWrapper.IsLoggedIn(action);
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
