using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorWorldResponses;

namespace MirrorworldSDK
{
    public class MWSUIWrapper
    {
        //Asset
        //Mint
        public static void GetMintedCollections(Action<CommonResponse<List<SUIResGetMintedCollectionsObj>>> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.AssetMint) + "get-collections";
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndGet(url, null, (response) =>
            {
                CommonResponse<List<SUIResGetMintedCollectionsObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<SUIResGetMintedCollectionsObj>>>(response);
                action(responseBody);
            }));
        }

        public static void GetMintedNFTOnCollection(string collection_address,Action<CommonResponse<List<SUIResGetMintedNFTOnCollectionObj>>> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.AssetMint) + "get-collection-nfts/" + collection_address;
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndGet(url, null, (response) =>
            {
                CommonResponse<List<SUIResGetMintedNFTOnCollectionObj>> responseBody = JsonUtility.FromJson<CommonResponse<List<SUIResGetMintedNFTOnCollectionObj>>>(response);
                action(responseBody);
            }));
        }

        public static void MintCollection(string name,string symbol, string description,List<string> creators,Action<CommonResponse<SUIResMintCollection>> action)
        {
            SUIReqMintCollection requestParams = new SUIReqMintCollection();
            requestParams.creators = creators;
            requestParams.name = name;
            requestParams.symbol = symbol;
            requestParams.description = description;

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMint, "collection");
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            var rawRequestBody = JsonUtility.ToJson(requestParams);
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                CommonResponse<SUIResMintCollection> responseBody = JsonUtility.FromJson<CommonResponse<SUIResMintCollection>>(response);
                action(responseBody);
            }));
        }

        public static void MintNFT(string collection_address,string name,string description,string image_url,List<SUIReqMintNFTAttribute> attributes,string to_wallet_address,Action<CommonResponse<SUIResMintNFT>> action)
        {
            SUIReqMintNFT requestParams = new SUIReqMintNFT();
            requestParams.collection_address = collection_address;
            requestParams.name = name;
            requestParams.description = description;
            requestParams.image_url = image_url;
            requestParams.to_wallet_address = to_wallet_address;
            requestParams.attributes = attributes;

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMint, "nft");
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            var rawRequestBody = JsonUtility.ToJson(requestParams);
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                CommonResponse<SUIResMintNFT> responseBody = JsonUtility.FromJson<CommonResponse<SUIResMintNFT>>(response);
                action(responseBody);
            }));
        }

        //Asset/Search
        public static void QueryNFT(string nft_object_id, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.AssetNFT) + nft_object_id;
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndGet(url, null, (response) =>
            {
                CommonResponse<List<SUIResQueryNFT>> responseBody = JsonUtility.FromJson<CommonResponse<List<SUIResQueryNFT>>>(response);
                action(responseBody);
            }));
        }

        public static void SearchNFTsByOwner(string owner_address, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            SUIReqSearchNFTsByOwner requestParams = new SUIReqSearchNFTsByOwner();
            requestParams.owner_address = owner_address;

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetNFT, "owner");
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            var rawRequestBody = JsonUtility.ToJson(requestParams);
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                CommonResponse<List<SUIResQueryNFT>> responseBody = JsonUtility.FromJson<CommonResponse<List<SUIResQueryNFT>>>(response);
                action(responseBody);
            }));
        }

        public static void SearchNFTs(List<string> nft_object_ids, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            SUIReqSearchNFTs requestParams = new SUIReqSearchNFTs();
            requestParams.nft_object_ids = nft_object_ids;

            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetNFT, "mints");
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            var rawRequestBody = JsonUtility.ToJson(requestParams);
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                CommonResponse<List<SUIResQueryNFT>> responseBody = JsonUtility.FromJson<CommonResponse<List<SUIResQueryNFT>>>(response);
                action(responseBody);
            }));
        }

        //Wallet
        public static void GetTransactionsByDigest(string digest, Action<CommonResponse<SUIResGetTransactionByDigest>> action)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.Wallet) + "transactions/" + digest;
            MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
            monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndGet(url, null, (response) =>
            {
                CommonResponse<SUIResGetTransactionByDigest> responseBody = JsonUtility.FromJson<CommonResponse<SUIResGetTransactionByDigest>>(response);
                action(responseBody);
            }));
        }

        public static void GetTokens(Action<CommonResponse<SUIResGetTokens>> action)
        {
            MirrorWrapper.Instance.GetWalletTokens((response)=> {

                CommonResponse<SUIResGetTokens> responseBody = JsonUtility.FromJson<CommonResponse<SUIResGetTokens>>(response);

                action(responseBody);
            });
        }

        public static void TransferSUI(string to_publickey, int amount, Action approveFinished, Action<CommonResponse<SUIResTransferSUI>> callBack)
        {
            SUIReqTransferSUI requestParams = new SUIReqTransferSUI();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;

            if (MWConfig.isSUIBeta)
            {
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-sui");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferSUI> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferSUI>>(response);
                    callBack(responseBody);
                }));
                return;
            }

            string approveValue = PrecisionUtil.GetApproveValue(amount);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferSUI, approveValue, "transfer sui", requestParams, () => {
                if (approveFinished != null)
                {
                    approveFinished();
                }
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-sui");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferSUI> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferSUI>>(response);
                    callBack(responseBody);
                }));
            });
        }

        public static void TransferToken(string to_publickey, int amount, string token,Action approveFinished, Action<CommonResponse<SUIResTransferToken>> callBack)
        {
            if (token == null || !token.Contains("::"))
            {
                CommonResponse<SUIResTransferToken> commonResponse = new CommonResponse<SUIResTransferToken>();
                commonResponse.code = (long)MirrorResponseCode.LocalFailed;
                commonResponse.error = "Please check your token address,normally it contains ::.";
                commonResponse.data = new SUIResTransferToken();
                commonResponse.data.tx_signature = "";

                callBack(commonResponse);
                return;
            }
            SUIReqTransferToken requestParams = new SUIReqTransferToken();
            requestParams.to_publickey = to_publickey;
            requestParams.amount = amount;
            requestParams.token = token;

            if (MWConfig.isSUIBeta)
            {
                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-token");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) =>
                {
                    CommonResponse<SUIResTransferToken> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferToken>>(response);
                    callBack(responseBody);
                }));
                return;
            }

            string approveValue = PrecisionUtil.GetApproveValue(amount);

            MirrorWrapper.Instance.StartSecuirtyApprove(MirrorSafeOptType.TransferToken, approveValue, "transfer token", requestParams, () => {

                if (approveFinished != null)
                {
                    approveFinished();
                }

                var rawRequestBody = JsonUtility.ToJson(requestParams);

                string url = UrlUtils.GetMirrorPostUrl(MirrorService.Wallet, "transfer-token");
                MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                monoBehaviour.StartCoroutine(MirrorWrapper.Instance.CheckAndPost(url, rawRequestBody, (response) => {
                    CommonResponse<SUIResTransferToken> responseBody = JsonUtility.FromJson<CommonResponse<SUIResTransferToken>>(response);
                    callBack(responseBody);
                }));
            });
        }
    }
}
