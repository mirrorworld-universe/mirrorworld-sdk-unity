﻿#if !DEBUG_ANDROID && (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using System.Collections;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using Newtonsoft.Json;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IWalletService
    {
        private readonly string urlGetWalletTokens = "wallet/tokens";

        private readonly string urlGetWalletTransaction = "wallet/transactions";

        private readonly string urlGetWalletTransactionBySignature = "wallet/transactions";

        private readonly string urlTransferSolToAnotherAddress = "wallet/transfer-sol";

        private readonly string urlTransferTokenToAnotherAddress = "wallet/transfer-token";

        public void GetWalletTokens(Action<CommonResponse<WalletTokenResponse>> action)
        {
            string url = GetAPIRoot() + urlGetWalletTokens;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                CommonResponse<WalletTokenResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<WalletTokenResponse>>(response);

                action(responseBody);
            }));
        }

        public void GetWalletTransactions(decimal number,string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
        {
            string url = GetAPIRoot() + urlGetWalletTransaction + "?limit=" + number + "&next_before=" + nextBefore;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                CommonResponse<TransferTokenResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferTokenResponse>>(response);

                action(responseBody);
            }));
        }

        public void GetWalletTransactionsBySignatrue(string signature,Action<CommonResponse<TransferTokenResponse>> action)
        {
            string url = GetAPIRoot() + urlGetWalletTransactionBySignature + "/" + signature;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) =>
            {
                LogFlow("response:"+ response);
                CommonResponse<TransferTokenResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferTokenResponse>>(response);

                action(responseBody);
            }));
        }

        public void TransferSol(ulong amout, string publicKey,string confirmation, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            string url = GetAPIRoot() + urlTransferSolToAnotherAddress;

            TransferSolRequest requestBody = new TransferSolRequest();

            requestBody.Amount = amout;

            requestBody.ToPublicKey = publicKey;

            requestBody.Confirmation = confirmation;

            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferSol result :" + response);

                CommonResponse<TransferSolResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferSolResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void TransferSPLToken(ulong amout, string publicKey, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            string url = GetAPIRoot() + urlTransferTokenToAnotherAddress;

            TransferTokenRequest requestBody = new TransferTokenRequest();

            requestBody.Amount = amout;

            requestBody.ToPublicKey = publicKey;

            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<TransferTokenResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferTokenResponse>>(response);

                callBack(responseBody);

            }));
        }
    }
}
#endif