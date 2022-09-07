
using System;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.UI;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string apiKey = "";
        private bool debugMode = false;
        private MirrorEnv environment;

        private string accessToken = "";
        private string refreshToken = "";
        private UserResponse tmpUser = null;

        //logic
        private MonoBehaviour monoBehaviour;
        private bool inited = false;

        public void InitSDK(MonoBehaviour monoBehaviour,MirrorEnv environment,string apiKey,bool useDebug)
        {
            if (inited)
            {
                LogFlow("Unity wrapper can't be inited again.");
                return;
            }
            inited = true;
            this.environment = environment;
            this.monoBehaviour = monoBehaviour;
            SetAPIKey(apiKey);
            SetDebug(useDebug);
            LogFlow("Unity wrapper inited.");
        }

        public void SetDebug(bool debugMode)
        {
            this.debugMode = debugMode;
        }

        public void SetAPIKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return monoBehaviour;
        }

        private void SetLoginResponse(LoginResponse response)
        {
            SaveStringToLocal(localKeyRefreshToken, response.RefreshToken);
            accessToken = response.AccessToken;
        }

        public void LogFlow(string content)
        {
            if (debugMode)
            {
                Debug.Log("MirrorSDKUnity:" + content);
            }
        }

        public void LogWarn(string content)
        {
            if (debugMode)
            {
                Debug.LogWarning("MirrorSDK Warn:" + content);
            }
        }

        public UserResponse GetCurrentUser()
        {
            return tmpUser;
        }

    }

}