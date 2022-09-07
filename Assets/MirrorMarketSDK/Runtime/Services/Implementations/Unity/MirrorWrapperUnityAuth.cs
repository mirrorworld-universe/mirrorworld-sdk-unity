
using System;
using System.Collections;
using MirrorworldSDK;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
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
                CommonResponse<UserResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(response);
                SaveCurrentUser(responseBody.Data);
                callBack(responseBody);
            }));
        }

        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            BasicEmailLoginRequest requestBody = new BasicEmailLoginRequest();
            requestBody.Email = emailAddress;
            requestBody.Password = password;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string url = GetAuthRoot() + urlLoginWithEmail;

            monoBehaviour.StartCoroutine(Post(url, rawRequestBody, (rawResponseBody) =>
            {
                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);

                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                callBack(responseBody);
            }));
        }

        public void FetchUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            string url = GetAuthRoot() + urlQueryUser + "?email=" + email;
            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {
                CommonResponse<UserResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(response);
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

            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);
            Utils.SetRefreshToken(request, refreshToken);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);

            if (responseBody.Code.Equals((int)MirrorResponseCode.Success))
            {
                LogFlow("GetAccessToken success");

                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);
            }
            else
            {
                LogFlow("GetAccessToken failed: code:" + responseBody.Code + " reason:" + responseBody.Error);
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

                CommonResponse<UserResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(response);

                if(responseBody.Data != null)
                {
                    SaveCurrentUser(responseBody.Data);
                }

                action(responseBody.Data != null);

            }));
        }
    }
}