using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK;
using System.Collections.Generic;
using MirrorworldSDK.Models;

public class MWEVMWrapper
{
    //Asset/Auction
    public static void BuyNFT(string collection_address, double price, int token_id, string marketplace_address, Action approveFinished, Action<string> callBack)
    {
        EVMBuyNFTReq requestParams = new EVMBuyNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.price = price;
        requestParams.token_id = token_id;
        requestParams.marketplace_address = marketplace_address;

        string approveValue = PrecisionUtil.GetApproveValue(price);

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.BuyNFT, approveValue, "buy nft", requestParams, () => {
            if (approveFinished != null)
            {
                approveFinished();
            }

            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.BuyNFT(rawRequestBody, (response) => {

                callBack(response);

            });
        });
    }


    public static void CancelListing(string collection_address, int token_id, string marketplace_address, Action approveFinished, Action<string> callBack)
    {
        EVMCancelNFTReq requestParams = new EVMCancelNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.token_id = token_id;
        requestParams.marketplace_address = marketplace_address;

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.CancelListing, "cancel list nft", requestParams, () => {
            if (approveFinished != null)
            {
                approveFinished();
            }
            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.CancelNFTListing(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    public static void ListNFT(string collection_address, int token_id, double price, string marketplace_address, Action approveFinished, Action<string> callBack)
    {
        EVMListNFTReq requestParams = new EVMListNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.token_id = token_id;
        requestParams.price = price;
        requestParams.marketplace_address = marketplace_address;

        string approveValue = PrecisionUtil.GetApproveValue(price);

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.ListNFT, approveValue, "list nft", requestParams, () => {
            if (approveFinished != null)
            {
                approveFinished();
            }

            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.ListNFT(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    public static void TransferNFT(string collection_address, int token_id, string to_wallet_address, Action approveAction, Action<string> callBack)
    {
        EVMTransferNFTReq requestParams = new EVMTransferNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.token_id = token_id;
        requestParams.to_wallet_address = to_wallet_address;

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferNFT, "transfer nft", requestParams, () => {
            if (approveAction != null)
            {
                approveAction();
            }

            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.TransferNFT(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    //Asset/Mint
    public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, Action approveFinished, Action<string> callBack)
    {
        EVMMintCollectionReq requestParams = new EVMMintCollectionReq();
        requestParams.name = collectionName;
        requestParams.symbol = collectionSymbol;
        requestParams.url = NFTDetailJson;

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.CreateCollection, "create collection", requestParams, () => {
            if (approveFinished != null)
            {
                approveFinished();
            }
            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.CreateVerifiedCollection(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    public static void MintNFT(string collection_address, int token_id, string to_wallet_address, int mint_amount, string confirmation, Action approveFinished, Action<string> callBack)
    {
        EVMMintNFTReq requestParams = new EVMMintNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.token_id = token_id;
        requestParams.to_wallet_address = to_wallet_address;
        requestParams.mint_amount = mint_amount;
        requestParams.confirmation = confirmation;

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

            string rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.MintNFT(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    //Asset/Search
    public static void QueryNFT(string mintAddress, string token_id, Action<string> action)
    {
        MirrorWrapper.Instance.GetNFTDetailsOnEVM(mintAddress, token_id,(response) => {

            action(response);
        });
    }

    public static void SearchNFTsByOwner(string owner_address, int limit, string cursor, Action<string> action)
    {
        EVMSearchNFTsByOwner requestBody = new EVMSearchNFTsByOwner();

        requestBody.owner_address = owner_address;
        requestBody.limit = limit;
        requestBody.cursor = cursor;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetNFTsOwnedByAddressOnEVM(rawRequestBody, (response) => {

            action(response);
        });
    }

    public static void SearchNFTsByMintAddress(List<EVMSearchNFTsByAddressesReqToken> tokens, Action<string> callBack)
    {
        EVMSearchNFTsByAddressesReq requestBody = new EVMSearchNFTsByAddressesReq();

        requestBody.tokens = tokens;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.FetchNFTsByMintAddresses(rawRequestBody, (response) => {

            callBack(response);
        });
    }


    //Wallet
    public static void GetTransactions(double number, Action<string> action)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("limit", "" + number);

        MirrorWrapper.Instance.GetWalletTransactions(requestParams, (response) => {

            action(response);
        });
    }

    public static void GetTransactionsByWallet(string wallet_address, int limit, Action<string> action)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("limit", "" + limit);

        MirrorWrapper.Instance.GetWalletTransactionsByWallet(wallet_address, requestParams, action);
    }

    public static void GetTransactionsBySignature(string signature, Action<string> action)
    {
        MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, (response) => {

            //CommonResponse<TransferTokenResponse> responseBody = JsonUtility.FromJson<CommonResponse<TransferTokenResponse>>(response);

            action(response);
        });
    }

    public static void GetTokens(Action<string> action)
    {
        MirrorWrapper.Instance.GetWalletTokens((response) => {

            action(response);
        });
    }

    public static void GetTokensByWalletByWallet(string wallet, Action<string> action)
    {
        MirrorWrapper.Instance.GetWalletTokensByWallet(wallet, null, action);
    }

    public static void TransferETH(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<string> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-eth");

        TransferOnEVM(url,nonce,gasPrice,gasLimit,to,amount,approveFinished,callBack);
    }
    public static void TransferBNB(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<string> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-bnb");

        TransferOnEVM(url, nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
    }

    public static void TransferMatic(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<string> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-matic");

        TransferOnEVM(url, nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
    }

    private static void TransferOnEVM(string url, string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<string> callBack)
    {
        EVMTransferETHReq requestParams = new EVMTransferETHReq();
        requestParams.nonce = nonce;
        requestParams.gasPrice = gasPrice;
        requestParams.gasLimit = gasLimit;
        requestParams.to = to;
        requestParams.amount = amount;

        string approveValue = PrecisionUtil.GetApproveValue(PrecisionUtil.StrToInt(amount));

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferETH, approveValue, "transfer sol", requestParams, () => {
            if (approveFinished != null)
            {
                approveFinished();
            }

            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.TransferOnEVM(url, rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    public static void TransferToken(string nonce, string gasPrice, string gasLimit, string to, string amount, string contract, Action approveFinished, Action<string> callBack)
    {
        EVMTransferTokenReq requestParams = new EVMTransferTokenReq();
        requestParams.nonce = nonce;
        requestParams.gasPrice = gasPrice;
        requestParams.gasLimit = gasLimit;
        requestParams.to = to;
        requestParams.amount = amount;
        requestParams.contract = contract;

        string approveValue = PrecisionUtil.GetApproveValue(PrecisionUtil.StrToInt(amount));

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSPLToken, approveValue, "transfer spl token", requestParams, () => {

            if (approveFinished != null)
            {
                approveFinished();
            }

            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.TransferSPLToken(rawRequestBody, (response) => {

                callBack(response);
            });
        });
    }

    //Metadata/Collections
    public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
    {
        GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetCollectionInfo(rawRequestBody, (response) => {

            CommonResponse<List<GetCollectionInfoResponse>> responseBody = JsonUtility.FromJson<CommonResponse<List<GetCollectionInfoResponse>>>(response);

            callback(responseBody);
        });
    }

    public static void MetadataCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("collection", collection);

        MirrorWrapper.Instance.GetCollectionFilterInfo(requestParams, (response) => {

            CommonResponse<GetCollectionFilterInfoResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetCollectionFilterInfoResponse>>(response);

            callBack(responseBody);
        });
    }

    public static void MetadataCollectionsSummary(List<string> collections, Action<string> action)
    {
        GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetCollectionsSummary(rawRequestBody, action);
    }

    //Metadata/NFT
    public static void MetadataNFTInfo(string contract, string token_id, Action<string> callBack)
    {
        MirrorWrapper.Instance.GetNFTInfoOnEVM(contract, token_id, callBack);
    }

    public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<EVMGetNFTsByParamsReqFilter> filters, Action<string> callback)
    {
        EVMMetadataGetNFTsByParamsReq requestBody = new EVMMetadataGetNFTsByParamsReq();

        requestBody.contract = collection;

        requestBody.page = page;

        requestBody.page_size = pageSize;

        requestBody.order = new EVMGetNFTsByParamsReqOrder();

        requestBody.order.order_by = orderByString;

        requestBody.order.desc = desc;

        requestBody.filter = filters;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetNFTsByUnabridgedParams(rawRequestBody, (response) => {

            callback(response);
        });
    }

    public static void MetadataNFTEvents(string contract, int page, int pageSize,int token_id,string marketplace_address, Action<string> callback)
    {
        EVMMetadataGetNFTEventsReq requestBody = new EVMMetadataGetNFTEventsReq();

        requestBody.contract = contract;

        requestBody.page = page;

        requestBody.page_size = pageSize;

        requestBody.token_id = token_id;

        requestBody.marketplace_address = marketplace_address;


        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetNFTEvents(rawRequestBody, (response) => {

            callback(response);
        });
    }

    public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<string> callback)
    {
        EVMMetadataSearchNFTReq req = new EVMMetadataSearchNFTReq();

        req.collections = collections;

        req.search = searchString;

        string rawRequestBody = JsonUtility.ToJson(req);

        MirrorWrapper.Instance.SearchNFTs(rawRequestBody, (response) => {
            callback(response);
        });
    }

    public static void MetadataRecommendSearchNFTs(List<string> collections, Action<string> callback)
    {
        RecommendSearchNFTRequest requestBody = new RecommendSearchNFTRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.RecommendSearchNFT(rawRequestBody, (response) => {

            callback(response);
        });
    }
}
