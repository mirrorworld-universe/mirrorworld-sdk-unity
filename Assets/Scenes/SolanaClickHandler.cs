using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorWorld;
using MirrorworldSDK;
using MirrorworldSDK.UI;
using System.Collections.Generic;
using MirrorWorldResponses;
using MirrorworldSDK.Models;
using TMPro;

public class SolanaClickHandler
{
    public GameObject apiInfo;
    public GameObject apiList;
    private TextMeshProUGUI apiNameText,btnText, contentText;
    private ParamCell cell1, cell2, cell3, cell4, cell5, cell6;
    private string v1, v2, v3, v4, v5, v6;
    private Action clickAction;

    public void Init(GameObject apiInfo,GameObject apiList,TextMeshProUGUI apiNameText,TextMeshProUGUI btnText,TextMeshProUGUI contentText,
ParamCell cell1,ParamCell cell2,ParamCell cell3,ParamCell cell4,ParamCell cell5,ParamCell cell6)
    {
        this.apiInfo = apiInfo;
        this.apiList = apiList;
        this.apiNameText = apiNameText;
        this.btnText = btnText;
        this.contentText = contentText;
        this.cell1 = cell1;
        this.cell2 = cell2;
        this.cell3 = cell3;
        this.cell4 = cell4;
        this.cell5 = cell5;
        this.cell6 = cell6;
    }

    private void ClearLog()
    {
        contentText.text = "";
    }

    public void OnConfirmClicked()
    {
        UpdateValues();
        clickAction();
    }
    private void UpdateValues()
    {
        v1 = cell1.GetInput();
        v1 = SubLastChar(v1);
        v2 = cell2.GetInput();
        v2 = SubLastChar(v2);
        v3 = cell3.GetInput();
        v3 = SubLastChar(v3);
        v4 = cell4.GetInput();
        v4 = SubLastChar(v4);
        v5 = cell5.GetInput();
        v5 = SubLastChar(v5);
        v6 = cell6.GetInput();
        v6 = SubLastChar(v6);
    }
    private string SubLastChar(string originStr)
    {
        if (originStr.Length == 0) return originStr;
        originStr = originStr.Substring(0, originStr.Length - 1);
        return originStr;
    }
    private void SetInfoPanel(string apiName, string hint1, string hint2, string hint3, string hint4, string hint5, string hint6, string btnText, string content, Action action)
    {
        apiNameText.text = apiName;
        if (hint1 == null)
        {
            cell1.Hide();
        }
        else
        {
            cell1.Show(hint1, hint1);
        }
        if (hint2 == null)
        {
            cell2.Hide();
        }
        else
        {
            cell2.Show(hint2, hint2);
        }
        if (hint3 == null)
        {
            cell3.Hide();
        }
        else
        {
            cell3.Show(hint3, hint3);
        }
        if (hint4 == null)
        {
            cell4.Hide();
        }
        else
        {
            cell4.Show(hint4, hint4);
        }
        if (hint5 == null)
        {
            cell5.Hide();
        }
        else
        {
            cell5.Show(hint5, hint5);
        }
        if (hint6 == null)
        {
            cell6.Hide();
        }
        else
        {
            cell6.Show(hint6, hint6);
        }
        this.btnText.text = btnText;
        PrintLog(content);
        clickAction = action;
    }

