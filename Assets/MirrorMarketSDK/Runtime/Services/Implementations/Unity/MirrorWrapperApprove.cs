using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        /// <summary>
        /// Used for the final action of this approving
        /// </summary>
        private Action approveFinalAction;

        private readonly string urlActionRequest = "auth/actions/request";
        private readonly string urlActionAPPROVE = "approve/";

        public void GetSecurityToken(string type, string message, Dictionary<string, string> request, Action callback)
        {
            if(apiKey == "" || accessToken == "" || refreshToken == "")
            {
                LogFlow("You must login first!");
                return;
            }
            approveFinalAction = callback;
            RequestActionAuthorization(type,message, request);
        }

        public string GetActionRoot()
        {
            if (environment == MirrorEnv.StagingMainNet)
            {
                return "https://api-staging.mirrorworld.fun/v1/";
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return "https://api-staging.mirrorworld.fun/v1/";
            }
            else if (environment == MirrorEnv.ProductionDevnet)
            {
                return "https://api.mirrorworld.fun/v1/";
            }
            else if (environment == MirrorEnv.ProductionMainnet)
            {
                return "https://api.mirrorworld.fun/v1/";
            }
            else
            {
                LogFlow("Unknown env:" + environment);
                return "https://api.mirrorworld.fun/v1/";
            }
        }

        public void RequestActionAuthorization(string type, string message, Dictionary<string, string> requestPrams)
        {
            Dictionary<string, string> jsonObject = new Dictionary<string, string>();
            jsonObject.Add("type", type);
            jsonObject.Add("message", message);
            jsonObject.Add("value", "0");
            jsonObject.Add("params", JsonUtility.ToJson(requestPrams));
            HandleValue(jsonObject, requestPrams);

            string data = JsonUtility.ToJson(jsonObject);
            LogFlow("unity request auth data" + data);

            string url = GetActionRoot() + urlActionRequest;
            monoBehaviour.StartCoroutine(CheckAndPost(url, null, (result) => {
                LogFlow("requestActionAuthorization result:" + result);
                CommonResponse<ActionAuthResponse> response = JsonUtility.FromJson<CommonResponse<ActionAuthResponse>>(result);
                if (response.code == (long)MirrorResponseCode.Success)
                {
                    OpenApprovePage(response.data.uuid);
                }
                else
                {
                    //                    if(callback != null) callback("");
                }
            }));
        }

        public void HandleValue(Dictionary<string,string> approveRequest, Dictionary<string, string> apiParams)
        {
            string amountObj = null;
            if(apiParams.ContainsKey("amount")){
                amountObj = apiParams["amount"];
            }

            string priceObj = null;
            if(apiParams.ContainsKey("price")) {
                priceObj = apiParams["price"];
            }

            int decimals = -100;
            if (apiParams.ContainsKey("decimals"))
            {
                string decimalsObj = apiParams["decimals"];
                decimals = int.Parse(decimalsObj);
            }

            float valueValue = 0;
            int valueCount = 0;
            if (amountObj != null)
            {
                valueValue = float.Parse(amountObj);
                valueCount++;
            }
            if (priceObj != null)
            {
                valueValue = float.Parse(priceObj);
                valueCount++;
            }

            if (valueCount == 0)
            {
                return;
            }

            if (valueCount > 1)
            {
                LogFlow("There is both exists amount and price!");
                return;
            }

            if (decimals == -100)
            {
                decimals = 9;
            }

            string valueKey = "value";
            if (approveRequest.ContainsKey(valueKey))
            {
                approveRequest.Remove(valueKey);
                double dec = Math.Pow(10, decimals);
                double v = valueValue / dec;
                string strNeed = string.Format("{0:F"+ decimals + "}", v);

                approveRequest.Add(valueKey, strNeed);
            }
            else
            {
                LogFlow("No value param when approving!");
            }
        }

        public void OpenApprovePage(string actionUUID)
        {
            if (actionUUID == "")
            {
                LogWarn("uuid from server is null!");
                return;
            }
            string url = GetActionRootWithoutVersion() + urlActionAPPROVE + actionUUID + "?useSchemeRedirect=true";

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            AndroidOpenUrl(url);

#elif (UNITY_IOS && !(UNITY_EDITOR))

#endif
        }

        public string GetActionRootWithoutVersion()
        {
            if (environment == MirrorEnv.StagingMainNet)
            {
                return "https://auth-staging.mirrorworld.fun/";
            }
            else if (environment == MirrorEnv.StagingDevNet)
            {
                return "https://auth-staging.mirrorworld.fun/";
            }
            else if (environment == MirrorEnv.ProductionDevnet)
            {
                return "https://auth.mirrorworld.fun/";
            }
            else if (environment == MirrorEnv.ProductionMainnet)
            {
                return "https://auth.mirrorworld.fun/";
            }
            else
            {
                LogFlow("Unknown env:" + environment);
                return "https://auth.mirrorworld.fun/";
            }
        }
    }
}
    
