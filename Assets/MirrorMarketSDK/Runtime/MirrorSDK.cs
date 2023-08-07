using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using UnityEngine;
using static MirrorworldSDK.Wrapper.MirrorWrapper;

public class MirrorSDK : MonoBehaviour
{
    #region settings
    [Header("AppInfo")]
    [Tooltip("You can get it on your developer management.")]
    public string apiKey = Constant.SDKDefaultAPIKeyValue;
    [Tooltip("Open debug mode")]
    public bool debugMode = false;
    [Tooltip("runtime environment")]
    public MirrorEnvPublic environment = MirrorEnvPublic.ProductionDevnet;
    [Tooltip("Chain")]
    public MirrorChain chain = MirrorChain.Solana;

    [Tooltip("Temp Attr")]
    public string debugEmail = "";
    #endregion settings


    private void Awake()
    {
        if (apiKey == "" || apiKey == "your api key")
        {
            MirrorWrapper.Instance.LogFlow("Please input an api key");
            return;
        }
        Debug.Log("Unity apikey:"+apiKey);
        MirrorEnv env = MirrorEnv.Devnet;
        if(environment == MirrorEnvPublic.ProductionMainnet)
        {
            env = MirrorEnv.Mainnet;
        }
        InitSDK(apiKey, gameObject, chain, debugMode, env);

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
        MirrorWrapper.Instance.SetDebugEmail(debugEmail);
#endif
    }

    public static void InitSDK(string apiKey, GameObject gameObject ,MirrorChain chain, bool useDebug, MirrorEnv environment)
    {
        Debug.Log("env:"+ environment);

        DontDestroyOnLoad(gameObject);

        MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

        MirrorWrapper.Instance.InitSDK(monoBehaviour, environment, chain, apiKey, useDebug);

        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidInitSDK(apiKey,environment,chain);
            
            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.IOSInitSDK((int)environment,(int)chain,apiKey);

            MirrorWrapper.Instance.LogFlow("Mirror SDK Inited.");
#endif
    }

    public static void SetChain(MirrorChain chain)
    {
        MirrorWrapper.Instance.SetChain(chain);
    }

    public static void SetEnvironment(MirrorEnv env)
    {
        MirrorWrapper.Instance.SetEnvironment(env);
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

    public static void GuestLogin(Action<LoginResponse> action)
    {
        MirrorWrapper.Instance.GuestLogin(action);
    }

    /// <summary>
    /// Open login page.
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
                iMGUIDialog.InitDialog("Have you complete login on popup page?","Yes","Cancel",()=> {
                    iMGUIDialog.SetContent("Checking login status...");
                    MirrorSDK.CompleteLoginWithSession((success) => {
                        if (success)
                        {
                            MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {
                                iMGUIDialog.SetContent("Login success!");
                                Destroy(dialogCanvas);
                            });
                        }
                        else
                        {
                            iMGUIDialog.SetContent("Login have no response,please try again.");
                        }
                    });
                },
                ()=> {
                    MirrorSDK.LoginDebugClear();
                    Destroy(dialogCanvas);
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
    /// Login with email,this email must registed.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="callBack"></param>
    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MirrorWrapper.Instance.LoginWithEmail(emailAddress, password, callBack);
    }

    public static void Logout(Action logoutAction)
    {
        MirrorWrapper.Instance.Logout(logoutAction);
    }

    public static string GetWallet()
    {
        UserResponse user = MirrorWrapper.Instance.GetCurrentUser();
        Debug.Log("MirrorSDK get wallet:" + user);
        if(user == null)
        {
            return "";
        }
        return user.wallet.sol_address;

        //UserResponse user = MirrorWrapper.Instance.GetCurrentUser();
        //if (user != null)
        //{
        //    MirrorWrapper.Instance.LogFlow("Have old current user,use old data.");
        //    callback(user);
        //}
        //else
        //{
        //    MirrorWrapper.Instance.LogFlow("No old current user,try to get data.");
        //    MirrorWrapper.Instance.GetCurrentUserInfo((response) => {
        //        callback(response.data);
        //    });
        //}
    }

    public static void GetAccessToken(Action<bool> action)
    {
        MirrorWrapper.Instance.GetAccessToken(action);
    }

    public static void IsLoggedIn(Action<bool> action)
    {
        MirrorWrapper.Instance.IsLoggedIn(action);
    }

    #region unity debug flow

    public static void CompleteLoginWithSession(Action<bool> action)
    {
        string token = MirrorWrapper.Instance.GetDebugSession();

        if (token == "")
        {
            MirrorWrapper.Instance.LogFlow("Please start debug login first.");

            return;
        }

        MirrorWrapper.Instance.CompleteLoginWithSession(token, (loginRes) => {

            if (loginRes.code != (long)MirrorResponseCode.Success)
            {
                MirrorWrapper.Instance.LogFlow("Login failed.");

                action(false);
            }
            else
            {
                action(true);
            }
        });
    }

    public static void LoginDebugClear()
    {
        MirrorWrapper.Instance.LoginDebugClear();
    }

    #endregion

    #region market ui
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
        string url = Instance.GetMarketUrl(marketUrl);
        MirrorWrapper.Instance.LogFlow("IOSOpenMarketPlace:"+url);

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
    #endregion
}
