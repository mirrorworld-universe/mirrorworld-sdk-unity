using UnityEngine;
using System.Collections;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using System;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlSolAssetConfirmationTransactionStatus = "transactions-status";

        private readonly string urlCheckStatusOfMinting = "mints-status";

        public void GetStatusOfTransactions(List<string> signatures, Action<string> callBack)
        {
            GetStatusOfTransactionsRequest requestBody = new GetStatusOfTransactionsRequest();

            requestBody.signatures = signatures;

            string rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Confirmation, urlSolAssetConfirmationTransactionStatus);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) =>
            {
                LogFlow("GetStatusOfTransactions result:" + response);

                callBack(response);
            }));
        }


        public void GetStatusOfMintings(List<string> mintAddresses, Action<string> callBack)
        {
            GetStatusOfMintingRequest requestBody = new GetStatusOfMintingRequest();

            requestBody.mint_addresses = mintAddresses;

            string rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Confirmation, urlCheckStatusOfMinting);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) =>
            {
                LogFlow("GetStatusOfMintings result:" + response);

                callBack(response);
            }));
        }
    }
}