
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
        private readonly string urlGetWalletTokens = "wallet/tokens";

        private readonly string urlGetWalletTransaction = "wallet/transactions";

        private readonly string urlGetWalletTransactionBySignature = "wallet/transactions";

        private readonly string urlTransferSolToAnotherAddress = "wallet/transfer-sol";

        private readonly string urlTransferTokenToAnotherAddress = "wallet/transfer-token";

        public void GetWalletTokens(Action<string> action)
        {
            string url = GetAPIRoot() + urlGetWalletTokens;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("GetWalletTokens result:" + JsonUtility.ToJson(response));

                action(response);
            }));
        }

        public void GetWalletTokensByWallet(string walletAddress, Action<string> action)
        {
            string url = GetAPIRoot() + urlGetWalletTokens + walletAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("GetWalletTokens result:" + JsonUtility.ToJson(response));

                action(response);
            }));
        }

        public void GetWalletTransactions(Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = GetAPIRoot() + urlGetWalletTransaction;

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                action(response);
            }));
        }

        public void GetWalletTransactionsByWallet(string walletAddress, Dictionary<string, string> requestParams, Action<string> action)
        {
            string url = GetAPIRoot() + "wallet/" + walletAddress + "/transactions";

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) =>
            {
                action(response);
            }));
        }

        public void GetWalletTransactionsBySignatrue(string signature,Action<string> action)
        {
            string url = GetAPIRoot() + urlGetWalletTransactionBySignature + "/" + signature;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("response:"+ response);

                action(response);
            }));
        }

        public void TransferSol(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlTransferSolToAnotherAddress;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferSol result :" + response);

                callBack(response);

            }));
        }

        public void TransferSPLToken(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlTransferTokenToAnotherAddress;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callBack));
        }
    }
}