using System;
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
        private readonly string urlDebugLoginUrlPre = "https://auth-staging.mirrorworld.fun/login?session=";

        //flow
        private string debugSession = "";
        private Action<LoginResponse> loginCb = null;

        public void SetDebugEmail(string email,string password)
        {
            debugEmail = email;
            this.password = password;
        }

        public void CompleteLoginWithSession(string session, Action<CommonResponse<LoginResponse>> action)
        {
            string url = GetAuthRoot() + urlCompleteLoginWithSession + session;

            monoBehaviour.StartCoroutine(Get(url, null, (rawResponseBody) => {

                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);

                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                action(responseBody);

                bool loginSuccess = responseBody.Code == (long)MirrorResponseCode.Success;

                SaveCurrentUser(responseBody.Data.UserResponse);

                if (loginCb != null) loginCb(responseBody.Data);
            }));
        }

        public void GetLoginSession(string emailAddress, Action<bool> openBrowerResult, Action<LoginResponse> loginCb)
        {
            this.loginCb = loginCb;

            GetLoginSessionRequest requestBody = new GetLoginSessionRequest();

            requestBody.emailAddress = emailAddress;

            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string url = GetAuthRoot() + urlStartLoginSession;

            if(apiKey == "" || apiKey == Constant.SDKDefaultAPIKeyValue)
            {
                LogFlow("No api key,please set it.");
                return;
            }

            monoBehaviour.StartCoroutine(Post(url, rawRequestBody, (response) => {

                CommonResponse<GetLoginSessionResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<GetLoginSessionResponse>>(response);

                if(responseBody.Code == (long)MirrorResponseCode.Success)
                {
                    debugSession = responseBody.Data.sessionToken;

                    string session = responseBody.Data.sessionToken;

                    string url = urlDebugLoginUrlPre + session;

                    Application.OpenURL(url);

                    if(openBrowerResult != null) openBrowerResult(true);
                }
                else
                {
                    if (openBrowerResult != null) openBrowerResult(false);
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

        public void DebugOpenWalletPage()
        {
            IsLoggedIn((isLogged)=> {
                if (isLogged) {
                    string url = GetEntranceRoot() + apiKey;
                    LogFlow("Will open third browser..."+url);
                    Application.OpenURL(url);
                }
                else
                {
                    LogFlow("Please login first.");
                }
            });
        }

        public LoginResponse GetFakeLoginResponse()
        {
            if(accessToken == null || accessToken == "")
            {
                LogFlow("No access token yet!");
                return null;
            }
            if (refreshToken == null || refreshToken == "")
            {
                LogFlow("No refresh token yet!");
                return null;
            }
            if (GetCurrentUser() == null)
            {
                LogFlow("No user data yet!");
                return null;
            }

            LoginResponse fakeRes = new LoginResponse();
            fakeRes.AccessToken = accessToken;
            fakeRes.RefreshToken = refreshToken;
            fakeRes.UserResponse = GetCurrentUser();

            return fakeRes;
        }
    }
}