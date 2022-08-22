using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IMarketplaceService
    {
        public void CreateCollection(CreateCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public void CreateSubCollection(CreateSubCollectionRequest requestBody, string mintAddress, Action<CommonResponse<MintResponse>> callBack);
        
        public void CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public void FetchNFTsByMintAddress(List<string> mintAddresses, Action<MultipleNFTsResponse> callBack);
        
        public void FetchNftsByCreators(List<string> creators, Action<MultipleNFTsResponse> callBack);
        
        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<MultipleNFTsResponse> callBack);
        
        public void FetchNftsByOwners(List<string> owners, Action<MultipleNFTsResponse> callBack);
        
        public void FetchSingleNft(string mintAddress, Action<SingleNFTResponseObj> callBack);
        
        public void ListNftOnMarketplace(ListNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public void UpdateNftListOnMarketplace(UpdateNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public void CancelNftListOnMarketplace(CancelNftListOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public void BuyNftOnMarketplace(BuyNftOnMarketplaceRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
        
        public void TransferNft(TransferNftRequest requestBody, string accessToken, Action<CommonResponse<ListingResponse>> callBack);
    }
}