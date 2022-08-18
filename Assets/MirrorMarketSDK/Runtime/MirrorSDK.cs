using System;
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
        public static GameObject mirrorSDKObject;
        #endregion logic

        //Will try to init sdk with params on prefab
        private void Awake()
        {
            if (inited)
            {
                LogFlow("Already inited,no need to init in awake flow.");
                return;
            }

            if(apiKey == "")
            {
                LogFlow("Please input an api key");
                return;
            }

            InitSDK(apiKey,gameObject);

            SetDebugMode(debugMode);
        }

        //do init sdk,you can find apikey on developer website
        public static void InitSDK(string apiKey,GameObject mirrorSDKGameObject)
        {
            if (inited)
            {
                LogFlow("please don't call InitSDK function more than one time.");
                return;
            }

            LogFlow("Mirror SDK Inited.");
            mirrorSDKObject = mirrorSDKGameObject;
            MirrorWrapper.Instance.SetApiKey(apiKey);
        }

        //set if use debug mode
        public static void SetDebugMode(bool useDebug)
        {
            LogFlow("Set debug mode to "+useDebug);
            MirrorWrapper.Instance.SetDebug(useDebug);
        }

        //open login ui
        public static void StartLogin()
        {
            if(mirrorSDKObject == null)
            {
                LogFlow("Please call InitSDK function first.");
                return;
            }

            if(Screen.width > Screen.height)
            {
                GameObject loadingObj = ResourcesUtils.Instance.LoadPrefab("PageLoginHorizontal", mirrorSDKObject.transform);
            }
            else
            {
                GameObject loadingObj = ResourcesUtils.Instance.LoadPrefab("PageLoginHorizontal", mirrorSDKObject.transform);
                LogFlow("SDK do not support vertical layout now.");
            }
        }

        public static void GetWalletAddress(Action<string> callback)
        {
            LogFlow("TODO:GetWalletAddress.Not realizated yet.");
        }

        private static void LogFlow(string content)
        {
            if (MirrorWrapper.Instance.GetDebug())
            {
                Debug.Log("MirrorSDKUnity:" + content);
            }
        }
    }
}
