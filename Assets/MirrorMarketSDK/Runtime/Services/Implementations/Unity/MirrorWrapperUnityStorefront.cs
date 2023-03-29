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
        private readonly string urlGetCollectionFilterInfo = "filter_info";
        private readonly string urlGetNFTInfo = "nft/";
        private readonly string urlGetCollectionInfo = "collections";
        private readonly string urlGetNFTEvents = "events";
        private readonly string urlSearchNFT = "marketplace/nft/search";
        private readonly string urlRecommendSearchNFT = "recommend";
        private readonly string urlGetNFTs = "nfts";
        private readonly string urlGetNFTRealPrice = "marketplace/nft/real_price";

        public void GetCollectionFilterInfo(Dictionary<string, string> requestParams, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.MetadataCollection) + urlGetCollectionFilterInfo;

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, callBack));
        }

        public void GetNFTInfo(string mintAddress, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.MetadataNFT) + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, callBack));
        }

        public void GetNFTInfoOnEVM(string contract, string token_id, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.MetadataNFT) + contract + "/" + token_id;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, callBack));
        }

        public void GetCollectionInfo(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Metadata, urlGetCollectionInfo);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetCollectionsSummary(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.MetadataCollection, "summary");

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetNFTEvents(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.MetadataNFT, urlGetNFTEvents);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void SearchNFTs(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.MetadataNFT,"search");

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void RecommendSearchNFT(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.MetadataNFTSearch, urlRecommendSearchNFT);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callback));
        }

        public void GetNFTsByUnabridgedParams(string rawRequestBody, Action<string> callback)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.Metadata, urlGetNFTs);

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
