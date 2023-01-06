using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IMarketplaceService
    {
        //create
        public void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl, int seller_fee_basis_points, string confirmation, Action<CommonResponse<MintResponse>> callBack);
        
        //public void CreateVerifiedSubCollection(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, Action<CommonResponse<MintResponse>> callBack);

        public void MintNFT(string parentCollection, string collectionName, string collectionSymbol, string collectionInfoUrl, string confirmation, string mint_id, Action<CommonResponse<MintResponse>> callBack);

        public void UpdateNFT(string mintAddress, string name, string symbol, string updateAuthority, string NFTJsonUrl,int seller_fee_basis_points, string confirmation, Action<CommonResponse<MintResponse>> callBack);

        //fetch
        public void FetchNFTsByMintAddresses(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void FetchNftsByCreatorAddresses(List<string> creators, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void FetchNftsByUpdateAuthorities(List<string> updateAuthorities, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void GetNFTsOwnedByAddress(List<string> owners,long limit,long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack);
        
        public void GetNFTDetails(string mintAddress, Action<CommonResponse<SingleNFTResponse>> callBack);

        //list
        public void ListNFT(string mintAddress, double price,string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);
        
        public void UpdateNFTListing(string mintAddress, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);
        
        public void CancelNFTListing(string mintAddress, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack);

        //buy
        public void BuyNFT(string mintAddress, double price, string auction_house, Action<CommonResponse<ListingResponse>> callBack);

        //transfer
        public void TransferNFT(string mintAddress, string walletAddress, Action<CommonResponse<ListingResponse>> callBack);
    }
}