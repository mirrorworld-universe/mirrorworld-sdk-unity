using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorWorldResponses;

namespace MirrorWorld
{
    public class SolanaAsset
    {
        //Asset/Auction
        public void BuyNFT(string mint_address, double price, string auction_house, Action approveFinished, Action<CommonResponse<SolResBuyNFT>> callBack)
        {
            MWSolanaWrapper.BuyNFT(mint_address, price, auction_house, approveFinished, callBack);
        }

        public void CancelListing(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.CancelListing(mint_address, price, auction_house, confirmation, approveFinished, callBack);
        }

        public void ListNFT(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.ListNFT(mint_address, price, auction_house, confirmation, approveFinished, callBack);
        }

        public void TransferNFT(string mint_address, string to_wallet_address, Action approveAction, Action<CommonResponse<SolResTransferNFT>> callBack)
        {
            MWSolanaWrapper.TransferNFT(mint_address, to_wallet_address, approveAction, callBack);
        }

        //Asset/Confirmation
        public void CheckTransactionsStatus(List<string> signatures, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MWSolanaWrapper.CheckTransactionsStatus(signatures, callBack);
        }

        public void CheckMintingStatus(List<string> mintAddresses, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MWSolanaWrapper.CheckMintingStatus(mintAddresses, callBack);
        }

        //Asset/Mint
        public void CreateMarketplace(int seller_fee_basis_points, EVMReqStorefrontObj storefrontObj, List<string> collections, Action<CommonResponse<SolResCreateMarket>> callBack)
        {
            MWSolanaWrapper.CreateMarketplace(seller_fee_basis_points,storefrontObj,collections,callBack);
        }
        public void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, int seller_fee_basis_points, string confirmation, Action approveFinished, Action<CommonResponse<SolResMintResponse>> callBack)
        {
            MWSolanaWrapper.MintCollection(collectionName, collectionSymbol, NFTDetailJson, seller_fee_basis_points, confirmation, approveFinished, callBack);
        }

        public void MintNFT(string parentCollection, string nFTName, string nFTSymbol, string nFTJsonUrl, string confirmation, string mint_id, string receiveWallet, double amountSol, Action approveFinished, Action<CommonResponse<SolResMintResponse>> callBack)
        {
            LogUtils.LogFlow("Mint request:amountSol:" + amountSol + ",receiveWallet:" + receiveWallet);
            MWSolanaWrapper.MintNFT(parentCollection, nFTName, nFTSymbol, nFTJsonUrl, confirmation, mint_id, receiveWallet, amountSol, approveFinished, callBack);
        }

        public void UpdateNFT(string mintAddress, string NFTName, string symbol, string updateAuthority, string NFTJsonUrl, int seller_fee_basis_points, string confirmation, Action approveAction, Action<CommonResponse<SolResUpdateNFT>> callBack)
        {
            MWSolanaWrapper.UpdateNFT(mintAddress, NFTName, symbol, updateAuthority, NFTJsonUrl, seller_fee_basis_points, confirmation, approveAction, callBack);
        }

        //Asset/Search
        public void QueryNFT(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
        {
            MWSolanaWrapper.QueryNFT(mintAddress, action);
        }

        public void SearchNFTsByMintAddress(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
        {
            MWSolanaWrapper.SearchNFTsByMintAddress(mintAddresses, action);
        }

        public void SearchNFTsByOwner(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            MWSolanaWrapper.SearchNFTsByOwner(owners, limit, offset, callBack);
        }
    }

    public class SolanaWallet
    {
        //Wallet
        public void GetTransactions(int number, string nextBefore, Action<CommonResponse<SolResGetTransactions>> action)
        {
            MWSolanaWrapper.GetTransactions(number, nextBefore, action);
        }

        public void GetTransactionsByWallet(string wallet_address, int limit, string next_before, Action<CommonResponse<SolResGetTransactionByWallet>> action)
        {
            MWSolanaWrapper.GetTransactionsByWallet(wallet_address, limit, next_before, action);
        }

        public void GetTransactionsBySignature(string signature, Action<CommonResponse<SolResGetTransactionBySig>> action)
        {
            MWSolanaWrapper.GetTransactionsBySignature(signature, action);
        }

        public void GetTokens(Action<CommonResponse<WalletTokenResponse>> action)
        {
            MWSolanaWrapper.GetTokens(action);
        }

        public void GetTokensByWallet(string wallet, int limit, string next_before, Action<CommonResponse<WalletTokenResponse>> action)
        {
            MWSolanaWrapper.GetTokensByWalletByWallet(wallet, limit, next_before, action);
        }

        public void TransferSol(int amount, string to_publickey, string confirmation, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWSolanaWrapper.TransferSol(amount, to_publickey, confirmation, approveFinished, callBack);
        }

        public void TransferToken(string token_mint, int decimals, ulong amount, string to_publickey, Action approveFinished, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            MWSolanaWrapper.TransferToken(token_mint, decimals, amount, to_publickey, approveFinished, callBack);
        }
    }

    public class SolanaMetadata
    {

        //Metadata/Collections
        public void GetCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            MWSolanaWrapper.MetadataCollectionsInfo(collections, callback);
        }

        public void GetCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            MWSolanaWrapper.MetadataCollectionFilters(collection, callBack);
        }

        public void GetCollectionsSummary(List<string> collections, Action<CommonResponse<List<SolResMetadataGetCollectionSummary>>> action)
        {
            MWSolanaWrapper.MetadataCollectionsSummary(collections, action);
        }

        //Metadata/NFT
        public void GetNFTInfo(string mintAddress, Action<string> callBack)
        {
            MWSolanaWrapper.MetadataNFTInfo(mintAddress, callBack);
        }

        public void GetNFTs(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
        {
            MWSolanaWrapper.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, filters, callback);
        }

        public void GetNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
        {
            MWSolanaWrapper.MetadataNFTEvents(mintAddress, page, pageSize, callback);
        }

        public void SearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MWSolanaWrapper.MetadataSearchNFTs(collections, searchString, callback);
        }

        public void RecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MWSolanaWrapper.MetadataRecommendSearchNFTs(collections, callback);
        }
    }

    public class MirrorWorldSolana
    {
        public SolanaAsset Asset = new SolanaAsset();

        public SolanaWallet Wallet = new SolanaWallet();

        public SolanaMetadata Metadata = new SolanaMetadata();
    }
}
