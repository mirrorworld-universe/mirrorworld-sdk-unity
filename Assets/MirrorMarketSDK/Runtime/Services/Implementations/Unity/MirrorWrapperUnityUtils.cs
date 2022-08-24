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
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken());
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
                //LogFlow("Please set apiKey first.");
                CommonResponse<string> commonResponse = new CommonResponse<string>();
                commonResponse.Code = (long)MirrorResponseCode.LocalFailed;
                commonResponse.Error = "Please set apiKey first.";

                string resStr = JsonConvert.SerializeObject(commonResponse);
                callBack(resStr);
                yield break;
            }

            if (accessToken == "")
            {
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken());
                if (accessToken == "")
                {
                    //LogFlow("Get access token failed.");
                    CommonResponse<string> commonResponse = new CommonResponse<string>();
                    commonResponse.Code = (long)MirrorResponseCode.LocalFailed;
                    commonResponse.Error = "Get access token failed.";

                    string resStr = JsonConvert.SerializeObject(commonResponse);
                    callBack(resStr);
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
            Utils.SetAuthorizationHeader(request, accessToken);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(messageBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;
            //CommonResponse<string> responseBody = JsonConvert.DeserializeObject<CommonResponse<string>>(rawResponseBody);
            //UpdateRefreshToken(responseBody.Data.)
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
            UpdateRefreshToken(refreshToken);
        }

        private void SaveCurrentUser(UserResponse userResponse)
        {
            if(userResponse == null)
            {
                LogFlow("User Response object is null,logic error!");
                return;
            }
            tmpUser = userResponse;
        }

        private void UpdateRefreshToken(string newRefreshToken)
        {
            refreshToken = newRefreshToken;
            SaveStringToLocal(localKeyRefreshToken, refreshToken);
        }

        private string GetAPIRoot()
        {
            if(environment == Environment.Production)
            {
                return Constant.ApiRootProduction;
            }
            else if(environment == Environment.StagingMainnet)
            {
                return Constant.ApiRootStagingMainnet;
            }
            else if (environment == Environment.StagingDevnet)
            {
                return Constant.ApiRootStagingDevnet;
            }
            else
            {
                LogFlow("GetAPIRoot failed! env is:" + environment);
                return Constant.ApiRootStagingDevnet;
            }
        }

        private string GetAuthRoot()
        {
            if (environment == Environment.Production)
            {
                return Constant.UserRootProduction;
            }
            else if (environment == Environment.StagingMainnet)
            {
                return Constant.UserRootStagingMainnet;
            }
            else if (environment == Environment.StagingDevnet)
            {
                return Constant.UserRootStagingDevnet;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.UserRootStagingDevnet;
            }
        }
    }
}
#endif
