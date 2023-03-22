using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlGetCollectionFilterInfo = "marketplace/collection/filter_info";
        private readonly string urlGetNFTInfo = "marketplace/nft/";
        private readonly string urlGetCollectionInfo = "marketplace/collections";
        private readonly string urlGetNFTEvents = "marketplace/nft/events";
        private readonly string urlSearchNFT = "marketplace/nft/search";
        private readonly string urlRecommendSearchNFT = "marketplace/nft/search/recommend";
        private readonly string urlGetNFTs = "marketplace/nfts";
        private readonly string urlGetNFTRealPrice = "marketplace/nft/real_price";

        public void GetCollectionFilterInfo(Dictionary<string, string> requestParams, Action<string> callBack)
        {
            string url = GetAuthRoot() + urlGetCollectionFilterInfo;

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, callBack));
        }

        public void GetNFTInfo(string mintAddress, Action<string> callBack)
        {
            string url = GetAuthRoot() + urlGetNFTInfo + "/" + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, callBack));
        }

        public void GetCollectionInfo(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlGetCollectionInfo;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetCollectionsSummary(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlGetCollectionInfo;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetNFTEvents(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlGetNFTEvents;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void SearchNFTs(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlSearchNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void RecommendSearchNFT(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlRecommendSearchNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetNFTsByUnabridgedParams(string rawRequestBody, Action<string> callback)
        {
            string url = GetAuthRoot() + urlGetNFTs;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetNFTRealPrice(string price, int fee, Action<CommonResponse<GetNFTRealPriceResponse>> callback)
        {
            GetNFTRealPriceRequest requestBody = new GetNFTRealPriceRequest();

            requestBody.price = price;

            requestBody.fee = fee;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlGetNFTRealPrice;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<GetNFTRealPriceResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetNFTRealPriceResponse>>(response);

                callback(responseBody);

            }));
        }
    }
}
