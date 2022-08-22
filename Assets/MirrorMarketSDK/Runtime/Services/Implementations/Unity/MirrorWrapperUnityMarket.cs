#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using System.Collections.Generic;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using Newtonsoft.Json;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IMarketplaceService
    {
        private readonly string urlFetchSingleNFT = Constant.ApiRoot + "solana/nft/";
        private readonly string urlFetchNFTsByMintAddresses = Constant.ApiRoot + "solana/nft/mints";

        public void FetchSingleNft(string mintAddress, Action<SingleNFTResponseObj> callBack)
        {
            string url = urlFetchSingleNFT + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url,null,(response)=> {

                CommonResponse<SingleNFTResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<SingleNFTResponse>>(response);
                SingleNFTResponseObj nft = responseBody.Data.nft;
                callBack(nft);
            }));
        }

        public void BuyNftOnMarketplace(BuyNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void CancelNftListOnMarketplace(CancelNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void CreateCollection(CreateCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void CreateSubCollection(CreateSubCollectionRequest requestBody, string mintAddress, Action<CommonResponse<MintResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void FetchNftsByCreators(List<string> creators, Action<MultipleNFTsResponse> callBack)
        {
            FetchMultipleNftsByCreatorsRequest requestBody = new FetchMultipleNftsByCreatorsRequest();
            requestBody.Creators = creators;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);
            monoBehaviour.StartCoroutine(CheckAndPost(urlFetchNFTsByMintAddresses, rawRequestBody, (response) => {
                CommonResponse<MultipleNFTsResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNFTsResponse>>(response);
                MultipleNFTsResponse nfts = responseBody.Data;
                callBack(nfts);
            }));
        }

        public void FetchNFTsByMintAddress(List<string> mintAddresses, Action<MultipleNFTsResponse> callBack)
        {
            FetchMultipleNftsByMintAddressesRequest requestBody = new FetchMultipleNftsByMintAddressesRequest();
            requestBody.MintAddresses = mintAddresses;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);
            monoBehaviour.StartCoroutine(CheckAndPost(urlFetchNFTsByMintAddresses, rawRequestBody, (response)=> {
                CommonResponse<MultipleNFTsResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNFTsResponse>>(response);
                MultipleNFTsResponse nfts = responseBody.Data;
                callBack(nfts);
            }));
        }

        public void FetchNftsByOwners(List<string> owners, Action<MultipleNFTsResponse> callBack)
        {
            FetchMultipleNftsByOwnersRequest requestBody = new FetchMultipleNftsByOwnersRequest();
            requestBody.Owners = owners;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);
            monoBehaviour.StartCoroutine(CheckAndPost(urlFetchNFTsByMintAddresses, rawRequestBody, (response) => {
                CommonResponse<MultipleNFTsResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNFTsResponse>>(response);
                MultipleNFTsResponse nfts = responseBody.Data;
                callBack(nfts);
            }));
        }

        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<MultipleNFTsResponse> callBack)
        {
            FetchMultipleNftsByUpdateAuthoritiesRequest requestBody = new FetchMultipleNftsByUpdateAuthoritiesRequest();
            requestBody.UpdateAuthorities = updateAuthorities;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);
            monoBehaviour.StartCoroutine(CheckAndPost(urlFetchNFTsByMintAddresses, rawRequestBody, (response) => {
                CommonResponse<MultipleNFTsResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNFTsResponse>>(response);
                MultipleNFTsResponse nfts = responseBody.Data;
                callBack(nfts);
            }));
        }

        public void ListNftOnMarketplace(ListNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void TransferNft(TransferNftRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void UpdateNftListOnMarketplace(UpdateNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack)
        {
            throw new NotImplementedException();
        }

    }
}
#endif

