using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;

namespace MirrorWorld
{
    public class EVM
    {
        //Client
        public static void StartLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.StartLogin(action);
        }

        public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            MWClientWrapper.LoginWithEmail(emailAddress, password, callBack);
        }

        public static void IsLogged(Action<bool> action)
        {
            MWClientWrapper.IsLoggedIn(action);
        }

        public static void GuestLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.GuestLogin(action);
        }

        public static void Logout(Action logoutAction)
        {
            MWClientWrapper.Logout(logoutAction);
        }

        public static void QueryUser(string email, Action<CommonResponse<UserResponse>> callback)
        {
            MWClientWrapper.QueryUser(email, callback);
        }

        public static void OpenWallet(Action walletLogoutAction)
        {
            MWClientWrapper.OpenWalletPage(walletLogoutAction);
        }

        public static void OpenMarket(string marketUrl)
        {
            MWClientWrapper.OpenMarketPage(marketUrl);
        }
        //Asset/Auction
        public static void BuyNFT(string collection_address, double price, int token_id, string marketplace_address, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.BuyNFT(collection_address, price, token_id, marketplace_address, approveFinished, callBack);
        }

        public static void CancelListing(string collection_address, int token_id, string marketplace_address, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.CancelListing(collection_address, token_id, marketplace_address, approveFinished, callBack);
        }

        public static void ListNFT(string collection_address, int token_id, double price, string marketplace_address, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.ListNFT(collection_address, token_id, price, marketplace_address, approveFinished, callBack);
        }

        public static void TransferNFT(string collection_address, int token_id, string to_wallet_address, Action approveAction, Action<string> callBack)
        {
            MWEVMWrapper.TransferNFT(collection_address, token_id, to_wallet_address, approveAction, callBack);
        }

        //Asset/Mint
        public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.MintCollection(collectionName, collectionSymbol, NFTDetailJson, approveFinished, callBack);
        }

        public static void MintNFT(string collection_address, int token_id, string to_wallet_address, int mint_amount, string confirmation, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.MintNFT(collection_address, token_id, to_wallet_address, mint_amount, confirmation, approveFinished, callBack);
        }

        //Asset/Search
        public static void QueryNFT(string mintAddress, string token_id, Action<string> action)
        {
            MWEVMWrapper.QueryNFT(mintAddress, token_id, action);
        }

        public static void SearchNFTsByOwner(string owner_address, int limit, string cursor, Action<string> action)
        {
            MWEVMWrapper.SearchNFTsByOwner(owner_address, limit, cursor, action);
        }

        public static void SearchNFTsByMintAddress(List<EVMSearchNFTsByAddressesReqToken> tokens, Action<string> callBack)
        {
            MWEVMWrapper.SearchNFTsByMintAddress(tokens, callBack);
        }


        //Wallet
        public static void GetTransactions(double number, Action<string> action)
        {
            MWEVMWrapper.GetTransactions(number, action);
        }

        public static void GetTransactionsByWallet(string wallet_address, int limit, Action<string> action)
        {
            MWEVMWrapper.GetTransactionsByWallet(wallet_address, limit, action);
        }

        public static void GetTransactionsBySignature(string signature, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MWEVMWrapper.GetTransactionsBySignature(signature, action);
        }

        public static void GetTokens(Action<string> action)
        {
            MWEVMWrapper.GetTokens(action);
        }

        public static void GetTokensByWalletByWallet(string wallet, Action<string> action)
        {
            MWEVMWrapper.GetTokensByWalletByWallet(wallet, action);
        }

        public static void TransferETH(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.TransferETH(nonce,gasPrice,gasLimit,to,amount,approveFinished,callBack);
        }

        public static void TransferToken(string nonce, string gasPrice, string gasLimit, string to, string amount, string contract, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.TransferToken(nonce, gasPrice, gasLimit, to, amount, contract, approveFinished, callBack);
        }

        //Metadata/Collections
        public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            MWEVMWrapper.MetadataCollectionsInfo(collections, callback);
        }

        public static void MetadataCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            MWEVMWrapper.MetadataCollectionFilters(collection, callBack);
        }

        public static void MetadataCollectionsSummary(List<string> collections, Action<string> action)
        {
            MWEVMWrapper.MetadataCollectionsSummary(collections, action);
        }

        //Metadata/NFT
        public static void MetadataNFTInfo(string contract, string token_id, Action<string> callBack)
        {
            MWEVMWrapper.MetadataNFTInfo(contract, token_id, callBack);
        }

        public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<EVMGetNFTsByParamsReqFilter> filters, Action<string> callback)
        {
            MWEVMWrapper.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, filters, callback);
        }

        public static void MetadataNFTEvents(string contract, int page, int pageSize, int token_id, string marketplace_address, Action<string> callback)
        {
            MWEVMWrapper.MetadataNFTEvents(contract, page, pageSize, token_id, marketplace_address, callback);
        }

        public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<string> callback)
        {
            MWEVMWrapper.MetadataSearchNFTs(collections, searchString, callback);
        }

        public static void MetadataRecommendSearchNFTs(List<string> collections, Action<string> callback)
        {
            MWEVMWrapper.MetadataCollectionsSummary(collections, callback);
        }
    }
}
