using UnityEngine;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using MirrorWorldResponses;

public class MWEVMWrapper
{
    //Asset/Auction
    public static void BuyNFT(string collection_address, double price, int token_id, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResBuyNFT>> callBack)
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
                CommonResponse<EVMResBuyNFT> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResBuyNFT>>(response);
                callBack(commonResponse);

            });
        });
    }


    public static void CancelListing(string collection_address, int token_id, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResCancelList>> callBack)
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
                CommonResponse<EVMResCancelList> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResCancelList>>(response);
                callBack(commonResponse);
            });
        });
    }

    public static void ListNFT(string collection_address, int token_id, double price, string marketplace_address, Action approveFinished, Action<CommonResponse<EVMResListNFT>> callBack)
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
                CommonResponse<EVMResListNFT> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResListNFT>>(response);
                callBack(commonResponse);
            });
        });
    }

    public static void TransferNFT(string collection_address, int token_id, string to_wallet_address, Action approveAction, Action<CommonResponse<EVMResTransferNFT>> callBack)
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
                CommonResponse<EVMResTransferNFT> responseBody = JsonUtility.FromJson<CommonResponse<EVMResTransferNFT>>(response);
                callBack(responseBody);
            });
        });
    }

    //Asset/Mint
    public static void CreateMarketplace(int seller_fee_basis_points,string payment_token,EVMReqStorefrontObj storefrontObj,List<string> collections,string confirmation,Action<CommonResponse<EVMResCreateMarketplace>> callBack)
    {
        EVMReqCreateMarketplace requestParams = new EVMReqCreateMarketplace();
        requestParams.seller_fee_basis_points = seller_fee_basis_points;
        requestParams.payment_token = payment_token;
        requestParams.storefront = storefrontObj;
        requestParams.collections = collections;
        requestParams.confirmation = confirmation;
        var rawRequestBody = JsonUtility.ToJson(requestParams);

        MirrorWrapper.Instance.CreateMarketplace(rawRequestBody, (response) => {
            CommonResponse<EVMResCreateMarketplace> responseBody = JsonUtility.FromJson<CommonResponse<EVMResCreateMarketplace>>(response);
            callBack(responseBody);
        });
    }

    public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, Action approveFinished, Action<CommonResponse<EVMResMintCollection>> callBack)
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
                CommonResponse<EVMResMintCollection> responseBody = JsonUtility.FromJson<CommonResponse<EVMResMintCollection>>(response);
                callBack(responseBody);
            });
        });
    }

    public static void MintNFT(string collection_address, int token_id, string url, string to_wallet_address, int mint_amount, string confirmation, Action approveFinished, Action<CommonResponse<EVMResMintNFT>> callBack)
    {
        EVMMintNFTReq requestParams = new EVMMintNFTReq();
        requestParams.collection_address = collection_address;
        requestParams.token_id = token_id;
        requestParams.url = url; //Url of NFT json config file. If not specified, it will default to collection's base_url + token_id

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
                CommonResponse<EVMResMintNFT> responseBody = JsonUtility.FromJson<CommonResponse<EVMResMintNFT>>(response);
                callBack(responseBody);
            });
        });
    }

    //Asset/Search
    public static void QueryNFT(string mintAddress, string token_id, Action<CommonResponse<EVMResNFTInfo[]>> action)
    {
        MirrorWrapper.Instance.GetNFTDetailsOnEVM(mintAddress, token_id,(response) => {
            CommonResponse<EVMResNFTInfo[]> responseBody = JsonUtility.FromJson<CommonResponse<EVMResNFTInfo[]>>(response);
            action(responseBody);
        });
    }

    public static void SearchNFTsByOwner(string owner_address, int limit, string cursor, Action<CommonResponse<NFTDataResponse>> action)
    {
        EVMSearchNFTsByOwner requestBody = new EVMSearchNFTsByOwner();

        requestBody.owner_address = owner_address;
        requestBody.limit = limit;
        requestBody.cursor = cursor;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetNFTsOwnedByAddressOnEVM(rawRequestBody, (response) => {
            CommonResponse<NFTDataResponse> responseBody = JsonUtility.FromJson<CommonResponse<NFTDataResponse>>(response);
            action(responseBody);
        });
    }

    public static void SearchNFTsByMintAddress(List<EVMSearchNFTsByAddressesReqToken> tokens, Action<CommonResponse<EVMResNFTInfo[]>> callBack)
    {
        EVMSearchNFTsByAddressesReq requestBody = new EVMSearchNFTsByAddressesReq();

        requestBody.tokens = tokens;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.FetchNFTsByMintAddresses(rawRequestBody, (response) => {
            CommonResponse<EVMResNFTInfo[]> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResNFTInfo[]>>(response);
            callBack(commonResponse);
        });
    }


    //Wallet
    public static void GetTransactions(int limit, Action<CommonResponse<EVMResTransactions>> action)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("limit", "" + limit);

        MirrorWrapper.Instance.GetWalletTransactions(requestParams, (response) => {
            CommonResponse<EVMResTransactions> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResTransactions>>(response);
            action(commonResponse);
        });
    }

    public static void GetTransactionsByWallet(string wallet_address, int limit, Action<CommonResponse<EVMResGetTransactionsByWallet>> action)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("limit", "" + limit);

        MirrorWrapper.Instance.GetWalletTransactionsByWallet(wallet_address, requestParams, (response) => {
            CommonResponse<EVMResGetTransactionsByWallet> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResGetTransactionsByWallet>>(response);
            action(commonResponse);
        });
    }

    public static void GetTransactionsBySignature(string signature, Action<CommonResponse<EVMResTransactionData>> action)
    {
        MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, (response) => {
            CommonResponse<EVMResTransactionData> responseBody = JsonUtility.FromJson<CommonResponse<EVMResTransactionData>>(response);
            action(responseBody);
        });
    }

    public static void GetTokens(Action<CommonResponse<EVMResGetTokens>> action)
    {
        MirrorWrapper.Instance.GetWalletTokens((response) => {
            CommonResponse<EVMResGetTokens> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResGetTokens>>(response);
            action(commonResponse);
        });
    }

    public static void GetTokensByWalletByWallet(string wallet, Action<string> action)
    {
        action("API is not open.");
        return;
        MirrorWrapper.Instance.GetWalletTokensByWalletOnEVM(wallet, null, action);
    }

    public static void TransferETH(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-eth");

        TransferOnEVM(url,nonce,gasPrice,gasLimit,to,amount,approveFinished,callBack);
    }
    public static void TransferBNB(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-bnb");

        TransferOnEVM(url, nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
    }
    public static void TransferMatic(string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-matic");

        TransferOnEVM(url, nonce, gasPrice, gasLimit, to, amount, approveFinished, callBack);
    }

    private static void TransferOnEVM(string url, string nonce, string gasPrice, string gasLimit, string to, string amount, Action approveFinished, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        EVMTransferETHReq requestParams = new EVMTransferETHReq();
        requestParams.nonce = nonce;
        requestParams.gasPrice = gasPrice;
        requestParams.gasLimit = gasLimit;
        requestParams.to = to;
        requestParams.amount = amount;

        string approveValue = PrecisionUtil.GetApproveValue(PrecisionUtil.StrToFloat(amount));

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferETH, approveValue, "transfer evm", requestParams, () => {
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

    public static void TransferNativeOnEVM(string to_publickey, int amount, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        EVMReqTransferNativeToken requestParams = new EVMReqTransferNativeToken();
        requestParams.to_publickey = to_publickey;
        requestParams.amount = amount;

        var rawRequestBody = JsonUtility.ToJson(requestParams);
        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-native-token");

        MirrorWrapper.Instance.TransferOnEVM(url, rawRequestBody, (response) => {
            callBack(response);
        });
    }

    public static void TransferToken(string to, string amount, string contract, Action approveFinished, Action<string> callBack)
    {
        EVMTransferTokenReq requestParams = new EVMTransferTokenReq();
        //requestParams.nonce = nonce;
        //requestParams.gasPrice = gasPrice;
        //requestParams.gasLimit = gasLimit;
        requestParams.to = to;
        requestParams.amount = amount;
        requestParams.contract = contract;

        string approveValue = PrecisionUtil.GetApproveValue(PrecisionUtil.StrToFloat(amount));

        MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSPLToken, approveValue, "transfer token", requestParams, () => {

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

    public static void SignTransactionAndSend(string nonce, string gasPrice, string gasLimit, string to, string value, string data, Action<CommonResponse<EVMResSignTransactionAndSend>> action)
    {
        if (IsEmpty(nonce) || IsEmpty(gasPrice) || IsEmpty(gasLimit) || IsEmpty(to) || IsEmpty(value))
        {
            CommonResponse<EVMResSignTransactionAndSend> responseBody = new CommonResponse<EVMResSignTransactionAndSend>();
            responseBody.code = (long)MirrorResponseCode.LocalFailed;
            responseBody.message = "Please input parameters.";
            action(responseBody);
            return;
        }

        string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "send-tx");
        EVMReqSignTransactionAndSend req = new EVMReqSignTransactionAndSend();
        req.nonce = nonce;
        req.gasPrice = gasPrice;
        req.gasLimit = gasLimit;
        req.to = to;
        req.value = value;
        req.data = data;
        var rawRequestBody = JsonUtility.ToJson(req);

        MirrorWrapper.Instance.StartPostWithTimoutConfig(url, rawRequestBody,300,"Transaction was not minted in 300 seconds,please make sure your transaction was property sent.", (response) => {
            CommonResponse<EVMResSignTransactionAndSend> responseBody = JsonUtility.FromJson<CommonResponse<EVMResSignTransactionAndSend>>(response);
            action(responseBody);
        });
    }

    private static bool IsEmpty(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return true;
        }

        return false;
    }

    private static bool IsHexString(string str)
    {
        if (string.IsNullOrEmpty(str))
        {
            return false;
        }
        if (str.Length < 2)
        {
            return false;
        }
        if (str[0] != '0' || (str[1] != 'x' && str[1] != 'X'))
        {
            return false;
        }
        for (int i = 2; i < str.Length; i++)
        {
            if (!char.IsDigit(str[i]) && (str[i] < 'A' || str[i] > 'F') && (str[i] < 'a' || str[i] > 'f'))
            {
                return false;
            }
        }
        return true;
    }

    //Metadata/Collections
    public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<EVMResMetadataCollectionInfo>>> callback)
    {
        GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetCollectionInfo(rawRequestBody, (response) => {
            CommonResponse<List<EVMResMetadataCollectionInfo>> responseBody = JsonUtility.FromJson<CommonResponse<List<EVMResMetadataCollectionInfo>>>(response);
            callback(responseBody);
        });
    }

    public static void MetadataCollectionFilters(string collection, Action<CommonResponse<EVMResMetadataCollectionFilter>> callBack)
    {
        Dictionary<string, string> requestParams = new Dictionary<string, string>();

        requestParams.Add("collection", collection);

        MirrorWrapper.Instance.GetCollectionFilterInfo(requestParams, (response) => {
            CommonResponse<EVMResMetadataCollectionFilter> responseBody = JsonUtility.FromJson<CommonResponse<EVMResMetadataCollectionFilter>>(response);
            callBack(responseBody);
        });
    }

    public static void MetadataCollectionsSummary(List<string> collections, Action<CommonResponse<List<EVMResMetadataCollectionsSummary>>> action)
    {
        GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetCollectionsSummary(rawRequestBody, (response) => {
            CommonResponse<List<EVMResMetadataCollectionsSummary>> commonResponse = JsonUtility.FromJson<CommonResponse<List<EVMResMetadataCollectionsSummary>>>(response);
            action(commonResponse);
        });
    }

    //Metadata/NFT
    public static void MetadataNFTInfo(string contract, string token_id, Action<CommonResponse<EVMResMetadataNFTInfo>> callBack)
    {
        MirrorWrapper.Instance.GetNFTInfoOnEVM(contract, token_id, (response)=> {
            CommonResponse<EVMResMetadataNFTInfo> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResMetadataNFTInfo>>(response);
            callBack(commonResponse);
        });
    }

    public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<EVMGetNFTsByParamsReqFilter> filters, Action<CommonResponse<EVMResMetadataNFTs>> callback)
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
            CommonResponse<EVMResMetadataNFTs> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResMetadataNFTs>>(response);
            callback(commonResponse);
        });
    }

    public static void MetadataNFTEvents(string contract, int page, int pageSize,int token_id,string marketplace_address, Action<CommonResponse<EVMResMetadataNFTEvents>> callback)
    {
        EVMMetadataGetNFTEventsReq requestBody = new EVMMetadataGetNFTEventsReq();

        requestBody.contract = contract;

        requestBody.page = page;

        requestBody.page_size = pageSize;

        requestBody.token_id = token_id;

        requestBody.marketplace_address = marketplace_address;


        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.GetNFTEvents(rawRequestBody, (response) => {
            CommonResponse<EVMResMetadataNFTEvents> commonResponse = JsonUtility.FromJson<CommonResponse<EVMResMetadataNFTEvents>>(response);
            callback(commonResponse);
        });
    }

    public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<EVMResMetadataSearchNFTs>>> callback)
    {
        EVMMetadataSearchNFTReq req = new EVMMetadataSearchNFTReq();

        req.collections = collections;

        req.search = searchString;

        string rawRequestBody = JsonUtility.ToJson(req);

        MirrorWrapper.Instance.SearchNFTs(rawRequestBody, (response) => {
            CommonResponse<List<EVMResMetadataSearchNFTs>> commonResponse = JsonUtility.FromJson<CommonResponse<List<EVMResMetadataSearchNFTs>>>(response);
            callback(commonResponse);
        });
    }

    public static void MetadataRecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<EVMResMetadataRecommendNFTs>>> callback)
    {
        RecommendSearchNFTRequest requestBody = new RecommendSearchNFTRequest();

        requestBody.collections = collections;

        var rawRequestBody = JsonUtility.ToJson(requestBody);

        MirrorWrapper.Instance.RecommendSearchNFT(rawRequestBody, (response) => {
            CommonResponse<List<EVMResMetadataRecommendNFTs>> commonResponse = JsonUtility.FromJson<CommonResponse<List<EVMResMetadataRecommendNFTs>>>(response);
            callback(commonResponse);
        });
    }
}
