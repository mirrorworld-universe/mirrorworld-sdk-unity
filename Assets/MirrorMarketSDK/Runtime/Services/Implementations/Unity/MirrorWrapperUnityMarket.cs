using System;
using System.Collections.Generic;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IMarketplaceService
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


        public void GetNFTDetails(string mintAddress, Action<CommonResponse<SingleNFTResponse>> callBack)
        {
            string url = GetAPIRoot() + urlFetchSingleNFT + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url,null,(response)=> {

                CommonResponse<SingleNFTResponse> responseBody = JsonUtility.FromJson<CommonResponse<SingleNFTResponse>>(response);

                callBack(responseBody);
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

        public void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl,int seller_fee_basis_points, string confirmation, Action<CommonResponse<MintResponse>> callBack)
        {
            CreateCollectionRequest requestBody = new CreateCollectionRequest();

            requestBody.name = collectionName;
            requestBody.symbol = collectionSymbol;
            requestBody.url = collectionInfoUrl;
            requestBody.seller_fee_basis_points = seller_fee_basis_points;
            if (confirmation != null) requestBody.comfirmation = confirmation;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlCreateCollection;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody,(response)=> {

                CommonResponse<MintResponse> responseBody = JsonUtility.FromJson<CommonResponse<MintResponse>>(response);

                callBack(responseBody);

            }));
        }

        //public void CreateVerifiedSubCollection(string parentCollection,string collectionName, string collectionSymbol, string collectionInfoUrl,string confirmation, Action<CommonResponse<MintResponse>> callBack)
        //{
        //    CreateSubCollectionRequest requestBody = new CreateSubCollectionRequest();

        //    requestBody.name = collectionName;
        //    requestBody.symbol = collectionSymbol;
        //    requestBody.url = collectionInfoUrl;
        //    requestBody.collection_mint = parentCollection;
        //    if (confirmation != null) requestBody.confirmation = confirmation;

        //    var rawRequestBody = JsonUtility.ToJson(requestBody);

        //    string url = GetAPIRoot() + urlMintLowerLevelCollection;

        //    monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

        //        CommonResponse<MintResponse> responseBody = JsonUtility.FromJson<CommonResponse<MintResponse>>(response);

        //        callBack(responseBody);

        //    }));
        //}

        public void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl,string confirmation, string mint_id,Action<CommonResponse<MintResponse>> callBack)
        {
            CreateNftRequest requestBody = new CreateNftRequest();

            requestBody.name = collectionName;
            requestBody.symbol = collectionSymbol;
            requestBody.url = collectionInfoUrl;
            requestBody.collection_mint = parentCollection;
            requestBody.mint_id = mint_id;
            requestBody.confirmation = confirmation;
            string rawRequestBody = JsonUtility.ToJson(requestBody);

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

        public void FetchNFTsByMintAddresses(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            FetchMultipleNftsByMintAddressesRequest requestBody = new FetchMultipleNftsByMintAddressesRequest();

            requestBody.mint_addresses = mintAddresses;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlFetchNFTsByMintAddresses;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response)=> {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                //MultipleNFTsResponse nfts = responseBody.Data;

                callBack(responseBody);
            }));
        }

        public void GetNFTsOwnedByAddress(List<string> owners,long limit,long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            FetchMultipleNftsByOwnersRequest requestBody = new FetchMultipleNftsByOwnersRequest();

            requestBody.owners = owners;

            requestBody.limit = limit;

            requestBody.offset = offset;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlFetchMultiNFTsDataByOwnerAddresses;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                //MultipleNFTsResponse nfts = responseBody.Data;

                callBack(responseBody);
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

        public void ListNFT(string mintAddress, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            ListNftOnMarketplaceRequest requestBody = new ListNftOnMarketplaceRequest();

            requestBody.mint_address = mintAddress;

            requestBody.confirmation = confirmation;

            requestBody.price = price.ToString();

            requestBody.auction_house = auction_house;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlListNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("ListNFT result:" + response);

                CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                callBack(responseBody);

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

        public void CancelNFTListing(string mintAddress, double price,string auction_house,string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            CancelNftListOnMarketplaceRequest requestBody = new CancelNftListOnMarketplaceRequest();

            requestBody.mint_address = mintAddress;

            requestBody.price = price;

            requestBody.auction_house = auction_house;

            requestBody.confirmation = confirmation;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlCancelListingOfNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("CancelNFTListing result:" + response);

                CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void CancelNFTListing(string mintAddress, double price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
        {
            CancelNFTListing(mintAddress, price, "", confirmation, callBack);
        }

        public void TransferNFT(string mintAddress, string walletAddress, Action<CommonResponse<ListingResponse>> callBack)
        {
            TransferNftRequest requestBody = new TransferNftRequest();

            requestBody.mint_address = mintAddress;

            requestBody.to_wallet_address = walletAddress;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlTransferNFTToAnotherSolanaWallet;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferNFT result:" + response);

                CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void BuyNFT(string mintAddress, double price,string auction_house, Action<CommonResponse<ListingResponse>> callBack)
        {
            BuyNftOnMarketplaceRequest requestBody = new BuyNftOnMarketplaceRequest();

            requestBody.mint_address = mintAddress;

            requestBody.price = price;

            requestBody.auction_house = auction_house;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            string url = GetAPIRoot() + urlBuyNFTOnTheMarketplace;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("BuyNFT result:"+response);

                CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                callBack(responseBody);

            }));
        }

        public void BuyNFT(string mintAddress, float price, Action<CommonResponse<ListingResponse>> callBack)
        {
            BuyNFT(mintAddress, price, "", callBack);
        }

        public void UpdateNFT(string mintAddress, string name, string symbol, string updateAuthority, string NFTJsonUrl, int seller_fee_basis_points, string confirmation, Action<CommonResponse<MintResponse>> callBack)
        {
            UpdateNFTRequest requestBody = new UpdateNFTRequest();

            requestBody.mint_address = mintAddress;
            requestBody.name = name;
            requestBody.symbol = symbol;
            requestBody.update_authority = updateAuthority;
            requestBody.url = NFTJsonUrl;
            requestBody.seller_fee_basis_points = seller_fee_basis_points;
            requestBody.confirmation = confirmation;
            string rawRequestBody = JsonUtility.ToJson(requestBody);


            string url = GetAPIRoot() + urlUpdateNFT;

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("UpdateNFT result:" + response);

                CommonResponse<MintResponse> responseBody = JsonUtility.FromJson<CommonResponse<MintResponse>>(response);

                callBack(responseBody);
            }));
        }
    }
}

