﻿using System;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IUnityDebugService
    {
        //keep
        public string debugEmail = "";
        public string password = "";

        //url
        private readonly string urlStartLoginSession = "auth/token/start-session";
        private readonly string urlCompleteLoginWithSession = "auth/token/complete-session/";

        //flow
        private string debugSession = "";

        public void SetDebugEmail(string email,string password)
        {
            debugEmail = email;
            this.password = password;
        }

        public void CompleteLoginWithSession(string session, Action<CommonResponse<LoginResponse>> action)
        {
            string url = GetAuthRoot() + urlCompleteLoginWithSession + session;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (rawResponseBody) => {

                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);

                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                action(responseBody);
            }));
        }

        public void GetLoginSession(string emailAddress, Action<bool> action)
        {
            GetLoginSessionRequest requestBody = new GetLoginSessionRequest();

            requestBody.emailAddress = emailAddress;

            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string url = GetAuthRoot() + urlStartLoginSession;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<GetLoginSessionResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<GetLoginSessionResponse>>(response);

                if(responseBody.Code == (long)MirrorResponseCode.Success)
                {
                    debugSession = responseBody.Data.sessionToken;

                    string session = responseBody.Data.sessionToken;

                    string url = urlDebugLoginUrlPre + session;

                    Application.OpenURL(url);

                    if(action != null) action(true);
                }
                else
                {
                    if (action != null) action(false);
                }
            }));
        }

        public void LoginDebugClear()
        {
            debugSession = "";
        }

        public string GetDebugSession()
        {
            return debugSession;
        }
    }
}
