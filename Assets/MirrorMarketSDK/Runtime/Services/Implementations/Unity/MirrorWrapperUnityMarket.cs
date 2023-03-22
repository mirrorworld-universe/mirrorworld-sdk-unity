using System;
using System.Collections.Generic;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        //fetch
        private readonly string urlFetchSingleNFT = "solana/nft/";
        private readonly string urlGetActivityOfSingleNFT = "solana/nft/activity/";
        private readonly string urlFetchNFTsByMintAddresses = "solana/nft/mints";
        private readonly string urlFetchMultiNFTsDataByOwnerAddresses = "solana/nft/owners";
        private readonly string urlFetchMultiNFTsDataByCreatorAddress = "solana/nft/creators";
        private readonly string urlFetchMultiNFTsDataByUpdateAuthorityAddress = "solana/nft/update-authorities";
        //create
        private readonly string urlCreateCollection = "solana/mint/collection";
        private readonly string urlMintLowerLevelCollection = "solana/mint/sub-collection";
        private readonly string urlMintNFTCollection = "solana/mint/nft";
        private readonly string urlUpdateNFT = "solana/mint/update";
        //list
        private readonly string urlListNFTOnTheMarketplace = "solana/marketplace/list";
        private readonly string urlUpdateListingOfNFTOnTheMarketplace = "solana/marketplace/update";
        private readonly string urlCancelListingOfNFTOnTheMarketplace = "solana/marketplace/cancel";
        //operate nft
        private readonly string urlTransferNFTToAnotherSolanaWallet = "solana/marketplace/transfer";
        private readonly string urlBuyNFTOnTheMarketplace = "solana/marketplace/buy";


        public void GetNFTDetails(string mintAddress, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlFetchSingleNFT + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url,null,(response)=> {

                callBack(response);
            }));
        }

        public void GetActivityOfSingleNFT(string mintAddress, Action<CommonResponse<ActivityOfSingleNftResponse>> callBack)
        {
            string url = GetAPIRoot() + urlGetActivityOfSingleNFT + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                CommonResponse<ActivityOfSingleNftResponse> responseBody = JsonUtility.FromJson<CommonResponse<ActivityOfSingleNftResponse>>(response);

                callBack(responseBody);
            }));
        }

        public void CreateVerifiedCollection(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlCreateCollection;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody,(response)=> {

                callBack(response);

            }));
        }

        public void MintNFT(string rawRequestBody, Action<string> callBack)
        {
            CreateNftRequestNoMintID requestBodyNoMintID = new CreateNftRequestNoMintID();

            string url = GetAPIRoot() + urlMintNFTCollection;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("MintNft result:" + response);

                callBack(response);
            }));
        }

        public void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl,string confirmation, string mint_id,Action<CommonResponse<MintResponse>> callBack)
        {
            CreateNftRequestWithoutPayment requestBody = new CreateNftRequestWithoutPayment();

            requestBody.name = collectionName;
            requestBody.symbol = collectionSymbol;
            requestBody.url = collectionInfoUrl;
            requestBody.collection_mint = parentCollection;
            requestBody.mint_id = mint_id;
            requestBody.confirmation = confirmation;
            string rawRequestBody = JsonUtility.ToJson(requestBody);
            LogFlow("MintNFT request:" + rawRequestBody);

            CreateNftRequestNoMintID requestBodyNoMintID = new CreateNftRequestNoMintID();

            string url = GetAPIRoot() + urlMintNFTCollection;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("MintNft result:" + response);

                CommonResponse<MintResponse> responseBody = JsonUtility.FromJson<CommonResponse<MintResponse>>(response);

                callBack(responseBody);
            }));
        }

        public void FetchNftsByCreatorAddresses(List<string> creators, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            FetchMultipleNftsByCreatorsRequest requestBody = new FetchMultipleNftsByCreatorsRequest();

            requestBody.creators = creators;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlFetchMultiNFTsDataByCreatorAddress;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                //MultipleNFTsResponse nfts = responseBody.Data;

                callBack(responseBody);
            }));
        }

        public void FetchNFTsByMintAddresses(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlFetchNFTsByMintAddresses;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callBack));
        }

        public void GetNFTsOwnedByAddress(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlFetchMultiNFTsDataByOwnerAddresses;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                callBack(response);
            }));
        }

        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            FetchMultipleNftsByUpdateAuthoritiesRequest requestBody = new FetchMultipleNftsByUpdateAuthoritiesRequest();

            requestBody.update_authorities = updateAuthorities;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlFetchMultiNFTsDataByUpdateAuthorityAddress;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void ListNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlListNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("ListNFT result:" + response);

                callBack(response);

            }));
        }

        public void ListNFT(string mintAddress, float price,string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            ListNFT(mintAddress, price, "", confirmation, callBack);
        }

        public void UpdateNFTListing(string mintAddress, double price,string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            ListNftOnMarketplaceRequest requestBody = new ListNftOnMarketplaceRequest();

            requestBody.mint_address = mintAddress;

            requestBody.price = price.ToString();

            requestBody.auction_house = auction_house;

            requestBody.confirmation = confirmation;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlUpdateListingOfNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("UpdateNFTListing result:" + response);

                CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void UpdateNFTListing(string mintAddress, float price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            UpdateNFTListing(mintAddress, price, "", confirmation, callBack);
        }

        public void CancelNFTListing(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlCancelListingOfNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("CancelNFTListing result:" + response);

                callBack(response);

            }));
        }

        //public void CancelNFTListing(string mintAddress, double price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        //{
        //    CancelNFTListing(mintAddress, price, "", confirmation, callBack);
        //}

        public void TransferNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlTransferNFTToAnotherSolanaWallet;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferNFT result:" + response);

                callBack(response);

            }));
        }

        public void BuyNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlBuyNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                callBack(response);

            }));
        }

        //public void BuyNFT(string mintAddress, float price, Action<CommonResponse<ListingResponse>> callBack)
        //{
        //    BuyNFT(mintAddress, price, "", callBack);
        //}

        public void UpdateNFTProperties(string rawRequestBody, Action<string> callBack)
        {
            string url = GetAPIRoot() + urlUpdateNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("UpdateNFT result:" + response);

                callBack(response);
            }));
        }
    }
}

