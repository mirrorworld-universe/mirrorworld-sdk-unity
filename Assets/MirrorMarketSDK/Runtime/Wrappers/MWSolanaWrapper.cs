using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorWorldResponses;

namespace MirrorworldSDK
{
    public class MWSolanaWrapper
    {
        //Asset/Auction
        public static void BuyNFT(string mint_address, double price, string auction_house, Action approveFinished, Action<CommonResponse<SolResBuyNFT>> callBack)
        {
            ApproveListNFT requestParams = new ApproveListNFT();
            requestParams.mint_address = mint_address;
            requestParams.price = price.ToString();
            requestParams.auction_house = auction_house;

            string approveValue = PrecisionUtil.GetApproveValue(price);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.BuyNFT, approveValue, "buy nft", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }

                BuyNftOnMarketplaceRequest requestBody = new BuyNftOnMarketplaceRequest();

                requestBody.mint_address = mint_address;

                requestBody.price = price;

                requestBody.auction_house = auction_house;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.BuyNFT(rawRequestBody, (response) => {

                    CommonResponse<SolResBuyNFT> responseBody = JsonUtility.FromJson<CommonResponse<SolResBuyNFT>>(response);

                    callBack(responseBody);
                });
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
                CancelNftListOnMarketplaceRequest requestBody = new CancelNftListOnMarketplaceRequest();

                requestBody.mint_address = mint_address;

                requestBody.price = price;

                requestBody.auction_house = auction_house;

