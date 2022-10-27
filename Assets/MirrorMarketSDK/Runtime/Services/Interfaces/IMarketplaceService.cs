using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IMarketplaceService
    {
        //create
        public void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack);
        
        public void CreateVerifiedSubCollection(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack);
        
        public void MintNft(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack);

        //fetch
        public void FetchNFTsByMintAddresses(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void FetchNftsByCreatorAddresses(List<string> creators, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void GetNFTsOwnedByAddress(List<string> owners, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void GetNFTDetails(string mintAddress, Action<CommonResponse<SingleNFTResponse>> callBack);

        //list
        public void ListNFT(string mintAddress, float price,string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);
        
        public void UpdateNFTListing(string mintAddress, float price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);
        
        public void CancelNFTListing(string mintAddress, float price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);

        //buy
        public void BuyNFT(string mintAddress, float price, string auction_house, Action<CommonResponse<ListingResponse>> callBack);

        //transfer
        public void TransferNFT(string mintAddress, string walletAddress, Action<CommonResponse<ListingResponse>> callBack);
    }
}