using System;
using System.Collections.Generic;
using MirrorworldSDK.Models;
using MirrorWorldResponses;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        //fetch
        private readonly string urlFetchSingleNFT = "nft";
        private readonly string urlGetActivityOfSingleNFT = "solana/nft/activity/";
        private readonly string urlFetchNFTsByMintAddresses = "mints";
        private readonly string urlFetchMultiNFTsDataByOwnerAddresses = "owners";
        private readonly string urlFetchMultiNFTsDataByCreatorAddress = "solana/nft/creators";
        private readonly string urlFetchMultiNFTsDataByUpdateAuthorityAddress = "solana/nft/update-authorities";
        //create
        private readonly string urlCreateCollection = "collection";
        private readonly string urlMintLowerLevelCollection = "solana/mint/sub-collection";
        private readonly string urlMintNFTCollection = "nft";
        private readonly string urlUpdateNFT = "update";
        //list
        private readonly string urlAssetAuctionList = "list";
        private readonly string urlUpdateListingOfNFTOnTheMarketplace = "solana/marketplace/update";
        private readonly string urlAssetAuctionCancel = "cancel";
        //operate nft
        private readonly string urlAssetAuctionTransfer = "transfer";
        private readonly string urlAssetAuctionBuy = "buy";


        public void GetNFTDetails(string mintAddress, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.AssetNFT) + mintAddress;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                callBack(response);
            }));
        }
        public void GetNFTDetailsOnEVM(string mintAddress,string token_id, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorGetUrl(MirrorService.AssetNFT) + mintAddress + "/" + token_id;

            monoBehaviour.StartCoroutine(CheckAndGet(url, null, (response) => {

                callBack(response);
            }));
        }

        public void CreateMarketplace(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMarketplace, "create");

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                callBack(response);

            }));
        }

        public void CreateVerifiedCollection(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMint, urlCreateCollection);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody,(response)=> {

                callBack(response);

            }));
        }

        public void MintNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMint, urlMintNFTCollection);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("MintNft result:" + response);

                callBack(response);
            }));
        }

        public void FetchNFTsByMintAddresses(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetNFT, urlFetchNFTsByMintAddresses);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, callBack));
        }

        public void GetNFTsOwnedByAddress(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetNFT, urlFetchMultiNFTsDataByOwnerAddresses);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                callBack(response);
            }));
        }

        public void GetNFTsOwnedByAddressOnEVM(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetNFT, "owner");

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                callBack(response);
            }));
        }

        public void ListNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetAuction, urlAssetAuctionList);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("ListNFT result:" + response);

                callBack(response);

            }));
        }

        public void CancelNFTListing(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetAuction, urlAssetAuctionCancel);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("CancelNFTListing result:" + response);

                callBack(response);

            }));
        }

        public void TransferNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetAuction, urlAssetAuctionTransfer);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("TransferNFT result:" + response);

                //CommonResponse<EVMResTransferNFT> responseBody = JsonUtility.FromJson<CommonResponse<EVMResTransferNFT>>(response);
                callBack(response);

            }));
        }

        public void BuyNFT(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetAuction, urlAssetAuctionBuy);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {
                LogFlow("Buy NFT response:"+ response);

                callBack(response);
            }));
        }

        public void UpdateNFTProperties(string rawRequestBody, Action<string> callBack)
        {
            string url = UrlUtils.GetMirrorPostUrl(MirrorService.AssetMint, urlUpdateNFT);

            monoBehaviour.StartCoroutine(CheckAndPost(url, rawRequestBody, (response) => {

                LogFlow("UpdateNFT result:" + response);

                callBack(response);
            }));
        }
    }
}