                requestBody.confirmation = confirmation;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.CancelNFTListing(rawRequestBody, (response) => {

                    CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                    callBack(responseBody);
                });
            });
        }

        public static void ListNFT(string mint_address, double price, string auction_house, string confirmation, Action approveFinished, Action<CommonResponse<ListingResponse>> callBack)
        {
            ApproveListNFT requestParams = new ApproveListNFT();
            requestParams.mint_address = mint_address;
            requestParams.price = price.ToString();
            requestParams.confirmation = confirmation;
            requestParams.auction_house = auction_house;

            string approveValue = PrecisionUtil.GetApproveValue(price);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.ListNFT, approveValue, "list nft", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                ListNftOnMarketplaceRequest requestBody = new ListNftOnMarketplaceRequest();

                requestBody.mint_address = mint_address;

                requestBody.confirmation = confirmation;

                requestBody.price = price.ToString();

                requestBody.auction_house = auction_house;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.ListNFT(rawRequestBody, (response)=> {

                    CommonResponse<ListingResponse> responseBody = JsonUtility.FromJson<CommonResponse<ListingResponse>>(response);

                    callBack(responseBody);
                });
            });
        }

        public static void TransferNFT(string mint_address, string to_wallet_address, Action approveAction, Action<CommonResponse<SolResTransferNFT>> callBack)
        {
            ApproveTransferNFT requestParams = new ApproveTransferNFT();
            requestParams.mint_address = mint_address;
            requestParams.to_wallet_address = to_wallet_address;

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferNFT, "transfer nft", requestParams, () => {
                if (approveAction != null)
                {
                    approveAction();
                }
                TransferNftRequest requestBody = new TransferNftRequest();

                requestBody.mint_address = mint_address;

                requestBody.to_wallet_address = to_wallet_address;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.TransferNFT(rawRequestBody, (response)=> {

                    CommonResponse<SolResTransferNFT> responseBody = JsonUtility.FromJson<CommonResponse<SolResTransferNFT>>(response);

                    callBack(responseBody);
                });
            });
        }

        //Asset/Confirmation
        public static void CheckTransactionsStatus(List<string> signatures, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MirrorWrapper.Instance.GetStatusOfTransactions(signatures, (response)=> {
                CommonResponse<GetStatusOfTransactionsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetStatusOfTransactionsResponse>>(response);
                callBack(responseBody);
            });
        }

        public static void CheckMintingStatus(List<string> mintAddresses, Action<CommonResponse<GetStatusOfTransactionsResponse>> callBack)
        {
            MirrorWrapper.Instance.GetStatusOfMintings(mintAddresses, (response)=> {

                CommonResponse<GetStatusOfTransactionsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetStatusOfTransactionsResponse>>(response);

                callBack(responseBody);
            });
        }

        //Asset/Mint
        public static void CreateMarketplace(int seller_fee_basis_points, EVMReqStorefrontObj storefrontObj, List<string> collections, Action<CommonResponse<SolResCreateMarket>> callBack)
        {
            SolReqCreateMarketplace requestParams = new SolReqCreateMarketplace();
            requestParams.seller_fee_basis_points = seller_fee_basis_points;
            requestParams.storefront = storefrontObj;
            requestParams.collections = collections;
            var rawRequestBody = JsonUtility.ToJson(requestParams);

            MirrorWrapper.Instance.CreateMarketplace(rawRequestBody, (response) => {
                CommonResponse<SolResCreateMarket> responseBody = JsonUtility.FromJson<CommonResponse<SolResCreateMarket>>(response);
                callBack(responseBody);
            });
        }

        public static void MintCollection(string collectionName, string collectionSymbol, string NFTDetailJson, int seller_fee_basis_points, string confirmation, Action approveFinished, Action<CommonResponse<SolResMintResponse>> callBack)
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
                CreateCollectionRequest requestBody = new CreateCollectionRequest();

                requestBody.name = collectionName;
                requestBody.symbol = collectionSymbol;
                requestBody.url = NFTDetailJson;
                requestBody.seller_fee_basis_points = seller_fee_basis_points;
                if (confirmation != null) requestBody.comfirmation = confirmation;

                var rawRequestBody = JsonUtility.ToJson(requestBody);
                MirrorWrapper.Instance.CreateVerifiedCollection(rawRequestBody, (response)=> {
                    LogUtils.LogFlow("Create collection result:"+response);
                    CommonResponse<SolResMintResponse> responseBody = JsonUtility.FromJson<CommonResponse<SolResMintResponse>>(response);
                    callBack(responseBody);
                });
            });
        }

        public static void MintNFT(string parentCollection, string nFTName, string nFTSymbol, string nFTJsonUrl, string confirmation, string mint_id, string receiveWallet, double amountSol, Action approveFinished, Action<CommonResponse<SolResMintResponse>> callBack)
        {
            LogUtils.LogFlow("Mint request:amountSol:" + amountSol + ",receiveWallet:" + receiveWallet);
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
                CreateNftRequest requestBody = new CreateNftRequest();

                requestBody.name = nFTName;
                requestBody.symbol = nFTSymbol;
                requestBody.url = nFTJsonUrl;
                requestBody.collection_mint = parentCollection;
                requestBody.mint_id = mint_id;
                requestBody.confirmation = confirmation;
                if (amountSol != 0 && amountSol != null && receiveWallet != "" && receiveWallet != null)
                {
                    requestBody.payment = new MintPayment();
                    requestBody.payment.amount_sol = amountSol;
                    requestBody.payment.receiver_wallet = receiveWallet;
                }

                string rawRequestBody = JsonUtility.ToJson(requestBody);

                LogUtils.LogFlow("MintNFT rawRequestBody:"+ rawRequestBody);

                MirrorWrapper.Instance.MintNFT(rawRequestBody, (response)=> {
                    CommonResponse<SolResMintResponse> responseBody = JsonUtility.FromJson<CommonResponse<SolResMintResponse>>(response);
                    callBack(responseBody);
                });
            });
        }

        public static void UpdateNFT(string mintAddress, string NFTName, string symbol, string updateAuthority, string NFTJsonUrl, int seller_fee_basis_points, string confirmation, Action approveAction, Action<CommonResponse<SolResUpdateNFT>> callBack)
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
                UpdateNFTRequest requestBody = new UpdateNFTRequest();

                requestBody.mint_address = mintAddress;
                requestBody.name = NFTName;
                requestBody.symbol = symbol;
                requestBody.update_authority = updateAuthority;
                requestBody.url = NFTJsonUrl;
                requestBody.seller_fee_basis_points = seller_fee_basis_points;
                requestBody.confirmation = confirmation;
                string rawRequestBody = JsonUtility.ToJson(requestBody);
                MirrorWrapper.Instance.UpdateNFTProperties(rawRequestBody, (response)=> {
                    CommonResponse<SolResUpdateNFT> responseBody = JsonUtility.FromJson<CommonResponse<SolResUpdateNFT>>(response);
                    callBack(responseBody);
                });
            });
        }

        //Asset/Search
        public static void QueryNFT(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
        {
            MirrorWrapper.Instance.GetNFTDetails(mintAddress, (response)=> {
                CommonResponse<SingleNFTResponse> responseBody = JsonUtility.FromJson<CommonResponse<SingleNFTResponse>>(response);
                action(responseBody);
            });
        }

        public static void SearchNFTsByMintAddress(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
        {
            FetchMultipleNftsByMintAddressesRequest requestBody = new FetchMultipleNftsByMintAddressesRequest();

            requestBody.mint_addresses = mintAddresses;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.FetchNFTsByMintAddresses(rawRequestBody, (response)=> {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                action(responseBody);
            });
        }

        public static void SearchNFTsByOwner(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
        {
            FetchMultipleNftsByOwnersRequest requestBody = new FetchMultipleNftsByOwnersRequest();

            requestBody.owners = owners;

            requestBody.limit = limit;

            requestBody.offset = offset;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.GetNFTsOwnedByAddress(rawRequestBody, (response)=> {

                CommonResponse<MultipleNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<MultipleNFTsResponse>>(response);

                callBack(responseBody);
            });
        }

        //Wallet
        public static void GetTransactions(int number, string nextBefore, Action<CommonResponse<SolResGetTransactions>> action)
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();

            requestParams.Add("limit", "" + number);

            requestParams.Add("next_before", "" + nextBefore);

            MirrorWrapper.Instance.GetWalletTransactions(requestParams, (response)=> {

                CommonResponse<SolResGetTransactions> responseBody = JsonUtility.FromJson<CommonResponse<SolResGetTransactions>>(response);

                action(responseBody);
            });
        }

        public static void GetTransactionsByWallet(string wallet_address, int limit, string next_before, Action<CommonResponse<SolResGetTransactionByWallet>> action)
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();

            requestParams.Add("limit", "" + limit);

            requestParams.Add("next_before", "" + next_before);

            MirrorWrapper.Instance.GetWalletTransactionsByWallet(wallet_address, requestParams, (response) => {

                CommonResponse<SolResGetTransactionByWallet> responseBody = JsonUtility.FromJson<CommonResponse<SolResGetTransactionByWallet>>(response);

                action(responseBody);
            });
        }

        public static void GetTransactionsBySignature(string signature, Action<CommonResponse<SolResGetTransactionBySig>> action)
        {
            MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, (response)=> {

                CommonResponse<SolResGetTransactionBySig> responseBody = JsonUtility.FromJson<CommonResponse<SolResGetTransactionBySig>>(response);

                action(responseBody);
            });
        }

        public static void GetTokens(Action<CommonResponse<WalletTokenResponse>> action)
        {
            MirrorWrapper.Instance.GetWalletTokens((response)=> {

                CommonResponse<WalletTokenResponse> responseBody = JsonUtility.FromJson<CommonResponse<WalletTokenResponse>>(response);

                action(responseBody);
            });
        }

        public static void GetTokensByWalletByWallet(string wallet, int limit, string next_before, Action<CommonResponse<WalletTokenResponse>> action)
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();
            requestParams.Add("limit",limit+"");
            requestParams.Add("next_before", next_before);
            MirrorWrapper.Instance.GetWalletTokensByWallet(wallet,requestParams, (response) => {
                CommonResponse<WalletTokenResponse> responseBody = JsonUtility.FromJson<CommonResponse<WalletTokenResponse>>(response);
                action(responseBody);
            });
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
                TransferSolRequest requestBody = new TransferSolRequest();

                requestBody.amount = amount;

                requestBody.to_publickey = to_publickey;

                requestBody.confirmation = confirmation;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.TransferSol(rawRequestBody, (response)=> {

                    CommonResponse<TransferSolResponse> responseBody = JsonUtility.FromJson<CommonResponse<TransferSolResponse>>(response);

                    callBack(responseBody);
                });
            });
        }

        public static void TransferToken(string token_mint, int decimals, ulong amount, string to_publickey, Action approveFinished, Action<CommonResponse<TransferTokenResponse>> callBack)
        {
            ApproveTransferSPLToken requestParams = new ApproveTransferSPLToken();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;
            requestParams.token_mint = token_mint;
            requestParams.decimals = decimals;

            string approveValue = PrecisionUtil.GetApproveValue(amount,decimals);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSPLToken, approveValue, "transfer spl token", requestParams, () => {

                if (approveFinished != null)
                {
                    approveFinished();
                }

                TransferTokenRequest requestBody = new TransferTokenRequest();

                requestBody.amount = amount;

                requestBody.to_publickey = to_publickey;

                requestBody.decimals = decimals;

                requestBody.token_mint = token_mint;

                var rawRequestBody = JsonUtility.ToJson(requestBody);

                MirrorWrapper.Instance.TransferSPLToken(rawRequestBody, (response)=> {

                    CommonResponse<TransferTokenResponse> responseBody = JsonUtility.FromJson<CommonResponse<TransferTokenResponse>>(response);

                    callBack(responseBody);
                });
            });
        }

        //Metadata/Collections
        public static void MetadataCollectionsInfo(List<string> collections, Action<CommonResponse<List<GetCollectionInfoResponse>>> callback)
        {
            GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

            requestBody.collections = collections;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.GetCollectionInfo(rawRequestBody, (response)=> {

                CommonResponse<List<GetCollectionInfoResponse>> responseBody = JsonUtility.FromJson<CommonResponse<List<GetCollectionInfoResponse>>>(response);

                callback(responseBody);
            });
        }

        public static void MetadataCollectionFilters(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
        {
            Dictionary<string, string> requestParams = new Dictionary<string, string>();

            requestParams.Add("collection", collection);

            MirrorWrapper.Instance.GetCollectionFilterInfo(requestParams, (response)=> {

                CommonResponse<GetCollectionFilterInfoResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetCollectionFilterInfoResponse>>(response);

                callBack(responseBody);
            });
        }

        public static void MetadataCollectionsSummary(List<string> collections, Action<CommonResponse<List<SolResMetadataGetCollectionSummary>>> action)
        {
            GetCollectionInfoRequest requestBody = new GetCollectionInfoRequest();

            requestBody.collections = collections;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.GetCollectionsSummary(rawRequestBody, (response) => {
                CommonResponse<List<SolResMetadataGetCollectionSummary>> responseBody = JsonUtility.FromJson<CommonResponse<List<SolResMetadataGetCollectionSummary>>>(response);
                action(responseBody);
            });
        }

        //Metadata/NFT
        public static void MetadataNFTInfo(string mintAddress, Action<string> callBack)
        {
            MirrorWrapper.Instance.GetNFTInfo(mintAddress, callBack);
        }

        public static void MetadataNFTsByUnabridgedParams(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
        {
            GetNFTsRequest requestBody = new GetNFTsRequest();

            requestBody.collection = collection;

            requestBody.page = page;

            requestBody.page_size = pageSize;

            requestBody.order = new GetNFTsRequestOrder();

            requestBody.order.order_by = orderByString;

            requestBody.order.desc = desc;

            requestBody.filter = filters;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.GetNFTsByUnabridgedParams(rawRequestBody, (response)=> {

                CommonResponse<GetNFTsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetNFTsResponse>>(response);

                callback(responseBody);
            });
        }

        public static void MetadataNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
        {
            GetNFTEventsRequest requestBody = new GetNFTEventsRequest();

            requestBody.mint_address = mintAddress;

            requestBody.page = page;

            requestBody.page_size = pageSize;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.GetNFTEvents(rawRequestBody, (response)=> {

                CommonResponse<GetNFTEventsResponse> responseBody = JsonUtility.FromJson<CommonResponse<GetNFTEventsResponse>>(response);

                callback(responseBody);
            });
        }

        public static void MetadataSearchNFTs(List<string> collections, string searchString, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            SolMetadataSearchNFTsReq req = new SolMetadataSearchNFTsReq();

            req.collections = collections;

            req.search = searchString;

            string rawRequestBody = JsonUtility.ToJson(req);

            MirrorWrapper.Instance.SearchNFTs(rawRequestBody, (response)=> {
                CommonResponse<List<MirrorMarketNFTObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<MirrorMarketNFTObj>>>(response);

                callback(responseBody);
            });
        }

        public static void MetadataRecommendSearchNFTs(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
        {
            RecommendSearchNFTRequest requestBody = new RecommendSearchNFTRequest();

            requestBody.collections = collections;

            var rawRequestBody = JsonUtility.ToJson(requestBody);

            MirrorWrapper.Instance.RecommendSearchNFT(rawRequestBody, (response)=> {

                CommonResponse<List<MirrorMarketNFTObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<MirrorMarketNFTObj>>>(response);

                callback(responseBody);
            });
        }
    }
}
