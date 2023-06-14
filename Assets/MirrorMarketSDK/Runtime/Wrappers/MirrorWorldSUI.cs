using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;
using MirrorWorldResponses;
using MirrorworldSDK;

namespace MirrorWorld
{
    public class MirrorWorldSUI
    {
        public SUIWallet Wallet = new SUIWallet();

        public SUIAsset Asset = new SUIAsset();
    }

    public class SUIAsset
    {
        public void GetMintedCollections(Action<CommonResponse<List<SUIResGetMintedCollectionsObj>>> action)
        {
            MWSUIWrapper.GetMintedCollections(action);
        }

        public void GetMintedNFTOnCollection(string collection_address, Action<CommonResponse<List<SUIResGetMintedNFTOnCollectionObj>>> action)
        {
            MWSUIWrapper.GetMintedNFTOnCollection(collection_address,action);
        }

        public void MintCollection(string name, string symbol, string description, List<string> creators, Action<CommonResponse<SUIResMintCollection>> action)
        {
            MWSUIWrapper.MintCollection(name,symbol, description, creators, action);
        }

        public void MintNFT(string collection_address, string name, string description, string image_url, List<SUIReqMintNFTAttribute> attributes, string to_wallet_address, Action<CommonResponse<SUIResMintNFT>> action)
        {
            MWSUIWrapper.MintNFT(collection_address,name,description,image_url,attributes,to_wallet_address,action);
        }

        public void QueryNFT(string nft_object_id, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            MWSUIWrapper.QueryNFT(nft_object_id, action);
        }

        public void SearchNFTsByOwner(string owner_address, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            MWSUIWrapper.SearchNFTsByOwner(owner_address, action);
        }

        public void SearchNFTs(List<string> nft_object_ids, Action<CommonResponse<List<SUIResQueryNFT>>> action)
        {
            MWSUIWrapper.SearchNFTs(nft_object_ids, action);
        }
    }

    public class SUIWallet
    {
        public void GetTransactionByDigest(string digest, Action<CommonResponse<SUIResGetTransactionByDigest>> action)
        {
            MWSUIWrapper.GetTransactionsByDigest(digest, action);
        }

        public void GetTokens(Action<CommonResponse<SUIResGetTokens>> action)
        {
            MWSUIWrapper.GetTokens(action);
        }

        public void TransferSUI(string to_publickey, int amount, Action approveFinished, Action<CommonResponse<SUIResTransferSUI>> callBack)
        {
            MWSUIWrapper.TransferSUI(to_publickey, amount, approveFinished, callBack);
        }

        public void TransferToken(string to_publickey, int amount, string token, Action approveFinished, Action<CommonResponse<SUIResTransferToken>> callBack)
        {
            MWSUIWrapper.TransferToken(to_publickey, amount, token, approveFinished, callBack);
        }
    }
}
