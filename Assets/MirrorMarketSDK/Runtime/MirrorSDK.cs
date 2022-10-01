using System;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using UnityEngine;


public class MirrorSDK : MonoBehaviour
{
    #region settings
    [Header("AppInfo")]
    [Tooltip("You can get it on your developer management.")]
    public string apiKey = Constant.SDKDefaultAPIKeyValue;
    [Tooltip("Open debug mode")]
    public bool debugMode = false;
    [Tooltip("runtime environment")]
    public MirrorEnv environment = MirrorEnv.StagingDevNet;

    [Tooltip("Temp Attr")]
    public string debugEmail = "";
    public string password = "";
    #endregion settings


    private void Awake()
    {
        if (apiKey == "" || apiKey == "your api key")
        {
            MirrorWrapper.Instance.LogFlow("Please input an api key");
            return;
        }

        InitSDK(apiKey, gameObject, debugMode, environment);

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
        MirrorWrapper.Instance.SetDebugEmail(debugEmail, password);
#endif
    }

    public static void InitSDK(string apiKey, GameObject gameObject, bool useDebug, MirrorEnv environment)
    {
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

            MirrorWrapper.Instance.SetApiKey(apiKey);

            MirrorWrapper.Instance.LogFlow("Mirror SDK Inited.");
#endif
    }

    public static void SetAPIKey(string apiKey)
    {
#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

        MirrorWrapper.Instance.SetAPIKey(apiKey);

#elif (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidSetAPIKey(apiKey);

            MirrorWrapper.Instance.SetAPIKey(apiKey);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.SetApiKey(apiKey);

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

            MirrorWrapper.Instance.LogFlow("IOS is not implemented");
#endif
    }


    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MirrorWrapper.Instance.LoginWithEmail(emailAddress, password, callBack);
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
                callback(response.Data);
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

    public static void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack)
    {
        MirrorWrapper.Instance.MintNft(parentCollection, collectionName, collectionSymbol, collectionInfoUrl, confirmation, callBack);
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

    public static void GetNFTsOwnedByAddress(List<string> owners, Action<CommonResponse<MultipleNFTsResponse>> callBack)
    {
        MirrorWrapper.Instance.GetNFTsOwnedByAddress(owners, callBack);
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

    public static void ListNFT(string mintAddress, decimal price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.ListNFT(mintAddress, price, confirmation, callBack);
    }

    public static void CancelNFTListing(string mintAddress, decimal price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.CancelNFTListing(mintAddress, price, confirmation, callBack);
    }

    public static void UpdateNFTListing(string mintAddress, decimal price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.UpdateNFTListing(mintAddress, price, confirmation, callBack);
    }

    public static void BuyNFT(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorWrapper.Instance.BuyNFT(mintAddress, price, callBack);
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
    public static void GetWalletTransactions(decimal number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
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

            if (loginRes.Code != (long)MirrorResponseCode.Success)
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
    public static void OpenWalletPage()
    {
        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenWalletPage();
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            MirrorWrapper.Instance.AndroidOpenWallet();
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
    //public static void OpenMarketPage(List<string> collections)
    //{
    //    if (Utils.IsEditor())
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        MirrorWrapper.Instance.AndroidOpenMarket(collections);
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else
    //    {
    //        MirrorWrapper.Instance.LogFlow("Unknown platform!");
    //    }
    //}
    //public static void OpenTransferPage(string mintAddress, string image, string name)
    //{
    //    if (Utils.IsEditor())
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        MirrorWrapper.Instance.AndroidOpenTransferPage(mintAddress, image, name);
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else
    //    {
    //        MirrorWrapper.Instance.LogFlow("Unknown platform!");
    //    }
    //}
    //public static void OpenSellPage(string mintAddress, string image, string name)
    //{
    //    if (Utils.IsEditor())
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        MirrorWrapper.Instance.AndroidOpenSellPage(mintAddress, image, name);
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else
    //    {
    //        MirrorWrapper.Instance.LogFlow("Unknown platform!");
    //    }
    //}
    //public static void OpenNFTManagePage(string mintAddress, string image, string name, double price)
    //{
    //    if (Utils.IsEditor())
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else if (Application.platform == RuntimePlatform.Android)
    //    {
    //        MirrorWrapper.Instance.AndroidOpenNFTManagePage(mintAddress, image, name, price);
    //    }
    //    else if (Application.platform == RuntimePlatform.IPhonePlayer)
    //    {
    //        MirrorWrapper.Instance.LogFlow("Not supported.");
    //    }
    //    else
    //    {
    //        MirrorWrapper.Instance.LogFlow("Unknown platform!");
    //    }
    //}
    #endregion
}