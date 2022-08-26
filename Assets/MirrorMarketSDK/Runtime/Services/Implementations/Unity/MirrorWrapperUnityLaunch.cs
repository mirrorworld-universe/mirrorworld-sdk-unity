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
        private Environment environment;

        private string accessToken = "";
        private string refreshToken = "";
        private UserResponse tmpUser = null;

        private MonoBehaviour monoBehaviour;

        public void InitSDK(MonoBehaviour monoBehaviour,Environment environment)
        {
            LogFlow("Unity wrapper inited.");
            this.environment = environment;
            this.monoBehaviour = monoBehaviour;
        }

        public void SetDebug(bool debugMode)
        {
            this.debugMode = debugMode;
        }

        public void SetAPIKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public bool GetDebug()
        {
            return debugMode;
        }

        public void StartLogin()
        {
            //test
            LogFlow("Start login.Waitting to login on web.but not implemented.Please use debug email.");
            LoginWithEmail(MirrorSDK.sdebugEmail, MirrorSDK.spassword, (response) =>
            {
                long resCode = response.Code;
                LoginResponse tokenResponse = response.Data;
                string error = response.Error;
                string message = response.Message;
                string status = response.Status;
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

        public UserResponse GetCurrentUser()
        {
            return tmpUser;
        }
    }

}
#endif