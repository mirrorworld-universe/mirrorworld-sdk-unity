using UnityEngine;
using System.Collections;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using System;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlCheckStatusOfTransactions = "solana/confirmation/transactions-status";

        private readonly string urlCheckStatusOfMinting = "solana/confirmation/mints-status";

        public void GetStatusOfTransactions(List<string> signatures, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            GetStatusOfTransactionsRequest requestBody = new GetStatusOfTransactionsRequest();

            requestBody.signatures = signatures;

            string rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlCheckStatusOfTransactions;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) =>
            {
                LogFlow("GetStatusOfTransactions result:" + response);

                CommonResponse<GetStatusOfTransactionsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetStatusOfTransactionsResponse>>(response);

                callBack(responseBody);
            }));
        }


        public void GetStatusOfMintings(List<string> mintAddresses, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            GetStatusOfMintingRequest requestBody = new GetStatusOfMintingRequest();

            requestBody.mint_addresses = mintAddresses;

            string rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlCheckStatusOfMinting;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) =>
            {
                LogFlow("GetStatusOfMintings result:" + response);

                CommonResponse<GetStatusOfTransactionsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetStatusOfTransactionsResponse>>(response);

                callBack(responseBody);
            }));
        }
    }
}