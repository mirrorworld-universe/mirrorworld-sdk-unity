
using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlGetWalletTokens = "tokens";

        private readonly string urlGetWalletTransaction = "transactions";

        private readonly string urlGetWalletTransactionBySignature = "transactions";

        private readonly string urlTransferSolToAnotherAddress = "transfer-sol";

        private readonly string urlTransferTokenToAnotherAddress = "transfer-token";

        public void GetWalletTokens(Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + urlGetWalletTokens;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("GetWalletTokens result:" + response);

                action(response);
            }));
        }

        public void GetWalletTokensByWallet(string walletAddress, Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + "tokens/" + walletAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                LogFlow("GetWalletTokens result:" + response);

                action(response);
            }));
        }

        public void GetWalletTokensByWalletOnEVM(string walletAddress, Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + "tokens" + walletAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                LogFlow("GetWalletTokens result:" + JsonUtility.ToJson(response));

                action(response);
            }));
        }

        public void GetWalletTransactions(Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, urlGetWalletTransaction);

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                action(response);
            }));
        }

        public void GetWalletTransactionsByWallet(string walletAddress, Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + walletAddress + "/transactions";

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                LogFlow("GetWalletTransactionsByWallet response:"+response);
                action(response);
            }));
        }

        public void GetWalletTransactionsBySignatrue(string signature,Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + urlGetWalletTransactionBySignature + "/" + signature;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("response:"+ response);

                action(response);
            }));
        }

        public void TransferSol(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, urlTransferSolToAnotherAddress);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferSol result :" + response);

                callBack(response);

            }));
        }

        public void TransferOnEVM(string url, string rawRequestBody, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferSol result :" + response);
                CommonResponse<TransferSolResponse> responseBody = JsonUtility.FromJson<CommonResponse<TransferSolResponse>>(response);
                callBack(responseBody);
            }));
        }

        public void TransferSPLToken(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, urlTransferTokenToAnotherAddress);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callBack));
        }

        public void StartPost(string wholeUrl, string rawRequestBody, Action<string> callBack)
        {
            monoBehaviour.StartCoroutine(CheckAndPost(wholeUrl, rawRequestBody, callBack));
        }

        public void StartPostWithTimoutConfig(string wholeUrl, string rawRequestBody,int timeOut,string timeOutMessage, Action<string> callBack)
        {
            monoBehaviour.StartCoroutine(CheckAndPostWithTimeoutConfig(wholeUrl, rawRequestBody,timeOut,timeOutMessage, callBack));
        }
    }
}