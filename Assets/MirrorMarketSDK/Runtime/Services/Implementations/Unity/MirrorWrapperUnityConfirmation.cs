using UnityEngine;
using System.Collections;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using System;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlCheckStatusOfTransactions = "https://api.mirrorworld.fun/v1/devnet/solana/confirmation/transactions-status";
        private readonly string urlCheckStatusOfMinting = "https://api.mirrorworld.fun/v1/devnet/solana/confirmation/mints-status";

        //public void GetStatusOfTransactions(List<string> signatures, Action<GetStatusOfTransactionsResponse> callBack)
        //{
        //    GetStatusOfTransactionsRequest requestBody = new GetStatusOfTransactionsRequest();

        //    requestBody.signatures = signatures;

        //    string rawRequestBody = JsonUtility.ToJson(requestBody);

        //    string url = GetAPIRoot() + urlCheckStatusOfTransactions;

        //    monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

        //        LogFlow("GetStatusOfTransactions result:" + response);

        //        CommonResponse<MintResponse> responseBody = JsonUtility.FromJson<CommonResponse<MintResponse>>(response);

        //        callBack(responseBody);
        //    }));
        //}

    }
}