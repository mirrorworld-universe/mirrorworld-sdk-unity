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

        public IEnumerator CheckAndPostWithTimeoutConfig(string url, string messageBody,int timeOut,string timeOutMessage, Action<string> callBack)
        {
            LogFlow("Try to post url:" + url);
            if (apiKey == "")
            {
                LogFlow("Please set apiKey first.");

                yield break;
            }

            if (accessToken == "")
            {
                LogFlow("No access token,try to get one...");
                yield return monoBehaviour.StartCoroutine(DoGetAccessToken((isSuccess) => {
                    if (isSuccess)
                    {
                        monoBehaviour.StartCoroutine(PostWithTotalParameters(url, messageBody,timeOut,timeOutMessage, callBack));
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
            yield return PostWithTotalParameters(url, messageBody,-1,"Request error",callBack);
        }

        private IEnumerator PostWithTotalParameters(string url, string messageBody,int outTime,string timeOutMessage, Action<string> callBack)
        {
            LogUtils.LogFlow("Post url:"+url);
            messageBody = RemoveNull(messageBody);

            UnityWebRequest request = new UnityWebRequest(url, "POST");

            MirrorUtils.SetContentTypeHeader(request);
            MirrorUtils.SetAcceptHeader(request);
            MirrorUtils.SetApiKeyHeader(request, apiKey);
            MirrorUtils.SetAuthorizationHeader(request, accessToken);
            MirrorUtils.SetXAuthToken(request,authToken);

            LogFlow("apiKey:" + apiKey);
            //LogFlow("accessToken:" + accessToken);
            //LogFlow("authToken:" + authToken);

            if (messageBody != null && messageBody != "")
            {
                LogFlow("Post:"+ messageBody);
                byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(messageBody);
                request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            }
            
            request.downloadHandler = new DownloadHandlerBuffer();
            if(outTime != 0 && outTime != -1) request.timeout = outTime;

            yield return request.SendWebRequest();

            //if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            //{
            //    LogUtils.LogWarn(request.error);

            //    CommonResponse<string> commonResponse = new CommonResponse<string>();
            //    commonResponse.code = (long)MirrorResponseCode.LocalFailed;
            //    commonResponse.data = null;
            //    commonResponse.error = timeOutMessage;
            //    commonResponse.http_status_code = (long)MirrorResponseCode.LocalFailed;
            //    commonResponse.status = "Time out";
            //    commonResponse.error = timeOutMessage;

            //    string rawResponseBody = JsonUtility.ToJson(commonResponse);
            //    LogUtils.LogFlow("Time out in client, fake response is:"+ rawResponseBody);

            //    callBack(rawResponseBody);

            //    request.Dispose();
            //}
            //else
            {
                string rawResponseBody = request.downloadHandler.text;

                LogFlow("post response raw:" + rawResponseBody);

                request.Dispose();

                callBack(rawResponseBody);
            }
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

            if(!string.IsNullOrEmpty(refreshToken)) UpdateRefreshToken(refreshToken);

            SaveCurrentUser(userResponse);
        }
        public void SaveKeyParams(string accessToken, string refreshToken)
        {
            this.accessToken = accessToken;

            if (!string.IsNullOrEmpty(refreshToken)) UpdateRefreshToken(refreshToken);
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

        public void UpdateRefreshToken(string newRefreshToken)
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

        public string GetMarketUrl(string marketRoot)
        {
            string url = marketRoot + "?auth=" + accessToken;

            return url;
        }

        private string GetAuthRoot()
        {
            string apiRoot = UrlUtils.GetAPIRoot();
            return apiRoot + "/" + MWConfig.serverAPIVersion + "/";
        }
    }
}
