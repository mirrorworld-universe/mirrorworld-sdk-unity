using System;
using System.Collections;
using MirrorworldSDK.Models;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IMarketplaceService
    {
        public IEnumerator CreateCollection(CreateCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator CreateSubCollection(CreateSubCollectionRequest requestBody, string mintAddress, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator FetchSingleNftDetails(string mintAddress, Action<CommonResponse<SingleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByMintAddresses(FetchMultipleNftsByMintAddressesRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByCreators(FetchMultipleNftsByCreatorsRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByUpdateAuthorities(FetchMultipleNftsByUpdateAuthoritiesRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByOwners(FetchMultipleNftsByOwnersRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator ActivityOfSingleNft(string mintAddress, Action<CommonResponse<ActivityOfSingleNftResponse>> callBack);
        
        public IEnumerator ListNftOnMarketplace(ListNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public IEnumerator UpdateNftListOnMarketplace(UpdateNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public IEnumerator CancelNftListOnMarketplace(CancelNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public IEnumerator BuyNftOnMarketplace(BuyNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public IEnumerator TransferNft(TransferNftRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
    }
}