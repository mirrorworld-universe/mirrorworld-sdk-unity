using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APINames
{
    //Client
    public static string ClientStartLogin = "Start Login";
    public static string ClientLoginWithEmail = "Login With Email";
    public static string ClientIsLogged = "Is Logged";
    public static string ClientOpenWallet = "Open Wallet";
    public static string ClientOpenMarket = "Open Market";
    public static string ClientLogout = "Logout";
    public static string ClientGuestLogin = "Guest Login";
    //public static string ClientFetchUser = "Fetch User";
    public static string ClientQueryUser = "Query User";

    //Solana
    //Asset/Auction
    public static string SolAssetAuctionBuyNFT = "Buy NFT";
    public static string SolAssetAuctionCancelListing = "Cancel Listing";
    public static string SolAssetAuctionListNFT = "List NFT";
    public static string SolAssetAuctionTransferNFT = "Transfer NFT";
    public static string AssetCreateMarketplace = "Create Marketplace";
    //Asset/Confirmation
    public static string SolAssetConfirmationCheckStatusOfTransactions = "Check Transactions Status";
    public static string SolAssetConfirmationCheckStatusOfMinting = "Check Minting Status";
    //Asset/Mint
    public static string SolAssetMintCollection = "Mint Collection";
    public static string SolAssetMintNFT = "Mint NFT";
    public static string SolAssetMintUpdateNFTProperties = "Update NFT";
    //Asset/Search
    public static string SolAssetSearchQueryNFT = "Query NFT";
    public static string SolAssetSearchSearchNFTs = "Search NFTs";
    public static string SolAssetSearchSearchNFTsByOwner = "Search NFTs By Owners";
    //Wallet
    public static string SolWalletGetTransactions = "Get Transactions";
    public static string SolWalletGetTransactionsByWallet = "Get Transactions By Wallet";
    public static string SolWalletGetTransactionsBySignature = "Get Transactions By Signature";
    public static string SolWalletGetTokens = "Get Tokens";
    public static string SolWalletGetTokensByWallet = "Get Tokens By Wallet";
    public static string EVMSignTransactionAndSend = "Sign Transaction And Send";
    public static string SolWalletTransferSOL = "Transfer SOL";
    public static string SolWalletTransferETH = "Transfer ETH";
    public static string SolWalletTransferBNB = "Transfer BNB";
    public static string SolWalletTransferSUI = "Transfer SUI";
    public static string EVMWalletTransferNativeToken = "Transfer Native Token";
    public static string SolWalletTransferMatic = "Transfer Matic";
    public static string SolWalletTransferToken = "Transfer Token";
    //Metadata/Collections
    public static string SolMetadataGetCollectionsInfo = "Collections Info";
    public static string SolMetadataGetCollectionFiltersInfo = "Collections Filters";
    public static string SolMetadataGetCollectionsSummary = "Collections Summary";
    //Metadata/NFT
    public static string SolMetadataNFTInfo = "Get NFT Info";
    public static string SolMetadataNFTsInfo = "Get NFTs Info";
    public static string SolMetadataNFTEvents = "Get NFT Events";
    public static string SolMetadataNFTSearchNFT = "Search NFT";
    public static string SolMetadataNFTRecommendSearchNFT = "Search Recommend NFTs";


    //SUI
    //Wallet
    public static string SUIWalletGetTransactionByDigest = "Get Transaction By Digest";
    public static string SUIWalletTokens = "Get Tokens";
    public static string SUITransferSUI = "Transfer SUI";
    public static string SUITransferToken = "Transfer Token";
    //Asset/Mint
    public static string SUIAssetGetMintedCollections = "Get Minted Collections";
    public static string SUIAssetGetMintedNFTOnCollections = "Get Minted Collections";
    public static string SUIAssetMintCollection = "Mint Collection";
    public static string SUIAssetMintNFT = "Mint NFT";
    //Asset/Search
    public static string SUIAssetQueryNFT = "Query NFT";
    public static string SUIAssetSearchNFTsByOwner = "Search NFTs By Owner";
    public static string SUIAssetSearchNFT = "Search NFT";
}
