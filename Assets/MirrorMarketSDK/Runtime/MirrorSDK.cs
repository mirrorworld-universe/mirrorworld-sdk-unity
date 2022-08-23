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

        #endregion settings

        #region logic
        private static bool inited = false;
        private string refreshToken = "";
        private string accessToken = "";
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

            InitSDK(apiKey,gameObject,debugMode);

            SetDebugMode(debugMode);
        }

        //do init sdk,you can find apikey on developer website
        public static void InitSDK(string apiKey,GameObject gameObject,bool useDebug)
        {
            if (inited)
            {
                MirrorWrapper.Instance.LogFlow("please don't call InitSDK function more than one time.");
                return;
            }

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
            MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            MirrorSDK.monoBehaviour = monoBehaviour;
            MirrorWrapper.Instance.InitSDK(monoBehaviour);
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

        #region marketplace

        public static void FetchSingleNFT(string mintAddress,Action<SingleNFTResponseObj> action)
        {
            MirrorWrapper.Instance.FetchSingleNft(mintAddress, action);
        }

        public static void FetchNFTsByMintAddress(List<string> mintAddresses,Action<MultipleNFTsResponse> action)
        {
            MirrorWrapper.Instance.FetchNFTsByMintAddress(mintAddresses, action);
        }

        public static void FetchNFTsByCreators(List<string> creators, Action<MultipleNFTsResponse> action)
        {
            MirrorWrapper.Instance.FetchNftsByCreators(creators, action);
        }

        public static void FetchNFTsByUpdateAuthorityAddress(List<string> updateAuthorityAddresses, Action<MultipleNFTsResponse> action)
        {
            MirrorWrapper.Instance.FetchNftsByUpdateAuthorities(updateAuthorityAddresses, action);
        }

        #endregion
    }
}
