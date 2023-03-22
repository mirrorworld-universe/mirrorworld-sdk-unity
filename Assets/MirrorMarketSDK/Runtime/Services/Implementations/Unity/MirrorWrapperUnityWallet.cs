﻿
using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Interfaces;
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
                LogFlow("GetWalletTokens result:" + JsonUtility.ToJson(response));

                action(response);
            }));
        }

        public void GetWalletTokensByWallet(string walletAddress, Action<string> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + urlGetWalletTokens + "/" + walletAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
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
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + "wallet/" + walletAddress + "/transactions";

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
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

        public void TransferSPLToken(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, urlTransferTokenToAnotherAddress);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callBack));
        }
    }
}