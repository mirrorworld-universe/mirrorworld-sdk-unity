using System;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using UnityEngine;


namespace MirrorworldSDK
{
    public partial class MirrorSDK : MonoBehaviour
    {
        #region settings
        [Header("AppInfo")]
        [Tooltip("You can get it on your developer management.")]
        public string apiKey = "your api key";
        [Tooltip("Open debug mode")]
        public bool debugMode = false;
        [Tooltip("runtime environment")]
        public Environment environment = Environment.StagingDevnet;

        [Tooltip("Temp Attr")]
        public string debugEmail = "";
        public string password = "";
        public static string sdebugEmail = "";
        public static string spassword = "";

        #endregion settings

        #region logic
        private static bool inited = false;
        public static MonoBehaviour monoBehaviour;
        #endregion logic

        //Will try to init sdk with params on prefab
        private void Awake()
        {
            if (inited)
            {
                MirrorWrapper.Instance.LogFlow("Already inited,no need to init in awake flow.");
                return;
            }

            if(apiKey == "")
            {
                MirrorWrapper.Instance.LogFlow("Please input an api key");
                return;
            }

            //for test
            spassword = password;
            sdebugEmail = debugEmail;

            InitSDK(apiKey,gameObject,debugMode, environment);

            SetDebugMode(debugMode);
        }

        //do init sdk,you can find apikey on developer website
        public static void InitSDK(string apiKey,GameObject gameObject,bool useDebug,Environment environment)
        {
            if (inited)
            {
                MirrorWrapper.Instance.LogFlow("please don't call InitSDK function more than one time.");
                return;
            }

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
            MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            MirrorSDK.monoBehaviour = monoBehaviour;
            MirrorWrapper.Instance.InitSDK(monoBehaviour,environment);
            MirrorWrapper.Instance.SetApiKey(apiKey);
            MirrorWrapper.Instance.SetDebug(useDebug);
#elif UNITY_ANDROID && !(UNITY_EDITOR)
            MirrorWrapper.Instance.InitSDK();
            MirrorWrapper.Instance.SetAPIKey(apiKey);
            MirrorWrapper.Instance.SetDebug(useDebug);
#elif UNITY_IOS && !(UNITY_EDITOR)
            MirrorWrapper.Instance.SetApiKey(apiKey);
            MirrorWrapper.Instance.LogFlow("Mirror SDK Inited.");
#endif
        }

        //set if use debug mode
        public static void SetDebugMode(bool useDebug)
        {
            MirrorWrapper.Instance.LogFlow("Set debug mode to "+useDebug);
            MirrorWrapper.Instance.SetDebug(useDebug);
        }

        //open login ui
        public static void StartLogin()
        {
            MirrorWrapper.Instance.StartLogin();
        }

        public static void GetWalletAddress(Action<string> callback)
        {
            UserResponse user = MirrorWrapper.Instance.GetCurrentUser();
            if (user != null)
            {
                MirrorWrapper.Instance.LogFlow("Have old current user,use old data.");
                callback(user.SolAddress);
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("No old current user,try to get data.");
                MirrorWrapper.Instance.GetCurrentUserInfo((response)=> {
                    callback(response.Data.SolAddress);
                });
            }
        }

        public static void GetAccessToken()
        {
            MirrorWrapper.Instance.GetAccessToken();
        }

        public static void QueryUser(string email, Action<UserResponse> callback)
        {
            MirrorWrapper.Instance.QueryUser(email, (response) =>
            {
                callback(response.Data);
            });
        }

        public static void IsLoggedIn(Action<bool> action)
        {
            MirrorWrapper.Instance.IsLoggedIn(action);
        }

        #region mint

        public static void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack)
        {
            MirrorWrapper.Instance.MintNft(parentCollection,collectionName,collectionSymbol,collectionInfoUrl,callBack);
        }

        public static void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack)
        {
            MirrorWrapper.Instance.CreateVerifiedCollection(collectionName, collectionSymbol, collectionInfoUrl, callBack);
        }

        public static void CreateVerifiedSubCollection(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack)
        {
            MirrorWrapper.Instance.CreateVerifiedSubCollection(parentCollection, collectionName, collectionSymbol, collectionInfoUrl, callBack);
        }

        #endregion

        #region marketplace

        public static void FetchSingleNFT(string mintAddress,Action<SingleNFTResponseObj> action)
        {
            MirrorWrapper.Instance.FetchSingleNFT(mintAddress, action);
        }

        public static void FetchNFTsByMintAddress(List<string> mintAddresses,Action<MultipleNFTsResponse> action)
        {
            MirrorWrapper.Instance.FetchNFTsByMintAddresses(mintAddresses, action);
        }

        public static void FetchNFTsByCreators(List<string> creators, Action<MultipleNFTsResponse> action)
        {
            MirrorWrapper.Instance.FetchNftsByCreatorAddresses(creators, action);
        }

        public static void FetchNftsByUpdateAuthorities(List<string> updateAuthorityAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
        {
            MirrorWrapper.Instance.FetchNftsByUpdateAuthorities(updateAuthorityAddresses, action);
        }

        public static void ListNFT(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack)
        {
            MirrorWrapper.Instance.ListNFT(mintAddress,price,callBack);
        }

        public static void CancelNFTListing(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack)
        {
            MirrorWrapper.Instance.CancelNFTListing(mintAddress, price, callBack);
        }

        public static void UpdateNFTListing(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack)
        {
            MirrorWrapper.Instance.UpdateNFTListing(mintAddress, price, callBack);
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
            MirrorWrapper.Instance.GetWalletTransactions(number,nextBefore,action);
        }
        public static void GetWalletTransactionsBySignatrue(string signature, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature,action);
        }
        public static void TransferSol(ulong amout, string publicKey, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MirrorWrapper.Instance.TransferSol(amout,publicKey,callBack);
        }
        public static void TransferToken(ulong amout, string publicKey, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            MirrorWrapper.Instance.TransferToken(amout,publicKey,callBack);
        }
        #endregion
    }
}
