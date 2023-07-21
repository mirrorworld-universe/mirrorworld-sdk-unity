using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MirrorworldSDK.Models;

using System.Runtime.InteropServices;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        /// <summary>
        /// Used for the final action of this approving
        /// </summary>
        public Action approveFinalAction;

        private readonly string urlActionRequest = "auth/actions/request";

        public void StartSecuirtyApprove<T>(string type, string message, T request, Action callback)
        {

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
            callback();
#else
            GetSecurityToken(type,"0",message,request,callback);
#endif
        }

        public void StartSecuirtyApprove<T>(string type, string value, string message, T request, Action callback)
        {

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
            callback();
#else
            GetSecurityToken(type,value,message,request,callback);
#endif
        }

        public void GetSecurityToken<T>(string type, string value, string message, T request, Action callback)
        {
            if(apiKey == "" || accessToken == "" || refreshToken == "")
            {
                LogFlow("You must login first!");
                return;
            }
            approveFinalAction = callback;
            RequestActionAuthorization<T>(type,value,message, request);
        }

        public string GetActionRoot()
        {
            //if (environment == MirrorEnv.Devnet)
            //{
            //    return "https://api.mirrorworld.fun/v1/";
            //}
            //else if (environment == MirrorEnv.Mainnet)
            //{
            //    return "https://api.mirrorworld.fun/v1/";
            //}
            //else
            //{
            //    LogFlow("Unknown env:" + environment);
            //    return "https://api.mirrorworld.fun/v1/";
            //}
            string apiRoot = UrlUtils.GetAPIRoot();
            return apiRoot + "/" + MWConfig.serverAPIVersion + "/";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>Only a few API need this value
        /// <param name="value"></param>
        /// <param name="message"></param>
        /// <param name="requestPrams"></param>
        public void RequestActionAuthorization<T>(string type, string value, string message, T requestPrams)
        {
            CommonApprove<T> jsonObject = new CommonApprove<T>();
            jsonObject.type = type;
            jsonObject.message = message;
            jsonObject.value = value;
            jsonObject.paramsPlaceHolder = requestPrams;
            //HandleValue<T>(jsonObject, requestPrams);

            string data = JsonUtility.ToJson(jsonObject);
            data = data.Replace("paramsPlaceHolder", "params");

            LogFlow("unity request auth data" + data);

            string url = GetActionRoot() + urlActionRequest;
            monoBehaviour.StartCoroutine(CheckAndPost(url, data, (result) => {
                LogFlow("requestActionAuthorization result:" + result);
                CommonResponse<ActionAuthResponse> response = JsonUtility.FromJson<CommonResponse<ActionAuthResponse>>(result);
                if (response.code == (long)MirrorResponseCode.Success)
                {
                    OpenApprovePage(response.data.uuid, jsonObject.value);
                }
                else
                {
                    LogFlow("requestActionAuthorization failed.");
                }
            }));
        }

        public void HandleValue<T>(CommonApprove<T> approveRequest, T apiParams)
        {
            bool haveAmountParam = false;
            double amountObj = 0;
            int decimalsObj = 9;

            if (apiParams.GetType() == typeof(ApproveListNFT))
            {
                ApproveListNFT approveListNFT = apiParams as ApproveListNFT;
                amountObj = PrecisionUtil.StrToFloat(approveListNFT.price);
                haveAmountParam = true;
                return;
            }
            else if (apiParams.GetType() == typeof(ApproveTransferSOL))
            {
                ApproveTransferSOL approveTransferSOL = apiParams as ApproveTransferSOL;
                amountObj = approveTransferSOL.amount;
                haveAmountParam = true;
            }
            else if (apiParams.GetType() == typeof(ApproveTransferSPLToken))
            {
                ApproveTransferSPLToken approveTransferSOL = apiParams as ApproveTransferSPLToken;
                amountObj = approveTransferSOL.amount;
                decimalsObj = approveTransferSOL.decimals;
                haveAmountParam = true;
            }

            double valueValue = 0;
            int valueCount = 0;
            if (haveAmountParam)
            {
                valueValue = amountObj;
                valueCount++;
            }
            LogFlow("HandleValue valueValue:" + valueValue);
            LogFlow("HandleValue decimalsObj:" + decimalsObj);

            if (valueCount == 0)
            {
                return;
            }

            if (valueCount > 1)
            {
                LogFlow("There is both exists amount and price!");
                return;
            }

            int digit = GetDigit(valueValue);
            int totalDigit = digit + decimalsObj;
            double dec = Math.Pow(10, decimalsObj);
            double v = valueValue / dec;
            string strNeed = string.Format("{0:F" + totalDigit + "}", v);

            approveRequest.value = strNeed;
        }

        public void OpenApprovePage(string actionUUID,string value)
        {
            if (actionUUID == "")
            {
                LogWarn("uuid from server is null!");

                return;
            }
            string url = UrlUtils.GetApproveUrl() + actionUUID + "?key=" + accessToken;

            LogFlow("Unity open approve url:"+url);

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

            //todo:need to add API to finish this flow.
            Application.OpenURL(url);

#elif (UNITY_ANDROID && !(UNITY_EDITOR))

            AndroidOpenUrl(url);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            IOSOpenUrl(url);

            IOSSecurityAuthCallback handler = new IOSSecurityAuthCallback(IOSGetSecurityAuthToken);
            IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
            MirrorWrapper.IOSOpenUrlSetCallBack(fp);

#endif
        }

        public string GetActionRootWithoutVersion()
        {
            return UrlUtils.GetAuthRoot();
        }

        private int GetDigit(double number)
        {
            if(haveSmallDigit(number))
            {
                return 0;
            }
            for(int i = 1; i < 10; i++)
            {
                if(haveSmallDigit(number * Math.Pow(10, i)))
                {
                    return i;
                }
            }
            return 10;
        }

        private bool haveSmallDigit(double number)
        {
            double pre = Math.Truncate(number);

            double after = number - pre;

            if(after == 0)
            {
                return true;
            }

            return false;
        }

    }
}
    
