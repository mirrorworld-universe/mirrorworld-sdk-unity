
using System;
using System.Collections;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlLoginWithEmail = "auth/login";
        private readonly string urlRefreshToken = "auth/refresh-token";
        private readonly string urlGetCurrentUser = "auth/me";
        private readonly string urlQueryUser = "auth/user";
        private readonly string urlLogout = "auth/logout";
        private readonly string urlGuestLogin = "auth/guest-login";

        public void GetCurrentUserInfo(Action<CommonResponse<UserResponse>> callBack)
        {
            string url = GetAuthRoot() + urlGetCurrentUser;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {
                CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);
                SaveCurrentUser(responseBody.data);
                callBack(responseBody);
            }));
        }



        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            BasicEmailLoginRequest requestBody = new BasicEmailLoginRequest();
            requestBody.email = emailAddress;
            requestBody.password = password;
            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlLoginWithEmail;

            monoBehaviour.StartCoroutine(Post(url, rawRequestBody, (rawResponseBody) =>
            {
                CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(rawResponseBody);

                SaveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);

                callBack(responseBody);
            }));
        }

        public void Logout(Action action)
        {
            string url = GetAuthRoot() + urlLogout;
            monoBehaviour.StartCoroutine(CheckAndPost(url, null, (response) => {
                //CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);
                action();
                ClearUnitySDKCache();
            }));
        }

        public void QueryUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            string url = GetAuthRoot() + urlQueryUser + "?email=" + email;
            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {
                CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);
                callBack(responseBody);
            }));
        }

        public IEnumerator DoGetAccessToken(Action<bool> action)
        {
            if (action == null)
            {
                LogFlow("DoGetAccessToken action is null. Seems logic is wrong.");
                yield break;
            }

            if (refreshToken == "")
            {
                refreshToken = GetStringFromLocal(localKeyRefreshToken);
                if (refreshToken == "")
                {
                    LogFlow("Try to get access token but there is no refresh token local. Maybe the cache is cleared. Please login again.");
                    action(false);
                    yield break;
                }
            }

            if (apiKey == "")
            {
                LogFlow("Try to get access token but there is no api key, please set it first!");
                action(false);
                yield break;
            }

            string url = GetAuthRoot() + urlRefreshToken;

            UnityWebRequest request = new UnityWebRequest(url, "GET");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);
            MirrorUtils.SetRefreshToken(request, refreshToken);

            request.downloadHandler = new DownloadHandlerBuffer();
            //request.timeout = MWConfig.MaxHttpTimeout;

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(rawResponseBody);

            if (responseBody.code.Equals((int)MirrorResponseCode.Success))
            {
                LogFlow("GetAccessToken success");

                SaveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);

                if(action != null)
                {
                    action(true);
                }
            }
            else
            {
                LogFlow("GetAccessToken failed: code:" + responseBody.code + " reason:" + responseBody.error);

                if (action != null)
                {
                    action(false);
                }
            }

        }

        public void GetAccessToken(Action<bool> action)
        {
            monoBehaviour.StartCoroutine(DoGetAccessToken(action));
        }

        public void IsLoggedIn(Action<bool> action)
        {
            string url = GetAuthRoot() + urlGetCurrentUser;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                LogFlow("IsLoggedIn result:"+ response);

                CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);

                if(responseBody.code == (long)MirrorResponseCode.Success)
                {
                    SaveCurrentUser(responseBody.data);
                }

                action(responseBody.code == (long)MirrorResponseCode.Success);

            }));
        }

        public void GuestLogin(Action<LoginResponse> action)
        {
            string url = GetAuthRoot() + urlGuestLogin;

            monoBehaviour.StartCoroutine(Get(url, null, (response) => {

                LogFlow("GuestLogin result:" + response);

                CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(response);

                if (responseBody.code == (long)MirrorResponseCode.Success)
                {
                    SaveKeyParams(responseBody.data.access_token,responseBody.data.refresh_token,responseBody.data.user);

                    action(responseBody.data);
                }
            }));
        }
    }
}