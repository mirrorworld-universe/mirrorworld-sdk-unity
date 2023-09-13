using UnityEngine;
using System.Collections;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.UI;
using System;
using MirrorworldSDK.Models;
using MirrorworldSDK;
using static MirrorworldSDK.Wrapper.MirrorWrapper;
using System.Runtime.InteropServices;

public class MWClientWrapper
{
    /// <summary>
    /// Open login page and let user to login SDK.
    /// </summary>
    /// <param name="action"></param>
    public static void StartLogin(Action<LoginResponse> action)
    {
        MirrorWrapper.Instance.LogFlow("Start login logic...");

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

        MirrorWrapper.Instance.LogFlow("Start login in unity...");

        MirrorWrapper.Instance.GetLoginSession(MirrorWrapper.Instance.debugEmail, (startSuccess) =>
        {
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();

            if (VersionUtils.IsUnityLowerThan2018())
            {
                GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("DialogCompatible", GameObject.Find("Canvas").transform);
                IMGUIDialog iMGUIDialog = dialogCanvas.GetComponent<IMGUIDialog>();
                iMGUIDialog.InitDialog("Have you complete login on popup page?", "Yes", "Cancel", () => {
                    iMGUIDialog.SetContent("Checking login status...");
                    MirrorSDK.CompleteLoginWithSession((success) => {
                        if (success)
                        {
                            MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {
                                iMGUIDialog.SetContent("Login success!");
                                GameObject.Destroy(dialogCanvas);
                            });
                        }
                        else
                        {
                            iMGUIDialog.SetContent("Login have no response,please try again.");
                        }
                    });
                },
                () => {
                    MirrorSDK.LoginDebugClear();
                    GameObject.Destroy(dialogCanvas);
                });
            }
            else
            {
                GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("DialogCanvas", GameObject.Find("Canvas").transform);
            }

            MirrorWrapper.Instance.LogFlow("Open login page result:" + startSuccess);

        }, action);
#elif UNITY_ANDROID && !(UNITY_EDITOR)

            MirrorWrapper.Instance.LogFlow("Start login in android...");

            MirrorWrapper.Instance.AndroidStartLogin(action);

#elif UNITY_IOS && !(UNITY_EDITOR)

        MirrorWrapper.iOSLoginAction = action;
            MirrorWrapper.Instance.LogFlow("Start login in iOS...");
            IOSLoginCallback handler = new IOSLoginCallback(MirrorWrapper.iOSloginCallback);
            IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
            MirrorWrapper.IOSStartLogin(fp);
#endif
    }

    /// <summary>
    /// Set if use debug mode
    /// </summary>
    /// <param name="useDebug"></param>
    public static void SetDebugMode(bool useDebug)
    {
        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.LogFlow("IOS is not implemented.");
#endif
    }

    /// <summary>
    /// Custom the scheme name.
    /// Tips: This is only effect android, because iOS don't use the scheme name to return to app.
    /// </summary>
    /// <param name="schemeName"></param>
    public static void SetSchemeName(string schemeName)
    {
#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidSetSchemeName(schemeName);
#endif
    }

    public static MirrorEnv GetEnv()
    {
        return MirrorWrapper.Instance.GetEnvironment();
    }

    public static MirrorChain GetChain()
    {
        return MirrorWrapper.Instance.GetChain();
    }

    /// <summary>
    /// Guest login
    /// </summary>
    /// <param name="action"></param>
    public static void GuestLogin(Action<LoginResponse> action)
    {
        MirrorWrapper.Instance.GuestLogin(action);
    }

    /// <summary>
    /// Login with email,this email must registed.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="callBack"></param>
    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MirrorWrapper.Instance.LoginWithEmail(emailAddress, password, callBack);
    }

    /// <summary>
    /// User logout
    /// </summary>
    /// <param name="logoutAction"></param>
    public static void Logout(Action logoutAction)
    {
        MirrorWrapper.Instance.Logout(logoutAction);
    }

    /// <summary>
    /// To get current access token, user may visit our API by themselves with this jwt token.
    /// </summary>
    /// <returns>access token if login is successful</returns>
    public static string GetAccessToken()
    {
        return MirrorWrapper.Instance.GetAccessToken();
    }

    public static void QueryUser(string email, Action<CommonResponse<UserResponse>> callback)
    {
        MirrorWrapper.Instance.QueryUser(email, (response) =>
        {
            callback(response);
        });
    }

    public static void IsLoggedIn(Action<bool> action)
    {
        MirrorWrapper.Instance.IsLoggedIn(action);
    }

    public static void OpenWalletPage(Action walletLogoutAction)
    {
        string walletUrl = UrlUtils.GetWalletUrl();

        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenWalletPage(walletUrl, walletLogoutAction);
        }
        else
        {

#if (UNITY_ANDROID && !(UNITY_EDITOR))

             MirrorWrapper.Instance.AndroidOpenWallet(walletUrl, walletLogoutAction);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.walletLogoutAction = walletLogoutAction;
            //MirrorWrapper.OpenWallet();
            iOSWalletLogOutCallback handler = new iOSWalletLogOutCallback(MirrorWrapper.iOSWalletCallBack);
            IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);

            iOSWalletLoginTokenCallback handler2 = new iOSWalletLoginTokenCallback(MirrorWrapper.iOSWalletLoginCallback);
            IntPtr fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            MirrorWrapper.IOSOpenWallet(walletUrl, fp, fp2);
#endif
        }
    }

    public static void OpenMarketPage(string marketUrl)
    {
        MirrorWrapper.Instance.LogFlow("IOSOpenMarketPlace OpenMarketPage");
        string url = MirrorWrapper.Instance.GetMarketUrl(marketUrl);
        MirrorWrapper.Instance.LogFlow("IOSOpenMarketPlace:" + url);

        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.LogFlow("IOSOpenMarketPlace IsEditor");
            MirrorWrapper.Instance.DebugOpenMarketPage(url);
        }
        else
        {
#if (UNITY_ANDROID && !(UNITY_EDITOR))

             MirrorWrapper.Instance.AndroidOpenMarket(url);

#elif (UNITY_IOS && !(UNITY_EDITOR))
            MirrorWrapper.Instance.LogFlow("IOSOpenMarketPlace click ios"); 
            MirrorWrapper.IOSOpenMarketPlace(url);
#endif
        }
    }
}
