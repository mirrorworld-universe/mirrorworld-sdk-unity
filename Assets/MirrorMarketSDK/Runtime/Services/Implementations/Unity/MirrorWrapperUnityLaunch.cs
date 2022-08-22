﻿#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string apiKey = "";
        private bool debugMode = false;

        private string accessToken = "";
        private string refreshToken = "";
        private UserResponse tmpUser = null;

        private MonoBehaviour monoBehaviour;

        public void InitSDK(MonoBehaviour monoBehaviour)
        {
            LogFlow("Unity wrapper inited.");
            this.monoBehaviour = monoBehaviour;
        }

        public void SetDebug(bool debugMode)
        {
            this.debugMode = debugMode;
        }

        public void SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public bool GetDebug()
        {
            return debugMode;
        }

        public void StartLogin()
        {
            LogFlow("Start login.Waitting to login on web.");
            LoginWithEmail("suqiang@rct.studio", "yuebaobao", (response) =>
            {
                LogFlow("LoginWithEmail result is:" + response);
            });
            //todo open url
            //todo show waiting dialog
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
    }

}
#endif