using System;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        /**
         * Email used in Unity Editor login flow
         */
        public string debugEmail = "";

        /**
         * API only used in Editor login flow
         */
        private readonly string urlStartLoginSession = "auth/token/start-session";
        private readonly string urlCompleteLoginWithSession = "auth/token/complete-session/";

        /**
         * Stuff temp
         */
        private string debugSession = "";
        private Action<LoginResponse> loginCb = null;

        public Action walletLogoutAction;


        public void SetDebugEmail(string email)
        {
            debugEmail = email;
        }

        public void CompleteLoginWithSession(string session, Action<CommonResponse<LoginResponse>> action)
        {
            string url = GetAuthRoot() + urlCompleteLoginWithSession + session;

            monoBehaviour.StartCoroutine(Get(url, null, (rawResponseBody) => {

                CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(rawResponseBody);

                SaveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);

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

                    string pageUrl = UrlUtils.GetDebugLoginPageRoot(session);

                    Application.OpenURL(pageUrl);

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

        public void DebugOpenWalletPage(string walletUrl,Action walletCb)
        {
            walletLogoutAction = walletCb;

            IsLoggedIn((isLogged)=> {
                if (isLogged) {
                    LogFlow("Will open third browser..."+ walletUrl);
                    Application.OpenURL(walletUrl);
                }
                else
                {
                    LogFlow("Please login first.");
                }
            });
        }

        public void DebugOpenMarketPage(string url)
        {
            IsLoggedIn((isLogged) => {
                if (isLogged)
                {
                    LogFlow("Will open third browser..." + url);
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