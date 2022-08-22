#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string localKeyRefreshToken = "local_key_refresh_token";

        private IEnumerator CheckAndPost(string url, string messageBody, Action<string> callBack)
        {
            if(apiKey == "")
            {
                LogFlow("Please set apiKey first.");
                yield break;
            }

            if (accessToken == "")
            {
                yield return monoBehaviour.StartCoroutine(GetAccessToken());
                if (accessToken == "")
                {
                    LogFlow("Get access token failed.");
                    yield break;
                }
            }

            yield return Post(url,messageBody,callBack);
        }

        private IEnumerator CheckAndGet(string url, Dictionary<string, string> requestParams, Action<string> callBack)
        {
            if (apiKey == "")
            {
                LogFlow("Please set apiKey first.");
                yield break;
            }

            if (accessToken == "")
            {
                yield return monoBehaviour.StartCoroutine(GetAccessToken());
                if (accessToken == "")
                {
                    LogFlow("Get access token failed.");
                    yield break;
                }
            }

            yield return Get(url, requestParams, callBack);
        }

        private IEnumerator Post(string url, string messageBody, Action<string> callBack)
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(messageBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            callBack(rawResponseBody);
        }

        private IEnumerator Get(string url, Dictionary<string,string> requestParams, Action<string> callBack)
        {
            UnityWebRequest request = new UnityWebRequest(url, "GET");

            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            callBack(rawResponseBody);
        }

        public IEnumerator GetAccessToken()
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

            if(apiKey == "")
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

        private void SaveStringToLocal(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        private string GetStringFromLocal(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        //Nessesary params
        private void saveKeyParams(string accessToken,string refreshToken,UserResponse userResponse)
        {
            this.accessToken = accessToken;
            this.refreshToken = refreshToken;
            tmpUser = userResponse;
            SaveStringToLocal(localKeyRefreshToken, refreshToken);
        }
    }
}
#endif
