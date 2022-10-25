using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;
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
                LogFlow("No access token,try to get one...");
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
                commonResponse.code = (long)MirrorResponseCode.LocalFailed;
                commonResponse.error = "Please set apiKey first.";

                string resStr = JsonUtility.ToJson(commonResponse);
                callBack(resStr);
                yield break;
            }

            if (accessToken == "")
            {
                LogFlow("No access token,try to get one...");
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken());
                if (accessToken == "")
                {
                    LogFlow("Get access token failed.");
                    CommonResponse<string> commonResponse = new CommonResponse<string>();
                    commonResponse.code = (long)MirrorResponseCode.LocalFailed;
                    commonResponse.error = "Get access token failed.";

                    string resStr = JsonUtility.ToJson(commonResponse);
                    callBack(resStr);
                    yield break;
                }
            }

            yield return Get(url, requestParams, callBack);
        }

        private IEnumerator Post(string url, string messageBody, Action<string> callBack)
        {
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(messageBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            request.Dispose();

            callBack(rawResponseBody);
        }

        private IEnumerator Get(string url, Dictionary<string,string> requestParams, Action<string> callBack)
        {
            UnityWebRequest request = new UnityWebRequest(url, "GET");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            request.Dispose();

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

        private void ClearUnitySDKCache()
        {
            accessToken = "";
            SaveStringToLocal(localKeyRefreshToken,"");
        }

        private string GetAPIRoot()
        {
            if(environment == MirrorEnv.ProductionMainnet)
            {
                return Constant.ApiRootProduction;
            }
            else if(environment == MirrorEnv.ProductionDevnet)
            {
                return Constant.ApiRootProductionDev;
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return Constant.ApiRootStagingDevnet;
            }
            else if (environment == MirrorEnv.StagingMainNet)
            {
                return Constant.ApiRootStagingMainnet;
            }
            else
            {
                LogFlow("GetAPIRoot failed! env is:" + environment);
                return Constant.ApiRootStagingDevnet;
            }
        }

        private string GetEntranceRoot()
        {
            if (environment == MirrorEnv.ProductionMainnet)
            {
                return Constant.AuthRootProduction;
            }
            else if (environment == MirrorEnv.ProductionDevnet)
            {
                return Constant.AuthRootProductionDev;
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return Constant.AuthRootStagingDevnet;
            }
            else if (environment == MirrorEnv.StagingMainNet)
            {
                return Constant.AuthRootStagingMainnet;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.UserRootStagingDevnet;
            }
        }

        private string GetMarketRoot()
        {
            if (environment == MirrorEnv.ProductionMainnet)
            {
                return Constant.MarketRootProduction;
            }
            else if (environment == MirrorEnv.ProductionDevnet)
            {
                return Constant.MarketRootProductionDev;
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return Constant.MarketRootStagingDevnet;
            }
            else if (environment == MirrorEnv.StagingMainNet)
            {
                return Constant.MarketRootStagingMainnet;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.MarketRootStagingDevnet;
            }
        }

        private string GetAuthRoot()
        {
            if (environment == MirrorEnv.ProductionMainnet)
            {
                return Constant.UserRootProduction;
            }
            else if (environment == MirrorEnv.ProductionDevnet)
            {
                return Constant.UserRootStagingDevnet;
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return Constant.UserRootStagingDevnet;
            }
            else if (environment == MirrorEnv.StagingMainNet)
            {
                return Constant.UserRootStagingMainnet;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.UserRootStagingDevnet;
            }
        }
    }
}
