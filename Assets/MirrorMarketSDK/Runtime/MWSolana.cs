﻿using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK;

namespace MirrorWorld
{
    public class Solana
    {
        public static void StartLogin(Action<LoginResponse> action)
        {
            MWClientWrapper.StartLogin(action);
        }

        public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            MWClientWrapper.LoginWithEmail(emailAddress,password,callBack);
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
            MWClientWrapper.QueryUser(email,callback);
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
        public static void BuyNFT(string mint_address, double price, string auction_house, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.BuyNFT(mint_address,price,auction_house,approveFinished,callBack);
        }

        public static void CancelListing(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.CancelListing(mint_address,price,auction_house,confirmation,approveFinished,callBack);
        }

        public static void ListNFT(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.ListNFT(mint_address, price, auction_house, confirmation, approveFinished, callBack);
        }

        public static void TransferNFT(string mint_address, string to_wallet_address, Action approveAction, Action<CommonResponse<ListingResponse>> callBack)
        {
            MWSolanaWrapper.TransferNFT(mint_address, to_wallet_address, approveAction, callBack);
        }

        //Asset/Confirmation
        public static void CheckTransactionsStatus(List<string> signatures, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MWSolanaWrapper.CheckTransactionsStatus(signatures, callBack);
        }

        public static void CheckMintingStatus(List<string> mintAddresses, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MWSolanaWrapper.CheckMintingStatus(mintAddresses, callBack);
        }

        //Asset/Mint
        public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, int seller_fee_basis_points, string confirmation, Action approveFinished, Action<CommonResponse<MintResponse>> callBack)
        {
            MWSolanaWrapper.MintCollection(collectionName, collectionSymbol, NFTDetailJson, seller_fee_basis_points, confirmation, approveFinished, callBack);
        }

        public static void MintNFT(string parentCollection, string nFTName, string nFTSymbol, string nFTJsonUrl, string confirmation, string mint_id, string receiveWallet, double amountSol, Action approveFinished, Action<CommonResponse<MintResponse>> callBack)
        {
            LogUtils.LogFlow("Mint request:amountSol:" + amountSol + ",receiveWallet:" + receiveWallet);
            MWSolanaWrapper.MintNFT(parentCollection, nFTName, nFTSymbol, nFTJsonUrl, confirmation, mint_id, receiveWallet, amountSol, approveFinished, callBack);
        }

        public static void UpdateNFT(string mintAddress, string NFTName, string symbol, string updateAuthority, string NFTJsonUrl, int seller_fee_basis_points, string confirmation, Action approveAction, Action<CommonResponse<MintResponse>> callBack)
        {
            MWSolanaWrapper.UpdateNFT(mintAddress, NFTName, symbol, updateAuthority, NFTJsonUrl, seller_fee_basis_points, confirmation, approveAction, callBack);
        }

        //Asset/Search
        public static void QueryNFT(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
        {
            MWSolanaWrapper.QueryNFT(mintAddress, action);
        }

        public static void SearchNFTsByMintAddress(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
        {
            MWSolanaWrapper.SearchNFTsByMintAddress(mintAddresses, action);
        }

        public static void SearchNFTsByOwner(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            MWSolanaWrapper.SearchNFTsByOwner(owners, limit, offset, callBack);
        }

        //Wallet
        public static void GetTransactions(double number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MWSolanaWrapper.GetTransactions(number, nextBefore, action);
        }

        public static void GetTransactionsByWallet(string wallet_address, int limit, string next_before, Action<string> action)
        {
            MWSolanaWrapper.GetTransactionsByWallet(wallet_address, limit, next_before, action);
        }

        public static void GetTransactionsBySignature(string signature, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MWSolanaWrapper.GetTransactionsBySignature(signature, action);
        }

        public static void GetTokens(Action<CommonResponse<WalletTokenResponse>> action)
        {
            MWSolanaWrapper.GetTokens(action);
        }

        public static void GetTokensByWalletByWallet(string wallet, int limit, string next_before, Action<string> action)
        {
            MWSolanaWrapper.GetTokensByWalletByWallet(wallet,limit,next_before, action);
        }

        public static void TransferSol(int amount, string to_publickey, string confirmation, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWSolanaWrapper.TransferSol(amount, to_publickey, confirmation, approveFinished, callBack);
        }

        public static void TransferToken(string token_mint, int decimals, ulong amount, string to_publickey, Action approveFinished, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            MWSolanaWrapper.TransferToken(token_mint, decimals, amount, to_publickey, approveFinished, callBack);
        }

        //Metadata/Collections
        public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            MWSolanaWrapper.MetadataCollectionsInfo(collections, callback);
        }

        public static void MetadataCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            MWSolanaWrapper.MetadataCollectionFilters(collection, callBack);
        }

        public static void MetadataCollectionsSummary(List<string> collections, Action<string> action)
        {
            MWSolanaWrapper.MetadataCollectionsSummary(collections, action);
        }

        //Metadata/NFT
        public static void MetadataNFTInfo(string mintAddress, Action<string> callBack)
        {
            MWSolanaWrapper.MetadataNFTInfo(mintAddress, callBack);
        }

        public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
        {
            MWSolanaWrapper.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, filters, callback);
        }

        public static void MetadataNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
        {
            MWSolanaWrapper.MetadataNFTEvents(mintAddress, page, pageSize, callback);
        }

        public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MWSolanaWrapper.MetadataSearchNFTs(collections, searchString, callback);
        }

        public static void MetadataRecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MWSolanaWrapper.MetadataRecommendSearchNFTs(collections, callback);
        }
    }
}
