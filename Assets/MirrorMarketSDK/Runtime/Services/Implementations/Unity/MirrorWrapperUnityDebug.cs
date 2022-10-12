using System;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IUnityDebugService
    {
        //keep
        public string debugEmail = "";

        //url
        private readonly string urlStartLoginSession = "auth/token/start-session";
        private readonly string urlCompleteLoginWithSession = "auth/token/complete-session/";
        private readonly string urlDebugLoginUrlPre = "https://auth-staging.mirrorworld.fun/login?session=";

        //flow
        private string debugSession = "";
        private Action<LoginResponse> loginCb = null;

        public void SetDebugEmail(string email)
        {
            debugEmail = email;
        }

        public void CompleteLoginWithSession(string session, Action<CommonResponse<LoginResponse>> action)
        {
            string url = GetAuthRoot() + urlCompleteLoginWithSession + session;

            monoBehaviour.StartCoroutine(Get(url, null, (rawResponseBody) => {

                CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(rawResponseBody);

                saveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);

                action(responseBody);

                bool loginSuccess = responseBody.code == (long)MirrorResponseCode.Success;

                SaveCurrentUser(responseBody.data.user);

                if (loginCb != null) loginCb(responseBody.data);
            }));
        }

        public void GetLoginSession(string emailAddress, Action<bool> openBrowerResult, Action<LoginResponse> loginCb)
        {
            this.loginCb = loginCb;

            GetLoginSessionRequest requestBody = new GetLoginSessionRequest();

            requestBody.email = emailAddress;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlStartLoginSession;

            if(apiKey == "" || apiKey == Constant.SDKDefaultAPIKeyValue)
            {
                LogFlow("No api key,please set it.");
                return;
            }

            monoBehaviour.StartCoroutine(Post(url, rawRequestBody, (response) => {

                CommonResponse<GetLoginSessionResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetLoginSessionResponse>>(response);

                if(responseBody.code == (long)MirrorResponseCode.Success)
                {
                    debugSession = responseBody.data.session_token;

                    string session = responseBody.data.session_token;

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
            fakeRes.access_token = accessToken;
            fakeRes.refresh_token = refreshToken;
            fakeRes.user = GetCurrentUser();

            return fakeRes;
        }
    }
}