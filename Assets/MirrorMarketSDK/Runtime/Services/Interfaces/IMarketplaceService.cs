﻿using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IMarketplaceService
    {
        //create
        public void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack);
        
        public void CreateVerifiedSubCollection(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack);
        
        public void MintNft(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, Action<CommonResponse<MintResponse>> callBack);

        //fetch
        public void FetchNFTsByMintAddresses(List<string> mintAddresses, Action<MultipleNFTsResponse> callBack);
        
        public void FetchNftsByCreatorAddresses(List<string> creators, Action<MultipleNFTsResponse> callBack);
        
        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void FetchNftsByOwnerAddresses(List<string> owners, Action<MultipleNFTsResponse> callBack);
        
        public void FetchSingleNFT(string mintAddress, Action<SingleNFTResponseObj> callBack);

        //list
        public void ListNFT(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack);
        
        public void UpdateNFTListing(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack);
        
        public void CancelNFTListing(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack);

        //buy
        public void BuyNFT(string mintAddress, decimal price, Action<CommonResponse<ListingResponse>> callBack);

        //transfer
        public void TransferNFT(string mintAddress, string walletAddress, Action<CommonResponse<ListingResponse>> callBack);
    }
}