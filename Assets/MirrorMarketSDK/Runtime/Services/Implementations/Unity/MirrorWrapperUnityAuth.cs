
using System;
using System.Collections;
using MirrorworldSDK;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IAuthenticationService
    {
        private readonly string urlLoginWithEmail = "auth/login";
        private readonly string urlRefreshToken = "auth/refresh-token";
        private readonly string urlGetCurrentUser = "auth/me";
        private readonly string urlQueryUser = "auth/user";

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

                saveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);

                callBack(responseBody);
            }));
        }

        public void FetchUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            string url = GetAuthRoot() + urlQueryUser + "?email=" + email;
            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {
                CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);
                callBack(responseBody);
            }));
        }

        public IEnumerator DoGetAccessToken()
        {
            if (refreshToken == "")
            {
                refreshToken = GetStringFromLocal(localKeyRefreshToken);
                if (refreshToken == "")
                {
                    LogFlow("Try to get access token but there is no refresh token local.Seems logic is wrong.");
                    yield break;
                }
            }

            if (apiKey == "")
            {
                LogFlow("Try to get access token but there is no api key.Seems logic is wrong.");
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

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody = JsonUtility.FromJson<CommonResponse<LoginResponse>>(rawResponseBody);

            if (responseBody.code.Equals((int)MirrorResponseCode.Success))
            {
                LogFlow("GetAccessToken success");

                saveKeyParams(responseBody.data.access_token, responseBody.data.refresh_token, responseBody.data.user);
            }
            else
            {
                LogFlow("GetAccessToken failed: code:" + responseBody.code + " reason:" + responseBody.error);
            }

        }

        public void GetAccessToken()
        {
            monoBehaviour.StartCoroutine(DoGetAccessToken());
        }

        public void IsLoggedIn(Action<bool> action)
        {
            string url = GetAuthRoot() + urlGetCurrentUser;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                CommonResponse<UserResponse> responseBody = JsonUtility.FromJson<CommonResponse<UserResponse>>(response);

                if(responseBody.data != null)
                {
                    SaveCurrentUser(responseBody.data);
                }

                action(responseBody.code == (long)MirrorResponseCode.Success);

            }));
        }
    }
}