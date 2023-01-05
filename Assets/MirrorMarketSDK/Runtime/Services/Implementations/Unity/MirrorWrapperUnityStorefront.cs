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

        public void GetCollectionFilterInfo(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            string url = GetAuthRoot() + urlGetCollectionFilterInfo;

            Dictionary<string, string> requestParams = new Dictionary<string, string>();

            requestParams.Add("collection", collection);

            monoBehaviour.StartCoroutine(CheckAndGet(url, requestParams, (response) => {

                CommonResponse<GetCollectionFilterInfoResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetCollectionFilterInfoResponse>>(response);

                callBack(responseBody);
            }));
        }

        public void GetNFTInfo(string mintAddress, Action<string> callBack)
        {
            string url = GetAuthRoot() + urlGetNFTInfo + "/" + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                callBack(response);
            }));
        }

        public void GetCollectionInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

            requestBody.collections = collections;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlGetCollectionInfo;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<List<GetCollectionInfoResponse>> responseBody = JsonUtility.FromJson<CommonResponse<List<GetCollectionInfoResponse>>>(response);

                callback(responseBody);

            }));
        }

        public void GetNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
        {
            GetNFTEventsRequest requestBody = new GetNFTEventsRequest();

            requestBody.mint_address = mintAddress;

            requestBody.page = page;

            requestBody.page_size = pageSize;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlGetNFTEvents;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<GetNFTEventsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetNFTEventsResponse>>(response);

                callback(responseBody);

            }));
        }

        public void SearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            SearchNFTsRequest requestBody = new SearchNFTsRequest();

            requestBody.collections = collections;

            requestBody.search = searchString;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlSearchNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                Debug.Log("SearchNFTs result:"+response);

                CommonResponse <List<MirrorMarketNFTObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<MirrorMarketNFTObj>>>(response);

                callback(responseBody);

            }));
        }

        public void RecommendSearchNFT(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            RecommendSearchNFTRequest requestBody = new RecommendSearchNFTRequest();

            requestBody.collections = collections;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlRecommendSearchNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<List<MirrorMarketNFTObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<MirrorMarketNFTObj>>>(response);

                callback(responseBody);

            }));
        }

        public void GetNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
        {
            GetNFTsRequest requestBody = new GetNFTsRequest();

            requestBody.collection = collection;

            requestBody.page = page;

            requestBody.page_size = pageSize;

            requestBody.order = new GetNFTsRequestOrder();

            requestBody.order.order_by = orderByString;

            requestBody.order.desc = desc;

            requestBody.filter = filters;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAuthRoot() + urlGetNFTs;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("GetNFTs response:" + response);

                CommonResponse <GetNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetNFTsResponse>>(response);

                callback(responseBody);

            }));
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
