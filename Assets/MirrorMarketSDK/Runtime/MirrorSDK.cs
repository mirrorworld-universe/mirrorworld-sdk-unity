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
        InitSDK(apiKey, gameObject, debugMode, (MirrorEnv)environment);

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
        MirrorWrapper.Instance.SetDebugEmail(debugEmail);
#endif
    }

    public static void InitSDK(string apiKey, GameObject gameObject, bool useDebug, MirrorEnv environment)
    {
        //Test
        //environment = MirrorEnv.StagingDevNet;

        if (environment == MirrorEnv.StagingDevNet || environment == MirrorEnv.StagingMainNet)
        {
            Debug.LogError("Environment error!");
        }

        DontDestroyOnLoad(gameObject);

        MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

        MirrorWrapper.Instance.InitSDK(monoBehaviour, environment, apiKey, useDebug);

        MirrorWrapper.Instance.SetAPIKey(apiKey);

        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidInitSDK(environment);

            MirrorWrapper.Instance.AndroidSetAPIKey(apiKey);
            
            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            // MirrorWrapper.initSDK(apiKey);

            MirrorWrapper.IOSInitSDK((int)environment,apiKey);

            MirrorWrapper.Instance.LogFlow("Mirror SDK Inited.");
#endif


    }

    //set if use debug mode
    public static void SetDebugMode(bool useDebug)
    {
        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.LogFlow("IOS is not implemented.");
#endif
    }

    //open login ui
    public static void StartLogin(Action<LoginResponse> action)
    {
        MirrorWrapper.Instance.LogFlow("Start login logic...");

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

        MirrorWrapper.Instance.LogFlow("Start login in unity...");

        MirrorWrapper.Instance.IsLoggedIn((logged)=> {
            if (logged)
            {
                LoginResponse loginResponse = MirrorWrapper.Instance.GetFakeLoginResponse();

                if (action != null) action(loginResponse);
            }
            else
            {
                MirrorWrapper.Instance.GetLoginSession(MirrorWrapper.Instance.debugEmail, (startSuccess) => {

                    MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();

                    GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("DialogCanvas", monoBehaviour.transform);

                    MirrorWrapper.Instance.LogFlow("Open login page result:" + startSuccess);

                }, action);
            }
        });
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


    //open login ui
//    public static void SetWalletLogoutCallback(Action action)
//    {
//        MirrorWrapper.Instance.LogFlow("SetWalletLogoutCallback.");

//#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

//        MirrorWrapper.Instance.LogFlow("SetLogoutCallback only implemented on native.");

//#elif UNITY_ANDROID && !(UNITY_EDITOR)

//            MirrorWrapper.Instance.LogFlow("SetLogoutCallback in android...");

//            MirrorWrapper.Instance.AndroidSetLogoutCallback(action);

//#elif UNITY_IOS && !(UNITY_EDITOR)

//            MirrorWrapper.Instance.LogFlow("IOS is not implemented");
//#endif
//    }

    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MirrorWrapper.Instance.LoginWithEmail(emailAddress, password, callBack);
    }

    public static void Logout(Action logoutAction)
    {
        MirrorWrapper.Instance.Logout(logoutAction);
    }

    public static void GetWallet(Action<UserResponse> callback)
    {
        UserResponse user = MirrorWrapper.Instance.GetCurrentUser();
        if (user != null)
        {
            MirrorWrapper.Instance.LogFlow("Have old current user,use old data.");
            callback(user);
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("No old current user,try to get data.");
            MirrorWrapper.Instance.GetCurrentUserInfo((response) => {
                callback(response.data);
            });
        }
    }

    public static void GetAccessToken()
    {
        MirrorWrapper.Instance.GetAccessToken();
    }

    public static void FetchUser(string email, Action<CommonResponse<UserResponse>> callback)
    {
        MirrorWrapper.Instance.FetchUser(email, (response) =>
        {
            callback(response);
        });
    }

    public static void IsLoggedIn(Action<bool> action)
    {
        MirrorWrapper.Instance.IsLoggedIn(action);
    }

    #region mint

    public static void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, string mint_id,Action<CommonResponse<MintResponse>> callBack)
    {
        MirrorWrapper.Instance.MintNft(parentCollection, collectionName, collectionSymbol, collectionInfoUrl, confirmation, mint_id, callBack);
    }

    public static void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack)
    {
        MirrorWrapper.Instance.CreateVerifiedCollection(collectionName, collectionSymbol, collectionInfoUrl, confirmation, callBack);
    }

    public static void CreateVerifiedSubCollection(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack)
    {
        MirrorWrapper.Instance.CreateVerifiedSubCollection(parentCollection, collectionName, collectionSymbol, collectionInfoUrl, confirmation, callBack);
    }

    #endregion

    #region marketplace

    public static void GetNFTDetails(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
    {
        MirrorWrapper.Instance.GetNFTDetails(mintAddress, action);
    }

    public static void GetActivityOfSingleNFT(string mintAddress, Action<CommonResponse<ActivityOfSingleNftResponse>> action)
    {
        MirrorWrapper.Instance.GetActivityOfSingleNFT(mintAddress, action);
    }

    public static void GetNFTsOwnedByAddress(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
    {
        MirrorWrapper.Instance.GetNFTsOwnedByAddress(owners,limit,offset, callBack);
    }

    public static void FetchNFTsByMintAddress(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNFTsByMintAddresses(mintAddresses, action);
    }

    public static void FetchNFTsByCreatorAddresses(List<string> creators, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNftsByCreatorAddresses(creators, action);
    }

    public static void FetchNFTsByUpdateAuthorities(List<string> updateAuthorityAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNftsByUpdateAuthorities(updateAuthorityAddresses, action);
    }

    public static void ListNFT(string mintAddress, float price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.ListNFT(mintAddress, price, confirmation, callBack);
    }

    public static void ListNFT(string mintAddress, float price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.ListNFT(mintAddress, price, auction_house, confirmation, callBack);
    }

    public static void CancelNFTListing(string mintAddress, float price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.CancelNFTListing(mintAddress, price, confirmation, callBack);
    }

    public static void CancelNFTListing(string mintAddress, float price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.CancelNFTListing(mintAddress, price, auction_house, confirmation, callBack);
    }

    public static void UpdateNFTListing(string mintAddress, float price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.UpdateNFTListing(mintAddress, price, confirmation, callBack);
    }


    public static void UpdateNFTListing(string mintAddress, float price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.UpdateNFTListing(mintAddress, price, auction_house, confirmation, callBack);
    }

    public static void BuyNFT(string mintAddress, float price, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.BuyNFT(mintAddress, price, callBack);
    }

    public static void BuyNFT(string mintAddress, float price, string auction_house, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.BuyNFT(mintAddress, price, auction_house, callBack);
    }

    public static void TransferNFT(string mintAddress, string toWallet, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.TransferNFT(mintAddress, toWallet, callBack);
    }

    #endregion

    #region Wallet
    public static void GetWalletTokens(Action<CommonResponse<WalletTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTokens(action);
    }
    public static void GetWalletTransactions(float number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTransactions(number, nextBefore, action);
    }
    public static void GetWalletTransactionsBySignatrue(string signature, Action<CommonResponse<TransferTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, action);
    }
    public static void TransferSol(ulong amout, string publicKey, string confirmation, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        MirrorWrapper.Instance.TransferSol(amout, publicKey, confirmation, callBack);
    }
    public static void TransferSPLToken(ulong amout, string publicKey, Action<CommonResponse<TransferTokenResponse>> callBack)
    {
        MirrorWrapper.Instance.TransferSPLToken(amout, publicKey, callBack);
    }
    #endregion

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
        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenWalletPage(walletLogoutAction);
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            MirrorWrapper.Instance.AndroidOpenWallet(walletLogoutAction);
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            MirrorWrapper.Instance.walletLogoutAction = walletLogoutAction;
            //MirrorWrapper.OpenWallet();
            iOSWalletLogOutCallback handler = new iOSWalletLogOutCallback(MirrorWrapper.iOSWalletCallBack);
             IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
             MirrorWrapper.IOSOpenWallet (fp);
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("Unknown platform!");
        }
    }

    public static void OpenMarketPage()
    {
        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenMarketPage();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            MirrorWrapper.Instance.AndroidOpenMarket();
        }
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            MirrorWrapper.Instance.LogFlow("Not supported.");
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("Unknown platform!");
        }
    }
    #endregion
}