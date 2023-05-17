using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorWorldResponses;

namespace MirrorworldSDK
{
    public class MWSUIWrapper
    {
        //Wallet
        public static void GetTransactionsByDigest(string digest, Action<CommonResponse<SUIResGetTransactionByDigest>> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + "transactions/" + digest;
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndGet(url, null, (response) =>
            {
                CommonResponse<SUIResGetTransactionByDigest> responseBody = JsonUtility.FromJson<CommonResponse<SUIResGetTransactionByDigest>>(response);
                action(responseBody);
            }));
        }

        public static void GetTokens(Action<CommonResponse<SUIResGetTokens>> action)
        {
            MirrorWrapper.Instance.GetWalletTokens((response)=> {

                CommonResponse<SUIResGetTokens> responseBody = JsonUtility.FromJson<CommonResponse<SUIResGetTokens>>(response);

                action(responseBody);
            });
        }

        public static void TransferSUI(string to_publickey, int amount, Action approveFinished, Action<CommonResponse<SUIResTransferSUI>> callBack)
        {
            SUIReqTransferSUI requestParams = new SUIReqTransferSUI();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;

            if (MWConfig.isSUIBeta)
            {
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-sui");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferSUI> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferSUI>>(response);
                    callBack(responseBody);
                }));
                return;
            }

            string approveValue = PrecisionUtil.GetApproveValue(amount);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSUI, approveValue, "transfer sui", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-sui");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferSUI> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferSUI>>(response);
                    callBack(responseBody);
                }));
            });
        }

        public static void TransferToken(string to_publickey, int amount, string token,Action approveFinished, Action<CommonResponse<SUIResTransferToken>> callBack)
        {
            if (token == null || !token.Contains("::"))
            {
                CommonResponse<SUIResTransferToken> commonResponse = new CommonResponse<SUIResTransferToken>();
                commonResponse.code = (long)MirrorResponseCode.LocalFailed;
                commonResponse.error = "Please check your token address,normally it contains ::.";
                commonResponse.data = new SUIResTransferToken();
                commonResponse.data.tx_signature = "";

                callBack(commonResponse);
                return;
            }
            SUIReqTransferToken requestParams = new SUIReqTransferToken();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;
            requestParams.token = token;

            if (MWConfig.isSUIBeta)
            {
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-token");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) =>
                {
                    CommonResponse<SUIResTransferToken> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferToken>>(response);
                    callBack(responseBody);
                }));
                return;
            }

            string approveValue = PrecisionUtil.GetApproveValue(amount);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferToken, approveValue, "transfer token", requestParams, () => {

                if (approveFinished != null)
                {
                    approveFinished();
                }

                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-token");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferToken> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferToken>>(response);
                    callBack(responseBody);
                }));
            });
        }
    }
}