    private void PrintLog(string content)
    {
        contentText.text += "MirrorTest:" + content + "\n";
        GUIUtility.systemCopyBuffer = content;
    }
    private UniversalDialog ShowUniversalNotice(string title, string content, string yesText, string noText, Action yesAction, Action noAction)
    {
        MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
        GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("UniversalDialog", GameObject.Find("Canvas").transform);
        UniversalDialog dialog = dialogCanvas.GetComponent<UniversalDialog>();
        dialog.Init(title, content, yesText, noText, yesAction, noAction);
        return dialog;
    }
    public void handleDemoClick(string btnName)
    {
        contentText.text = "MirrorTest:" + "wait to call..." + "\n";

        bool notOpenDetail = false;

        Action approveFinished = () => {
            MWSDK.DebugLog("Approve finished!");
        };

        if (btnName == "BtnClearCache")
        {
            notOpenDetail = true;
            PlayerPrefs.DeleteAll();
            MirrorWrapper.Instance.ClearUnitySDKCache();
            MWSDK.DebugLog("Cleared local storage.");
        }
        else if (btnName == APINames.ClientStartLogin)
        {
            notOpenDetail = true;
            MWSDK.StartLogin((loginResponse) => {
                MWSDK.DebugLog("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MWSDK.GuestLogin((loginResponse) => {
                MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {

                    MWSDK.DebugLog("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

                    UniversalDialog dialog = null;
                    dialog = ShowUniversalNotice("Guest Login", "Guest login success! But a guest account can not visit sensitive APIs.", "OK", null,
                    () => {
                        LogUtils.LogFlow("Click OK");
                        dialog.DestroyDialog();
                    },
                    null);
                });
            });
        }
        else if (btnName == APINames.ClientLoginWithEmail)
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, null, null, "Login", "Login with a registed email.", () => {
                MWSDK.LoginWithEmail(v1, v2, (res) => {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientQueryUser)
        {
            SetInfoPanel("QueryUser", "email", null, null, null, null, null, "Query", "Query user info.", () => {
                MWSDK.QueryUser(v1, (res) => {
                    CommonResponse<UserResponse> resObj = res;
                    string userName = resObj.data.username;
                    string solAddress = resObj.data.wallet.sol_address;
                    //todo...

                    PrintLog("Query result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () => {
                MWSDK.Solana.Wallet.GetTokens((res) => {
                    CommonResponse<WalletTokenResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    WalletTokenResponse token = resObj.data;
                    //todo...

                    PrintLog("Get tokens result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokensByWallet)
        {
            SetInfoPanel("GetWalletTokensByWallet", "wallet address", "limit", "next_before", null, null, null, "Get", "Get your tokens", () => {
                int limit = PrecisionUtil.StrToInt(v2);
                MWSDK.Solana.Wallet.GetTokensByWallet(v1, limit, v3, (res) => {
                    CommonResponse<WalletTokenResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    WalletTokenResponse tokens = resObj.data;
                    //todo...

                    PrintLog("Get tokens result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MWSDK.Logout(() => {
                UniversalDialog dialog = null;
                Action yesAction = () => {
                    dialog.DestroyDialog();
                };
                dialog = ShowUniversalNotice("Logout", "Your logged account has logged out.", "Got it", "", yesAction, null);
            });
        }
        else if (btnName == APINames.SolAssetSearchQueryNFT)
        {
            SetInfoPanel("QueryNFT", "mint address", null, null, null, null, null, "GetNFTDetails", "GetNFTDetails", () => {
                MWSDK.Solana.Asset.QueryNFT(v1, (res) => {
                    CommonResponse<SingleNFTResponse> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    string name = res.data.nft.name;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTsByOwner)
        {
            SetInfoPanel("GetNFTOwnedByAddress", "owner address", "limit", "offset", null, null, null, "Get", "Get NFTs wwned by address", () => {
                List<string> owners = new List<string>();
                owners.Add(v1);
                long limit = long.Parse(v2);
                long offset = long.Parse(v3);

                MWSDK.Solana.Asset.SearchNFTsByOwner(owners, limit, offset, (res) => {
                    CommonResponse<MultipleNFTsResponse> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    MultipleNFTsResponse multipleNFTs = res.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTs)
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", null, null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                List<string> list = new List<string>();
                list.Add(v1);
                MWSDK.Solana.Asset.SearchNFTsByMintAddress(list, (res) => {
                    CommonResponse<MultipleNFTsResponse> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    MultipleNFTsResponse multipleNFTs = res.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MWSDK.IsLogged((res) => {
                    bool isLogged = res;

                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == APINames.SolAssetMintCollection)
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", "seller fee basis points", null, null, "CreateVerifiedCollection", "CreateVerifiedCollection", () => {
                int seller_fee_basis_points = (int)PrecisionUtil.StrToDouble(v4);
                string name = v1;
                string symbol = v2;
                string jsonUrl = v3;
                string confirmation = Confirmation.Default;
                MWSDK.Solana.Asset.MintCollection(v1, v2, v3, seller_fee_basis_points, confirmation, approveFinished, (res) => {
                    CommonResponse<SolResMintResponse> resObj = res;
                    string collectionAddress = res.data.mint_address;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            }
                );
        }
        else if (btnName == APINames.AssetCreateMarketplace)
        {
            SetInfoPanel("Create Marketplace", "seller_fee_basis_points", "collection 1", "name", "sub domain", "description", null, "CreateMarket", "Create", () =>
            {
                int seller_fee_basis_points = PrecisionUtil.StrToInt(v1);
                EVMReqStorefrontObj storefrontObj = new EVMReqStorefrontObj();
                storefrontObj.description = v5;
                storefrontObj.name = v3;
                storefrontObj.subdomain = v4;
                List<string> collections = new List<string>();
                collections.Add(v2);
                MWSDK.Solana.Asset.CreateMarketplace(seller_fee_basis_points, storefrontObj, collections, (res) =>
                {
                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "parent collection", "name", "mint_id", "url", "receive wallet", "amount", "MintNFT", "MintNFT", () => {
                double amount = PrecisionUtil.StrToDouble(v6);
                MWSDK.Solana.Asset.MintNFT(v1, v2, "testsymbol", v4, Confirmation.Default, v3, v5, amount, approveFinished, (res) => {
                    CommonResponse<SolResMintResponse> resObj = res;
                    long code = res.code;
                    string nftMintAddress = res.data.mint_address;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetMintUpdateNFTProperties)
        {
            SetInfoPanel("UpdateNFTProperties", "mint address", "name", "updateAuthority", "json url", null, null, "MintNFT", "MintNFT", () => {
                MWSDK.Solana.Asset.UpdateNFT(v1, v2, "newsymbol", v3, v4, 200, Confirmation.Default, approveFinished, (res) => {
                    CommonResponse<SolResUpdateNFT> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    string nftName = res.data.nft.name;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionListNFT)
        {
            SetInfoPanel("ListNFT", "mint address", "price", "auction_house", null, null, null, "ListNFT", "ListNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MWSDK.Solana.Asset.ListNFT(v1, price, v3, Confirmation.Default, approveFinished, (res) => {

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionCancelListing)
        {
            SetInfoPanel("CancelNFTListing", "mint address", "price", "auction_house", null, null, null, "CancelNFTListing", "CancelNFTListing", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MWSDK.Solana.Asset.CancelListing(v1, price, v3, Confirmation.Default, approveFinished, (res) =>
                {
                    CommonResponse<ListingResponse> resObj = res;
                    long code = res.code;
                    ListingResponse lr = res.data;
                    string signature = res.data.signature;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionBuyNFT)
        {
            SetInfoPanel("Buy NFT", "mint address", "Price", "auction house", null, null, null, "BuyNFT", "BuyNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MWSDK.DebugLog("price:" + price);
                MWSDK.Solana.Asset.BuyNFT(v1, price, v3, approveFinished, (res) => {
                    CommonResponse<SolResBuyNFT> resObj = res;
                    long http_status_code = res.http_status_code;
                    SolResBuyNFTNft nft = res.data.nft;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionTransferNFT)
        {
            SetInfoPanel("Transfer NFT", "mint address", "to wallet", null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                MWSDK.Solana.Asset.TransferNFT(v1, v2, approveFinished, (res) => {
                    CommonResponse<SolResTransferNFT> resObj = res;
                    long code = res.code;
                    string nftAddress = res.data.mint_address;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactions)
        {
            SetInfoPanel("GetWalletTransactions", "limit", "next_before", null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                int limit = PrecisionUtil.StrToInt(v1);
                MWSDK.Solana.Wallet.GetTransactions(limit, v2, (res) => {
                    CommonResponse<SolResGetTransactions> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    SolResGetTransactions transactions = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsByWallet)
        {
            SetInfoPanel("GetWalletTransactionsByWallet", "wallet address", "limit", "next_before", null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                int limit = PrecisionUtil.StrToInt(v2);
                MWSDK.Solana.Wallet.GetTransactionsByWallet(v1, limit, v3, (res) => {
                    CommonResponse<SolResGetTransactionByWallet> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    SolResGetTransactionByWallet transactions = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsBySignature)
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () => {
                MWSDK.Solana.Wallet.GetTransactionsBySignature(v1, (res) =>
                {
                    CommonResponse<SolResGetTransactionBySig> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    SolResGetTransactionBySig transaction = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetConfirmationCheckStatusOfTransactions)
        {
            SetInfoPanel("GetStatusOfTransactions", "signature 1", "signature 2", null, null, null, null, "GetStatusOfTransactions", "GetStatusOfTransactions", () => {
                List<string> signatures = new List<string>();
                if (v1 != "") signatures.Add(v1);
                if (v2 != "") signatures.Add(v2);

                MWSDK.Solana.Asset.CheckTransactionsStatus(signatures, (res) =>
                {
                    CommonResponse<GetStatusOfTransactionsResponse> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    GetStatusOfTransactionsResponse transactionStatus = res.data;
                    //todo...

                    PrintLog("GetStatusOfTransactions result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolAssetConfirmationCheckStatusOfMinting)
        {
            SetInfoPanel("GetStatusOfMintings", "mint address 1", "mint address 2", null, null, null, null, "GetStatusOfMintings", "GetStatusOfMintings", () => {
                List<string> mintAddresses = new List<string>();
                if (v1 != "") mintAddresses.Add(v1);
                if (v2 != "") mintAddresses.Add(v2);
                MWSDK.Solana.Asset.CheckMintingStatus(mintAddresses, (res) =>
                {
                    CommonResponse<GetStatusOfTransactionsResponse> resObj = res;
                    long code = res.code;
                    string message = res.message;
                    GetStatusOfTransactionsResponse transactionStatus = res.data;
                    //todo...

                    PrintLog("GetStatusOfMintings result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferSOL)
        {
            SetInfoPanel("TransferSol", "amount", "public key", null, null, null, null, "TransferSol", "TransferSol", () => {
                if (v1.Contains("."))
                {
                    int realAmout = (int)PrecisionUtil.StrToDouble(v1);
                    UniversalDialog dialog = null;
                    Action yesAction = () => {
                        dialog.DestroyDialog();
                    };
                    dialog = ShowUniversalNotice("Tips", "You can only transfer integer, so the transfer amount now is:" + realAmout, "Got it", "", yesAction, null);
                    return;
                }
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MWSDK.Solana.Wallet.TransferSol(price, v2, Confirmation.Default, approveFinished, (res) => {
                    CommonResponse<TransferSolResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    TransferSolResponse transResult = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferToken)
        {
            SetInfoPanel("TransferSPLToken","public key", "amount", "token_mint", "decimals", null, null, "Transfer", "Transfer", () => {
                ulong price = PrecisionUtil.StrToULong(v2);
                int decimals = PrecisionUtil.StrToInt(v4);
                MWSDK.Solana.Wallet.TransferToken(v3, decimals, price, v1, approveFinished, (res) => {
                    CommonResponse<TransferTokenResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    TransferTokenResponse transferResult = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientOpenWallet)
        {
            notOpenDetail = true;
            MWSDK.OpenWallet(() => {
                MWSDK.DebugLog("Wallet logout callback runs!!");
            });
        }
        else if (btnName == APINames.ClientOpenMarket)
        {
            notOpenDetail = true;

            MWSDK.OpenMarket("https://jump-devnet.mirrorWorld.fun");
        }
        else if (btnName == APINames.SolMetadataGetCollectionFiltersInfo)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, null, null, "Get", "Get collection filter info", () => {
                string collection = v1;
                MWSDK.Solana.Metadata.GetCollectionFilters(v1, (res) => {
                    CommonResponse<GetCollectionFilterInfoResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    GetCollectionFilterInfoResponse filterInfo = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsSummary)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection 1", "collection 2", null, null, null, null, "Get", "Get collection filter info", () => {
                List<string> cols = new List<string>();
                if (string.IsNullOrWhiteSpace(v1)) cols.Add(v1);
                if (string.IsNullOrWhiteSpace(v2)) cols.Add(v2);
                if (string.IsNullOrWhiteSpace(v1) && string.IsNullOrWhiteSpace(v2))
                {
                    PrintLog("Please input something.");
                    return;
                }
                MWSDK.Solana.Metadata.GetCollectionsSummary(cols, (res) => {
                    CommonResponse<List<SolResMetadataGetCollectionSummary>> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    List<SolResMetadataGetCollectionSummary> collectionInfoList = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsInfo)
        {
            SetInfoPanel("GetCollectionInfo", "collection", null, null, null, null, null, "Get", "Get collection info", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MWSDK.Solana.Metadata.GetCollectionsInfo(collections, (res) => {
                    CommonResponse<List<GetCollectionInfoResponse>> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    List<GetCollectionInfoResponse> infoList = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTEvents)
        {
            SetInfoPanel("GetNFTEvents", "mint address", "page", "page size", null, null, null, "Get", "Get NFT events", () => {
                string mintAddress = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);

                MWSDK.Solana.Metadata.GetNFTEvents(mintAddress, page, pageSize, (res) => {
                    CommonResponse<GetNFTEventsResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    GetNFTEventsResponse events = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTSearchNFT)
        {
            SetInfoPanel("SearchNFTs", "collection 1", "search string", null, null, null, null, "Search", "Search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);
                string searchString = v2;

                MWSDK.Solana.Metadata.SearchNFTs(collections, searchString, (res) => {
                    CommonResponse<List<MirrorMarketNFTObj>> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    List<MirrorMarketNFTObj> nftObjs = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTRecommendSearchNFT)
        {
            SetInfoPanel("RecommendSearchNFT", "collection 1", null, null, null, null, null, "Search", "Recommend search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MWSDK.Solana.Metadata.RecommendSearchNFTs(collections, (res) => {
                    CommonResponse<List<MirrorMarketNFTObj>> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    List<MirrorMarketNFTObj> nftObjs = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTInfo)
        {
            SetInfoPanel("Get NFT Info", "mint address", null, null, null, null, null, "Get", "Get NFTs", () => {
                if (string.IsNullOrWhiteSpace(v1))
                {
                    PrintLog("Please input something.");
                    return;
                }

                MWSDK.Solana.Metadata.GetNFTInfo(v1, (res) => {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTsInfo)
        {
            SetInfoPanel("GetNFTs", "collection", "page", "page size", "orderByString", null, null, "Get", "Get NFTs", () => {
                string collection = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);
                string orderByString = v4;
                bool desc = true;
                List<GetNFTsRequestFilter> filters = new List<GetNFTsRequestFilter>();
                GetNFTsRequestFilter filter1 = new GetNFTsRequestFilter();
                filter1.filter_name = "filter_name";
                filter1.filter_type = "filter_type";
                filter1.filter_value = new List<object>();
                filter1.filter_value.Add(1);
                filter1.filter_value.Add("2");
                filters.Add(filter1);

                MWSDK.Solana.Metadata.GetNFTs(collection, page, pageSize, orderByString, desc, filters, (res) => {
                    CommonResponse<GetNFTsResponse> resObj = res;
                    long code = resObj.code;
                    string message = resObj.message;
                    GetNFTsResponse nftsResult = resObj.data;
                    //todo...

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else
        {
            MWSDK.DebugLog("Unknown btnname:" + btnName);
        }

        if (!notOpenDetail)
        {
            apiInfo.SetActive(true);
            apiList.SetActive(false);
        }
    }
}
