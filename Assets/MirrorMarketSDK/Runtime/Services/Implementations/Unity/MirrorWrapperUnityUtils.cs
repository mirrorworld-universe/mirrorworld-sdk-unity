using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string localKeyRefreshToken = "local_key_refresh_token";

        public IEnumerator CheckAndPost(string url, string messageBody, Action<string> callBack)
        {
            LogFlow("Try to post url:" + url);
            if(apiKey == "")
            {
                LogFlow("Please set apiKey first.");

                yield break;
            }

            if (accessToken == "")
            {
                LogFlow("No access token,try to get one...");
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken((isSuccess)=> {
                    if (isSuccess)
                    {
                        monoBehaviour.StartCoroutine(Post(url, messageBody, callBack));
                    }
                    else
                    {
                        LogFlow("No access token, please login first.");
                        return;
                    }
                }));
            }
            else
            {
                yield return Post(url, messageBody, callBack);
            }
        }

        public IEnumerator CheckAndGet(string url, Dictionary<string, string> requestParams, Action<string> callBack)
        {
            LogFlow("Try to get url:" + url);
            if (apiKey == "")
            {
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
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken((isSuccess)=> {
                    if (isSuccess)
                    {
                        monoBehaviour.StartCoroutine(Get(url, requestParams, callBack));
                    }
                    else
                    {
                        LogFlow("CheckAndGet: Get access token failed.");
                        CommonResponse<string> commonResponse = new CommonResponse<string>();
                        commonResponse.code = (long)MirrorResponseCode.LocalFailed;
                        commonResponse.error = "Please login first.";

                        string resStr = JsonUtility.ToJson(commonResponse);
                        callBack(resStr);
                    }
                }));
            }
            else
            {
                yield return Get(url, requestParams, callBack);
            }
        }

        private IEnumerator Post(string url, string messageBody, Action<string> callBack)
        {
            messageBody = RemoveNull(messageBody);

            UnityWebRequest request = new UnityWebRequest(url, "POST");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);
            MirrorUtils.SetXAuthToken(request,authToken);

            //LogFlow("apiKey:" + apiKey);
            //LogFlow("accessToken:" + accessToken);
            //LogFlow("authToken:" + authToken);

            if (messageBody != null && messageBody != "")
            {
                LogFlow("Post:"+ messageBody);
                byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(messageBody);
                request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            }
            
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            LogFlow("post response raw:"+ rawResponseBody);

            request.Dispose();

            callBack(rawResponseBody);
        }

        private IEnumerator Get(string url, Dictionary<string,string> requestParams, Action<string> callBack)
        {
            if(requestParams != null && requestParams.Count != 0)
            {
                string paramsString = "";
                for (int i = 0; i < requestParams.Count; i++)
                {
                    var item = requestParams.ElementAt(i);
                    if (i != 0)
                    {
                        paramsString += "&";
                    }
                    paramsString += item.Key + "=" + item.Value;
                }
                url = url + "?" + paramsString;
            }
            LogFlow("Get url:" + url);

            UnityWebRequest request = new UnityWebRequest(url, "GET");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);
            MirrorUtils.SetXAuthToken(request, authToken);

            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            string rawResponseBody = request.downloadHandler.text;

            LogFlow("get response raw:" + rawResponseBody);
            request.Dispose();

            callBack(rawResponseBody);
        }

        private string RemoveNull(string requestDataStr)
        {
            if(requestDataStr == null || requestDataStr == "")
            {
                return requestDataStr;
            }

            LogFlow("Handle data string:"+requestDataStr);
            string result = JsonAttrRemover.RemoveEmptyAttr(requestDataStr);
            LogFlow("Handle data string result:" + result);
            return result;
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
        public void SaveKeyParams(string accessToken,string refreshToken,UserResponse userResponse)
        {
            this.accessToken = accessToken;

            UpdateRefreshToken(refreshToken);

            SaveCurrentUser(userResponse);
        }
        public void SaveKeyParams(string accessToken, string refreshToken)
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

        public void ClearUnitySDKCache()
        {
            refreshToken = "";
            accessToken = "";
            SaveStringToLocal(localKeyRefreshToken, refreshToken);
        }

        private string GetAPIRoot()
        {
            if(environment == MirrorEnv.Mainnet)
            {
                return Constant.ApiRootProduction;
            }
            else if(environment == MirrorEnv.Devnet)
            {
                return Constant.ApiRootProductionDev;
            }
            else
            {
                LogFlow("GetAPIRoot failed! env is:" + environment);
                return Constant.ApiRootStagingDevnet;
            }
        }

        private string GetEntranceRoot()
        {
            if (environment == MirrorEnv.Mainnet)
            {
                return Constant.AuthRootProduction;
            }
            else if (environment == MirrorEnv.Devnet)
            {
                return Constant.AuthRootProductionDev;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.UserRootStagingDevnet;
            }
        }

        public string GetMarketUrl(string marketRoot)
        {
            string url = marketRoot + "?auth=" + accessToken;

            return url;
        }

        public string GetWalletUrl()
        {
            if (accessToken == null || accessToken == "")
            {
                return GetEntranceRoot();
            }
            else
            {
                return GetEntranceRoot() + "jwt?key=" + accessToken;
            }
        }

        private string GetMarketRoot()
        {
            if (environment == MirrorEnv.Mainnet)
            {
                return Constant.MarketRootProduction;
            }
            else if (environment == MirrorEnv.Devnet)
            {
                return Constant.MarketRootProductionDev;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.MarketRootStagingDevnet;
            }
        }

        private string GetAuthRoot()
        {
            if (environment == MirrorEnv.Mainnet)
            {
                return Constant.UserRootProduction;
            }
            else if (environment == MirrorEnv.Devnet)
            {
                return Constant.UserRootProduction;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.UserRootStagingDevnet;
            }
        }

        private string GetDebugLoginPageRoot()
        {
            if (environment == MirrorEnv.Mainnet)
            {
                return Constant.urlDebugLoginUrlPreProductionMain;
            }
            else if (environment == MirrorEnv.Devnet)
            {
                return Constant.urlDebugLoginUrlPreProductionDev;
            }
            else
            {
                LogFlow("GetAuthRoot failed! env is:" + environment);
                return Constant.urlDebugLoginUrlPreProductionDev;
            }
        }
    }
}
