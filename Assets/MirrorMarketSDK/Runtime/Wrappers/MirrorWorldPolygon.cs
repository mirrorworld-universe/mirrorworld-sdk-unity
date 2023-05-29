using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;
using MirrorWorldResponses;

namespace MirrorWorld
{
    public class MirrorWorldPolygon
    {
        public PolygonAsset Asset = new PolygonAsset();
        public PolygonWallet Wallet = new PolygonWallet();
        public PolygonMetadata Metadata = new PolygonMetadata();
    }

    public class PolygonMetadata
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

    public class PolygonWallet
    {
        //Wallet
        public void GetTransactions(int number, Action<CommonResponse<EVMResTransactions>> action)
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

        public void TransferNativeToken(string to_publickey, int amount, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWEVMWrapper.TransferNativeOnEVM(to_publickey, amount, callBack);
        }

        [Obsolete("This method is deprecated. Please use the NewMethod instead.")]
        public void TransferMatic(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            MWEVMWrapper.TransferMatic(nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
        }

        public void TransferToken(string to, string amount, string contract, Action approveFinished, Action<string> callBack)
        {
            MWEVMWrapper.TransferToken(to, amount, contract, approveFinished, callBack);
        }

        /**
         * Sign transaction and send to chain
         * Document: https://developer.mirrorworld.fun/#84998237-ba2f-46d7-9176-9aa1d5a63bce
         * 
         * nonce | string The transaction nonce, should be hex string
         * gasPrice | string The transaction gas price, should be hex string
         * gasLimit | string The transaction gas limit, should be hex string
         * to | string The receiver address
         * value | string The transaction value
         * data | string The transaction data
         */
        public void SignTransactionAndSend(string nonce, string gasPrice, string gasLimit, string to, string value, string data, Action<CommonResponse<EVMResSignTransactionAndSend>> action)
        {
            LogUtils.LogFlow("SignTransactionAndSend not support on Polygon for now.");
            return;
            MWEVMWrapper.SignTransactionAndSend(nonce, gasPrice, gasLimit, to, value, data, action);
        }
    }

    public class PolygonAsset
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

        public void ListNFT(string collection_address, int token_id, double price, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResListNFT>> callBack)
        {
            MWEVMWrapper.ListNFT(collection_address, token_id, price, marketplace_address, approveFinished, callBack);
        }

        public void TransferNFT(string collection_address, int token_id, string to_wallet_address, Action approveAction, Action<CommonResponse<EVMResTransferNFT>> callBack)
        {
            MWEVMWrapper.TransferNFT(collection_address, token_id, to_wallet_address, approveAction, callBack);
        }

        public void CreateMarketplace(int seller_fee_basis_points, string payment_token, EVMReqStorefrontObj storefrontObj, List<string> collections, string confirmation, Action<CommonResponse<EVMResCreateMarketplace>> callBack)
        {
            MWEVMWrapper.CreateMarketplace(seller_fee_basis_points,payment_token,storefrontObj,collections,confirmation,callBack);
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
