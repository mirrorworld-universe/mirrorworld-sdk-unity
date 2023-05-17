using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;
using MirrorWorldResponses;

namespace MirrorWorld
{
    public class MirrorWorldBNB
    {
        public BNBAsset Asset = new BNBAsset();
        public BNBWallet Wallet = new BNBWallet();
        public BNBMetadata Metadata = new BNBMetadata();
        //Client
        //public void StartLogin(Action<LoginResponse> action)
        //{
        //    MWClientWrapper.StartLogin(action);
        //}

        //public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        //{
        //    MWClientWrapper.LoginWithEmail(emailAddress, password, callBack);
        //}

        //public void IsLogged(Action<bool> action)
        //{
        //    MWClientWrapper.IsLoggedIn(action);
        //}

        //public void GuestLogin(Action<LoginResponse> action)
        //{
        //    MWClientWrapper.GuestLogin(action);
        //}

        //public void Logout(Action logoutAction)
        //{
        //    MWClientWrapper.Logout(logoutAction);
        //}

        //public void QueryUser(string email, Action<string> callback)
        //{
        //    MWClientWrapper.QueryUser(email, callback);
        //}

        //public void OpenWallet(Action walletLogoutAction)
        //{
        //    MWClientWrapper.OpenWalletPage(walletLogoutAction);
        //}

        //public void OpenMarket(string marketUrl)
        //{
        //    MWClientWrapper.OpenMarketPage(marketUrl);
        //}
    }

    public class BNBMetadata
    {
        //Metadata/Collections
        public void GetCollectionsInfo(List<string> collections, Action<CommonResponse<List<EVMResMetadataCollectionInfo>>> callback)
        {
            MWEVMWrapper.MetadataCollectionsInfo(collections, callback);
        }

        public void GetCollectionFilters(string collection, Action<CommonResponse<EVMResMetadataCollectionFilter>> callBack)
        {
            MWEVMWrapper.MetadataCollectionFilters(collection, callBack);
        }

        public void GetCollectionsSummary(List<string> collections, Action<CommonResponse<List<EVMResMetadataCollectionsSummary>>> action)
        {
            MWEVMWrapper.MetadataCollectionsSummary(collections, action);
        }

        //Metadata/NFT
        public void GetNFTInfo(string contract, string token_id, Action<CommonResponse<EVMResMetadataNFTInfo>> callBack)
        {
            MWEVMWrapper.MetadataNFTInfo(contract, token_id, callBack);
        }

        public void GetNFTs(string collection, int page, int pageSize, string orderByString, bool desc, List<EVMGetNFTsByParamsReqFilter> filters, Action<CommonResponse<EVMResMetadataNFTs>> callback)
        {
            MWEVMWrapper.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, filters, callback);
        }

        public void GetNFTEvents(string contract, int page, int pageSize, int token_id, string marketplace_address, Action<CommonResponse<EVMResMetadataNFTEvents>> callback)
        {
            MWEVMWrapper.MetadataNFTEvents(contract, page, pageSize, token_id, marketplace_address, callback);
        }

        public void SearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<EVMResMetadataSearchNFTs>>> callback)
        {
            MWEVMWrapper.MetadataSearchNFTs(collections, searchString, callback);
        }

        public void RecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<EVMResMetadataRecommendNFTs>>> callback)
        {
            MWEVMWrapper.MetadataRecommendSearchNFTs(collections, callback);
        }
    }

    public class BNBWallet
    {
        //Wallet
        public void GetTransactions(double number, Action<CommonResponse<EVMResTransactions>> action)
        {
            MWEVMWrapper.GetTransactions(number, action);
        }

        public void GetTransactionsByWallet(string wallet_address, int limit, Action<CommonResponse<EVMResGetTransactionsByWallet>> action)
        {
            MWEVMWrapper.GetTransactionsByWallet(wallet_address, limit, action);
        }

        public void GetTransactionsBySignature(string signature, Action<CommonResponse<EVMResTransactionData>> action)
        {
            MWEVMWrapper.GetTransactionsBySignature(signature, action);
        }

        public void GetTokens(Action<CommonResponse<EVMResGetTokens>> action)
        {
            MWEVMWrapper.GetTokens(action);
        }

        public void GetTokensByWalletByWallet(string wallet, Action<string> action)
        {
            MWEVMWrapper.GetTokensByWalletByWallet(wallet, action);
        }

        public void TransferETH(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWEVMWrapper.TransferETH(nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
        }

        public void TransferBNB(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWEVMWrapper.TransferBNB(nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
        }

        public void TransferToken(string to, string amount, string contract, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.TransferToken(to, amount, contract, approveFinished, callBack);
        }
    }

    public class BNBAsset
    {
        //Asset/Auction
        public void BuyNFT(string collection_address, double price, int token_id, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResBuyNFT>> callBack)
        {
            MWEVMWrapper.BuyNFT(collection_address, price, token_id, marketplace_address, approveFinished, callBack);
        }

        public void CancelListing(string collection_address, int token_id, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResCancelList>> callBack)
        {
            MWEVMWrapper.CancelListing(collection_address, token_id, marketplace_address, approveFinished, callBack);
        }

        public void ListNFT(string collection_address, int token_id, double price, string marketplace_address, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.ListNFT(collection_address, token_id, price, marketplace_address, approveFinished, callBack);
        }

        public void TransferNFT(string collection_address, int token_id, string to_wallet_address, Action approveAction, Action<CommonResponse<EVMResTransferNFT>> callBack)
        {
            MWEVMWrapper.TransferNFT(collection_address, token_id, to_wallet_address, approveAction, callBack);
        }

        //Asset/Mint
        public void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, Action approveFinished, Action<CommonResponse<EVMResMintCollection>> callBack)
        {
            MWEVMWrapper.MintCollection(collectionName, collectionSymbol, NFTDetailJson, approveFinished, callBack);
        }

        public void MintNFT(string collection_address, int token_id, string url, string to_wallet_address, int mint_amount, string confirmation, Action approveFinished, Action<CommonResponse<EVMResMintNFT>> callBack)
        {
            MWEVMWrapper.MintNFT(collection_address, token_id, url, to_wallet_address, mint_amount, confirmation, approveFinished, callBack);
        }

        //Asset/Search
        public void QueryNFT(string mintAddress, string token_id, Action<CommonResponse<EVMResNFTInfo[]>> action)
        {
            MWEVMWrapper.QueryNFT(mintAddress, token_id, action);
        }

        public void SearchNFTsByOwner(string owner_address, int limit, string cursor, Action<CommonResponse<NFTDataResponse>> action)
        {
            MWEVMWrapper.SearchNFTsByOwner(owner_address, limit, cursor, action);
        }

        public void SearchNFTsByMintAddress(List<EVMSearchNFTsByAddressesReqToken> tokens, Action<CommonResponse<EVMResNFTInfo[]>> callBack)
        {
            MWEVMWrapper.SearchNFTsByMintAddress(tokens, callBack);
        }
    }
}
