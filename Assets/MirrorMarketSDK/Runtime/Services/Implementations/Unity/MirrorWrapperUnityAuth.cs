#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
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
        private readonly string urlLoginWithEmail = Constant.ApiRoot + "auth/login";
        private readonly string urlRefreshToken = Constant.UserRoot + "auth/refresh-token";
        private readonly string urlGetCurrentUser = Constant.ApiRoot + "auth/me";
        private readonly string urlQueryUser = Constant.ApiRoot + "auth/user";

        public void GetCurrentUserInfo(Action<CommonResponse<UserResponse>> callBack)
        {
            monoBehaviour.StartCoroutine(CheckAndGet(urlGetCurrentUser, null, (response) => {
                CommonResponse<UserResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(response);
                SaveCurrentUser(responseBody.Data);
                callBack(responseBody);
            }));
        }

        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            //todo check email format

            BasicEmailLoginRequest requestBody = new BasicEmailLoginRequest();
            requestBody.Email = emailAddress;
            requestBody.Password = password;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            monoBehaviour.StartCoroutine(Post(urlLoginWithEmail, rawRequestBody, (rawResponseBody) =>
            {
                LogFlow("rawResponseBody:" + rawResponseBody);

                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                callBack(responseBody);
            }));
        }

        public void QueryUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            string url = urlQueryUser + "?email=" + email;
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


            UnityWebRequest request = new UnityWebRequest(urlRefreshToken, "GET");

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
                LogFlow("GetAccessToken failed:" + rawResponseBody);
            }

        }

        public void GetAccessToken()
        {
            monoBehaviour.StartCoroutine(DoGetAccessToken());
        }
    }
}
#endif