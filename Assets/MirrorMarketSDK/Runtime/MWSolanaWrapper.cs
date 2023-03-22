using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.Models;
using System.Collections.Generic;

namespace MirrorworldSDK
{
    public class MWSolanaWrapper
    {
        //Asset/Auction
        public static void BuyNFT(string mint_address, double price, string auction_house, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            ApproveListNFT requestParams = new ApproveListNFT();
            requestParams.mint_address = mint_address;
            requestParams.price = price.ToString();
            requestParams.auction_house = auction_house;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.BuyNFT, "buy nft", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.BuyNFT(mint_address, price, auction_house, callBack);
            });
        }


        public static void CancelListing(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            ApproveListNFT requestParams = new ApproveListNFT();
            requestParams.mint_address = mint_address;
            requestParams.price = price.ToString();
            requestParams.confirmation = confirmation;
            requestParams.auction_house = auction_house;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.CancelListing, "cancel list nft", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.CancelNFTListing(mint_address, price, auction_house, confirmation, callBack);
            });
        }

        public static void ListNFT(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            ApproveListNFT requestParams = new ApproveListNFT();
            requestParams.mint_address = mint_address;
            requestParams.price = price.ToString();
            requestParams.confirmation = confirmation;
            requestParams.auction_house = auction_house;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.ListNFT, "list nft", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.ListNFT(mint_address, price, auction_house, confirmation, callBack);
            });
        }

        public static void TransferNFT(string mint_address, string to_wallet_address, Action approveAction, Action<CommonResponse<ListingResponse>> callBack)
        {
            ApproveTransferNFT requestParams = new ApproveTransferNFT();
            requestParams.mint_address = mint_address;
            requestParams.to_wallet_address = to_wallet_address;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferNFT, "transfer nft", requestParams, () => {
                if (approveAction != null)
                {
                    approveAction();
                }
                MirrorWrapper.Instance.TransferNFT(mint_address, to_wallet_address, callBack);
            });
        }

        //Asset/Confirmation
        public static void CheckTransactionsStatus(List<string> signatures, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MirrorWrapper.Instance.GetStatusOfTransactions(signatures, callBack);
        }

        public static void CheckMintingStatus(List<string> mintAddresses, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MirrorWrapper.Instance.GetStatusOfMintings(mintAddresses, callBack);
        }

        //Asset/Mint
        public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, int seller_fee_basis_points, string confirmation, Action approveFinished, Action<CommonResponse<MintResponse>> callBack)
        {
            ApproveCreateCollection requestParams = new ApproveCreateCollection();
            requestParams.name = collectionName;
            requestParams.symbol = collectionSymbol;
            requestParams.url = NFTDetailJson;
            requestParams.confirmation = confirmation;
            requestParams.seller_fee_basis_points = seller_fee_basis_points;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.CreateCollection, "create collection", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.CreateVerifiedCollection(collectionName, collectionSymbol, NFTDetailJson, seller_fee_basis_points, confirmation, callBack);
            });
        }

        public static void MintNFT(string parentCollection, string nFTName, string nFTSymbol, string nFTJsonUrl, string confirmation, string mint_id, string receiveWallet, double amountSol, Action approveFinished, Action<CommonResponse<MintResponse>> callBack)
        {
            ApproveMintNFT requestParams = new ApproveMintNFT();
            requestParams.collection_mint = parentCollection;
            requestParams.name = nFTName;
            requestParams.symbol = nFTSymbol;
            requestParams.url = nFTJsonUrl;

            if (confirmation == null || confirmation == "")
            {
                confirmation = Confirmation.Confirmed;
            }
            requestParams.confirmation = confirmation;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.MintNFT, "mint nft", requestParams, () =>
            {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.MintNFT(parentCollection, nFTName, nFTSymbol, nFTJsonUrl, confirmation, mint_id, receiveWallet, amountSol, callBack);
            });
        }

        public static void UpdateNFT(string mintAddress, string NFTName, string symbol, string updateAuthority, string NFTJsonUrl, int seller_fee_basis_points, string confirmation, Action approveAction, Action<CommonResponse<MintResponse>> callBack)
        {
            ApproveUpdateNFTProperties requestParams = new ApproveUpdateNFTProperties();
            requestParams.mint_address = mintAddress;
            requestParams.name = NFTName;
            requestParams.confirmation = confirmation;
            requestParams.symbol = symbol;
            requestParams.seller_fee_basis_points = seller_fee_basis_points;
            requestParams.update_authority = updateAuthority;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.UpdateNFT, "update nft", requestParams, () => {
                if (approveAction != null)
                {
                    approveAction();
                }
                MirrorWrapper.Instance.UpdateNFTProperties(mintAddress, NFTName, symbol, updateAuthority, NFTJsonUrl, seller_fee_basis_points, confirmation, callBack);
            });
        }

        //Asset/Search
        public static void QueryNFT(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
        {
            MirrorWrapper.Instance.GetNFTDetails(mintAddress, action);
        }

        public static void SearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MirrorWrapper.Instance.SearchNFTs(collections, searchString, callback);
        }

        public static void SearchNFTsByOwner(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            MirrorWrapper.Instance.GetNFTsOwnedByAddress(owners, limit, offset, callBack);
        }

        //Wallet
        public static void GetTransactions(double number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MirrorWrapper.Instance.GetWalletTransactions(number, nextBefore, action);
        }

        public static void GetTransactionsByWallet()
        {

        }

        public static void GetTransactionsBySignature(string signature, Action<CommonResponse<TransferTokenResponse>> action)
        {
            MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, action);
        }

        public static void GetTokens(Action<CommonResponse<WalletTokenResponse>> action)
        {
            MirrorWrapper.Instance.GetWalletTokens(action);
        }

        public static void GetTokensByWallet()
        {

        }

        public static void TransferSol(int amount, string to_publickey, string confirmation, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
        {
            ApproveTransferSOL requestParams = new ApproveTransferSOL();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;

            string approveValue = PrecisionUtil.GetApproveValue(amount);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSol, approveValue, "transfer sol", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.TransferSol(amount, to_publickey, confirmation, callBack);
            });
        }

        public static void TransferToken(string token_mint, int decimals, ulong amount, string to_publickey, Action approveFinished, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            ApproveTransferSPLToken requestParams = new ApproveTransferSPLToken();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;
            requestParams.token_mint = token_mint;
            requestParams.decimals = decimals;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSPLToken, "transfer spl token", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                MirrorWrapper.Instance.TransferSPLToken(token_mint, decimals, amount, to_publickey, callBack);
            });
        }

        //Metadata/Collections
        public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            MirrorWrapper.Instance.GetCollectionInfo(collections, callback);
        }

        public static void MetadataCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            MirrorWrapper.Instance.GetCollectionFilterInfo(collection, callBack);
        }

        public static void MetadataCollectionsSummary()
        {

        }

        //Metadata/NFT
        public static void MetadataNFTInfo(string mintAddress, Action<string> callBack)
        {
            MirrorWrapper.Instance.GetNFTInfo(mintAddress, callBack);
        }

        public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
        {
            MirrorWrapper.Instance.GetNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, filters, callback);
        }

        public static void MetadataNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
        {
            MirrorWrapper.Instance.GetNFTEvents(mintAddress, page, pageSize, callback);
        }

        public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MirrorWrapper.Instance.SearchNFTs(collections, searchString, callback);
        }

        public static void MetadataRecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            MirrorWrapper.Instance.RecommendSearchNFT(collections, callback);
        }
    }
}
