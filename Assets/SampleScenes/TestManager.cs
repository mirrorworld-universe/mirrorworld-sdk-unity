using System;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject apiInfo;
    public GameObject apiList;
    public InitPanel initPanel;

    public Transform apiParent;
    public GameObject listLineModel;
    public GameObject listItemModel;

    public TextMeshProUGUI apiNameText;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI contentText;
    public ParamCell cell1;
    public ParamCell cell2;
    public ParamCell cell3;
    public ParamCell cell4;
    public ParamCell cell5;
    public ParamCell cell6;

    private string v1, v2, v3, v4, v5, v6;
    private Action clickAction;
    private MirrorChain selectedChain;
    // Start is called before the first frame update
    void Start()
    {
        initPanel.Show();
        apiInfo.SetActive(false);
        apiList.SetActive(false);
        initPanel.EnterNextPage = (chain) =>
        {
            selectedChain = chain;
            InitAPIList();
            initPanel.Hide();
            apiInfo.SetActive(false);
            apiList.SetActive(true);
        };
    }

    public void InitAPIList()
    {
        for (int i = 0; i < apiParent.childCount; i++)
        {
            Destroy(apiParent.GetChild(i).gameObject);
        }

        if (selectedChain == MirrorChain.Solana)
        {
            InitSolanaAPIList();
        }
        else if (selectedChain == MirrorChain.Ethereum || selectedChain == MirrorChain.Polygon || selectedChain == MirrorChain.BNB)
        {
            InitEVMAPIList();
        }
        else
        {
            Debug.Log("Unknown selected chain:"+selectedChain);
        }
    }

    private void InitSolanaAPIList()
    {
        //Client
        Transform lineClient = AddAPILine(apiParent,"Client");
        AddAPIButton(lineClient, APINames.ClientGuestLogin);
        AddAPIButton(lineClient, APINames.ClientStartLogin);
        AddAPIButton(lineClient, APINames.ClientIsLogged);
        AddAPIButton(lineClient, APINames.ClientLoginWithEmail,22);
        AddAPIButton(lineClient, APINames.ClientLogout);
        AddAPIButton(lineClient, APINames.ClientOpenWallet);
        AddAPIButton(lineClient, APINames.ClientOpenMarket);
        AddAPIButton(lineClient, APINames.ClientQueryUser);
        //AddAPIButton(lineClient, APINames.ClientFetchUser);
        //Asset/Auction
        Transform lineAssetAuction = AddAPILine(apiParent,"Asset/Auction");
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionBuyNFT);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionCancelListing,22);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionListNFT);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionTransferNFT);
        //Asset/Confirmation
        Transform lineAssetConfirmation = AddAPILine(apiParent,"Asset/Confirmation");
        AddAPIButton(lineAssetConfirmation, APINames.SolAssetConfirmationCheckStatusOfMinting,20);
        AddAPIButton(lineAssetConfirmation, APINames.SolAssetConfirmationCheckStatusOfTransactions,18);
        //Asset/Mint
        Transform lineAssetMint = AddAPILine(apiParent,"Asset/Mint");
        AddAPIButton(lineAssetMint, APINames.SolAssetMintCollection);
        AddAPIButton(lineAssetMint, APINames.SolAssetMintNFT);
        AddAPIButton(lineAssetMint, APINames.SolAssetMintUpdateNFTProperties);
        //Asset/Search
        Transform lineAssetSearch = AddAPILine(apiParent,"Asset/Search");
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchQueryNFT);
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTs);
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTsByOwner, 20);
        //Wallet
        Transform lineWallet = AddAPILine(apiParent,"Wallet");
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactions);
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactionsByWallet, 18);
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactionsBySignature,18);
        AddAPIButton(lineWallet, APINames.SolWalletGetTokens);
        AddAPIButton(lineWallet, APINames.SolWalletGetTokensByWallet, 18);
        AddAPIButton(lineWallet, APINames.SolWalletTransferSOL);
        AddAPIButton(lineWallet, APINames.SolWalletTransferToken);
        //Metadata/Collection
        Transform lineMetadataCollections = AddAPILine(apiParent,"Metadata/Collection");
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsInfo, 18);
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionFiltersInfo, 18);
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsSummary, 18);
        //Metadata/NFT
        Transform lineMetadataNFT = AddAPILine(apiParent,"Metadata/NFT");
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTInfo);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTsInfo);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTEvents);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTSearchNFT);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTRecommendSearchNFT, 18);
    }

    private void InitEVMAPIList()
    {
        //Client
        Transform lineClient = AddAPILine(apiParent, "Client");
        AddAPIButton(lineClient, APINames.ClientGuestLogin);
        AddAPIButton(lineClient, APINames.ClientStartLogin);
        AddAPIButton(lineClient, APINames.ClientIsLogged);
        AddAPIButton(lineClient, APINames.ClientLoginWithEmail, 22);
        AddAPIButton(lineClient, APINames.ClientLogout);
        AddAPIButton(lineClient, APINames.ClientOpenWallet);
        AddAPIButton(lineClient, APINames.ClientOpenMarket);
        AddAPIButton(lineClient, APINames.ClientQueryUser);
        //Asset/Auction
        Transform lineAssetAuction = AddAPILine(apiParent, "Asset/Auction");
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionBuyNFT);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionCancelListing, 22);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionListNFT);
        AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionTransferNFT);
        //Asset/Confirmation
        //Asset/Mint
        Transform lineAssetMint = AddAPILine(apiParent, "Asset/Mint");
        AddAPIButton(lineAssetMint, APINames.SolAssetMintCollection);
        AddAPIButton(lineAssetMint, APINames.SolAssetMintNFT);
        //AddAPIButton(lineAssetMint, APINames.SolAssetMintUpdateNFTProperties);
        //Asset/Search
        Transform lineAssetSearch = AddAPILine(apiParent, "Asset/Search");
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchQueryNFT);
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTs);
        AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTsByOwner, 20);
        //Wallet
        Transform lineWallet = AddAPILine(apiParent, "Wallet");
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactions);
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactionsByWallet, 18);
        AddAPIButton(lineWallet, APINames.SolWalletGetTransactionsBySignature, 18);
        AddAPIButton(lineWallet, APINames.SolWalletGetTokens);
        AddAPIButton(lineWallet, APINames.SolWalletGetTokensByWallet, 18);
        AddAPIButton(lineWallet, APINames.SolWalletTransferETH);
        AddAPIButton(lineWallet, APINames.SolWalletTransferToken);
        //Metadata/Collection
        Transform lineMetadataCollections = AddAPILine(apiParent, "Metadata/Collection");
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsInfo, 18);
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionFiltersInfo, 18);
        AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsSummary, 18);
        //Metadata/NFT
        Transform lineMetadataNFT = AddAPILine(apiParent, "Metadata/NFT");
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTInfo);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTsInfo);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTEvents);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTSearchNFT);
        AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTRecommendSearchNFT, 18);
    }

    private Transform AddAPILine(Transform parent,string title)
    {
        Transform lineSystem = Instantiate(listLineModel, parent).transform;
        lineSystem.Find("Title").GetComponent<TextMeshProUGUI>().text = title;
        lineSystem.gameObject.SetActive(true);
        return lineSystem;
    }

    private void AddAPIButton(Transform lineTrans, string apiName, int fontSize)
    {
        //Transform buttonParent = lineTrans.Find("APIs");
        GameObject btnGO = Instantiate(listItemModel, lineTrans);
        btnGO.SetActive(true);
        btnGO.name = apiName;
        TextMeshProUGUI label = btnGO.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        label.text = apiName;
        label.fontSize = fontSize;
    }
    private void AddAPIButton(Transform lineTrans, string apiName)
    {
        //Transform buttonParent = lineTrans.Find("APIs");
        GameObject btnGO = Instantiate(listItemModel, lineTrans);
        btnGO.SetActive(true);
        btnGO.name = apiName;
        TextMeshProUGUI label = btnGO.transform.Find("Text").GetComponent<TextMeshProUGUI>();
        label.text = apiName;
        label.fontSize = 24;
    }

    public void OnButtonsClicked()
    {
        var btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("button name:" + btnName);

        if(selectedChain == MirrorChain.Solana)
        {
            handleSolanaDemoClick(btnName);
        }
        else if(selectedChain == MirrorChain.Ethereum || selectedChain == MirrorChain.Polygon || selectedChain == MirrorChain.BNB)
        {
            handleEVMDemoClick(btnName);
        }
    }

    private void handleSolanaDemoClick(string btnName)
    {
        ClearLog();
        PrintLog("wait to call...");

        bool notOpenDetail = false;

        Action approveFinished = () => {
            MirrorWrapper.Instance.LogFlow("Approve finished!");
        };

        if (btnName == "BtnClearCache")
        {
            notOpenDetail = true;
            PlayerPrefs.DeleteAll();
            MirrorWrapper.Instance.ClearUnitySDKCache();
            MirrorWrapper.Instance.LogFlow("Cleared local storage.");
        }
        else if (btnName == APINames.ClientStartLogin)
        {
            notOpenDetail = true;
            MirrorWorld.Solana.StartLogin((loginResponse) => {
                Debug.Log("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MirrorWorld.Solana.GuestLogin((loginResponse) => {
                MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {

                    Debug.Log("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

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
                MirrorWorld.Solana.LoginWithEmail(v1, v2, (res) => {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientQueryUser)
        {
            SetInfoPanel("QueryUser", "email", null, null, null, null, null, "Query", "Query user info.", () => {
                MirrorWorld.Solana.QueryUser(v1, (res) => {
                    PrintLog("Query result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () => {
                MirrorWorld.Solana.GetTokens((res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("Get tokens result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokensByWallet)
        {
            SetInfoPanel("GetWalletTokensByWallet", "wallet address", "limit", "next_before", null, null, null, "Get", "Get your tokens", () => {
                int limit = PrecisionUtil.StrToInt(v2);
                MirrorWorld.Solana.GetTokensByWalletByWallet(v1,limit,v3,(res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("Get tokens result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MirrorWorld.Solana.Logout(() => {
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
                MirrorWorld.Solana.QueryNFT(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.Solana.SearchNFTsByOwner(owners, limit, offset, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTs)
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", null, null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                List<string> list = new List<string>();
                list.Add(v1);
                MirrorWorld.Solana.SearchNFTsByMintAddress(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MirrorWorld.Solana.IsLogged((res) => {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == APINames.SolAssetMintCollection)
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", "seller fee basis points", null, null, "CreateVerifiedCollection", "CreateVerifiedCollection", () => {
                int seller_fee_basis_points = (int)PrecisionUtil.StrToDouble(v4);
                MirrorWorld.Solana.MintCollection(v1, v2, v3, seller_fee_basis_points, null, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            }
                );
        }
        else if (btnName == APINames.SolAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "parent collection", "name", "symbol", "url", "receive wallet", "amount", "MintNFT", "MintNFT", () => {
                double amount = PrecisionUtil.StrToDouble(v6);
                string mint_id = "demo_test_id";
                MirrorWorld.Solana.MintNFT(v1, v2, v3, v4, Confirmation.Default, mint_id, v5, amount, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetMintUpdateNFTProperties)
        {
            SetInfoPanel("UpdateNFTProperties", "mint address", "name", "updateAuthority", "json url", null, null, "MintNFT", "MintNFT", () => {
                MirrorWorld.Solana.UpdateNFT(v1, v2, "newsymbol", v3, v4, 200, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionListNFT)
        {
            SetInfoPanel("ListNFT", "mint address", "price", "auction_house", null, null, null, "ListNFT", "ListNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MirrorWorld.Solana.ListNFT(v1, price, v3, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionCancelListing)
        {
            SetInfoPanel("CancelNFTListing", "mint address", "price", "auction_house", null, null, null, "CancelNFTListing", "CancelNFTListing", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MirrorWorld.Solana.CancelListing(v1, price, v3, Confirmation.Default, approveFinished, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionBuyNFT)
        {
            SetInfoPanel("Buy NFT", "mint address", "Price", "auction house", null, null, null, "BuyNFT", "BuyNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                Debug.Log("price:" + price);
                MirrorWorld.Solana.BuyNFT(v1, price, v3, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionTransferNFT)
        {
            SetInfoPanel("Transfer NFT", "mint address", "to wallet", null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                MirrorWorld.Solana.TransferNFT(v1, v2, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactions)
        {
            SetInfoPanel("GetWalletTransactions", "number", "next_before", null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                float price = PrecisionUtil.StrToFloat(v1);
                MirrorWorld.Solana.GetTransactions(price, v2, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsByWallet)
        {
            SetInfoPanel("GetWalletTransactions", "wallet address", "limit", "next_before", null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                int limit = PrecisionUtil.StrToInt(v2);
                MirrorWorld.Solana.GetTokensByWalletByWallet(v1, limit, v3, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsBySignature)
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () => {
                MirrorWorld.Solana.GetTransactionsBySignature(v1, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetConfirmationCheckStatusOfTransactions)
        {
            SetInfoPanel("GetStatusOfTransactions", "signature 1", "signature 2", null, null, null, null, "GetStatusOfTransactions", "GetStatusOfTransactions", () => {
                List<string> signatures = new List<string>();
                if (v1 != "") signatures.Add(v1);
                if (v2 != "") signatures.Add(v2);

                MirrorWorld.Solana.CheckTransactionsStatus(signatures, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("GetStatusOfTransactions result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetConfirmationCheckStatusOfMinting)
        {
            SetInfoPanel("GetStatusOfMintings", "mint address 1", "mint address 2", null, null, null, null, "GetStatusOfMintings", "GetStatusOfMintings", () => {
                List<string> mintAddresses = new List<string>();
                if (v1 != "") mintAddresses.Add(v1);
                if (v2 != "") mintAddresses.Add(v2);
                MirrorWorld.Solana.CheckMintingStatus(mintAddresses, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("GetStatusOfMintings result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferSOL)
        {
            SetInfoPanel("TransferSol", "amount", "public key", null, null, null, null, "TransferSol", "TransferSol", () => {
                if (v1.Contains('.'))
                {
                    int realAmout = (int)PrecisionUtil.StrToDouble(v1);
                    UniversalDialog dialog = null;
                    Action yesAction = () => {
                        dialog.DestroyDialog();
                    };
                    dialog = ShowUniversalNotice("Tips", "You can only transfer integer, so the transfer amount now is:" + realAmout, "Got it", "", yesAction, null);
                }
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MirrorWorld.Solana.TransferSol(price, v2, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferToken)
        {
            SetInfoPanel("TransferSPLToken", "amount", "public key", "amount", "mint_address", null, null, "Transfer", "Transfer", () => {
                ulong price = PrecisionUtil.StrToULong(v1);
                int decimals = PrecisionUtil.StrToInt(v3);
                MirrorWorld.Solana.TransferToken(v4, decimals, price, v2, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientOpenWallet)
        {
            notOpenDetail = true;
            MirrorWorld.Solana.OpenWallet(() => {
                MirrorWrapper.Instance.LogFlow("Wallet logout callback runs!!");
            });
        }
        else if (btnName == APINames.ClientOpenMarket)
        {
            notOpenDetail = true;

            MirrorWorld.Solana.OpenMarket("https://jump-devnet.mirrorWorld.fun");
        }
        else if (btnName == APINames.SolMetadataGetCollectionFiltersInfo)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, null, null, "Get", "Get collection filter info", () => {
                string collection = v1;
                MirrorWorld.Solana.MetadataCollectionFilters(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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
                MirrorWorld.Solana.MetadataCollectionsSummary(cols, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsInfo)
        {
            SetInfoPanel("GetCollectionInfo", "collection", null, null, null, null, null, "Get", "Get collection info", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorWorld.Solana.MetadataCollectionsInfo(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTEvents)
        {
            SetInfoPanel("GetNFTEvents", "mint address", "page", "page size", null, null, null, "Get", "Get NFT events", () => {
                string mintAddress = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);

                MirrorWorld.Solana.MetadataNFTEvents(mintAddress, page, pageSize, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.Solana.MetadataSearchNFTs(collections, searchString, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTRecommendSearchNFT)
        {
            SetInfoPanel("RecommendSearchNFT", "collection 1", null, null, null, null, null, "Search", "Recommend search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorWorld.Solana.MetadataRecommendSearchNFTs(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.Solana.MetadataNFTInfo(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.Solana.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, null, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("Unknown btnname:"+btnName);
        }

        if (!notOpenDetail)
        {
            apiInfo.SetActive(true);
            apiList.SetActive(false);
        }
    }

    private void handleEVMDemoClick(string btnName)
    {
        ClearLog();
        PrintLog("wait to call...");

        bool notOpenDetail = false;

        Action approveFinished = () => {
            MirrorWrapper.Instance.LogFlow("Approve finished!");
        };

        if (btnName == "BtnClearCache")
        {
            notOpenDetail = true;
            PlayerPrefs.DeleteAll();
            MirrorWrapper.Instance.ClearUnitySDKCache();
            MirrorWrapper.Instance.LogFlow("Cleared local storage.");
        }
        else if (btnName == APINames.ClientStartLogin)
        {
            notOpenDetail = true;
            MirrorWorld.EVM.StartLogin((loginResponse) => {
                Debug.Log("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MirrorWorld.EVM.GuestLogin((loginResponse) => {
                MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {

                    Debug.Log("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

                    UniversalDialog dialog = null;
                    dialog = ShowUniversalNotice("Guest Login", "Guest login success! But a guest account can not visit sensitive APIs.", "OK", null,
                    () => {
                        LogUtils.LogFlow("Click OK");
                        dialog.DestroyDialog();
                    },
                    null);
                });
                //Debug.Log("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

                //UniversalDialog dialog = null;
                //dialog = ShowUniversalNotice("Guest Login", "Guest login success! your account is :" + loginResponse.user.email, "OK", null,
                //() => {
                //    LogUtils.LogFlow("Click OK");
                //    dialog.DestroyDialog();
                //},
                //null);
            });
        }
        else if (btnName == APINames.ClientLoginWithEmail)
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, null, null, "Login", "Login with a registed email.", () => {
                MirrorWorld.EVM.LoginWithEmail(v1, v2, (res) => {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientQueryUser)
        {
            SetInfoPanel("QueryUser", "email", null, null, null, null, null, "Query", "Query user info.", () => {
                MirrorWorld.EVM.QueryUser(v1, (res) => {
                    PrintLog("Query result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () => {
                MirrorWorld.EVM.GetTokens((res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("Get tokens result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MirrorWorld.EVM.Logout(() => {
                UniversalDialog dialog = null;
                Action yesAction = () => {
                    dialog.DestroyDialog();
                };
                dialog = ShowUniversalNotice("Logout", "Your logged account has logged out.", "Got it", "", yesAction, null);
            });
        }
        else if (btnName == APINames.SolAssetSearchQueryNFT)
        {
            SetInfoPanel("QueryNFT", "mint address", "token id", null, null, null, null, "GetNFTDetails", "GetNFTDetails", () => {
                MirrorWorld.EVM.QueryNFT(v1, v2, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTsByOwner)
        {
            SetInfoPanel("GetNFTOwnedByAddress", "owner address", "limit", "cursor", null, null, null, "Get", "Get NFTs wwned by address", () => {
                int limit = int.Parse(v2);

                MirrorWorld.EVM.SearchNFTsByOwner(v1, limit, v3, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTs)
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", "token id", null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                List<EVMSearchNFTsByAddressesReqToken> list = new List<EVMSearchNFTsByAddressesReqToken>();
                EVMSearchNFTsByAddressesReqToken token = new EVMSearchNFTsByAddressesReqToken();
                token.token_address = v1;
                token.token_id = int.Parse(v2);
                list.Add(token);
                MirrorWorld.EVM.SearchNFTsByMintAddress(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MirrorWorld.EVM.IsLogged((res) => {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == APINames.SolAssetMintCollection)
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", null, null, null, "CreateVerifiedCollection", "CreateVerifiedCollection", () => {
                int seller_fee_basis_points = (int)PrecisionUtil.StrToDouble(v4);
                MirrorWorld.EVM.MintCollection(v1, v2, v3, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            }
                );
        }
        else if (btnName == APINames.SolAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "parent collection", "token_id", "to_wallet", "mint amount", null, null, "MintNFT", "MintNFT", () => {
                int amount = PrecisionUtil.StrToInt(v4);
                int token_id = int.Parse(v2);
                MirrorWorld.EVM.MintNFT(v1, token_id, v3, amount, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        //else if (btnName == APINames.SolAssetMintUpdateNFTProperties)
        //{
        //    SetInfoPanel("UpdateNFTProperties", "mint address", "name", "updateAuthority", "json url", null, null, "MintNFT", "MintNFT", () => {
        //        MirrorWorld.EVM.UpdateNFT(v1, v2, "newsymbol", v3, v4, 200, Confirmation.Default, approveFinished, (res) => {
        //            var body = JsonUtility.ToJson(res);
        //            PrintLog("result:" + body);
        //        });
        //    });
        //}
        else if (btnName == APINames.SolAssetAuctionListNFT)
        {
            SetInfoPanel("ListNFT", "collection address", "token id", "price", "marketplace address", null, null, "ListNFT", "ListNFT", () => {
                int token_id = PrecisionUtil.StrToInt(v2);
                double price = PrecisionUtil.StrToDouble(v3);
                MirrorWorld.EVM.ListNFT(v1, token_id, price, v4, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionCancelListing)
        {
            SetInfoPanel("CancelNFTListing", "collection address", "token id", "marketplace_address", null, null, null, "CancelNFTListing", "CancelNFTListing", () => {
                int token_id = PrecisionUtil.StrToInt(v2);
                MirrorWorld.EVM.CancelListing(v1, token_id, v3, approveFinished, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionBuyNFT)
        {
            SetInfoPanel("Buy NFT", "mint address", "Price", "token id", "marketplace_address", null, null, "BuyNFT", "BuyNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                int token_id = PrecisionUtil.StrToInt(v3);
                MirrorWorld.EVM.BuyNFT(v1, price, token_id, v4, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionTransferNFT)
        {
            SetInfoPanel("Transfer NFT", "collection address", "token id", "to wallet address", null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                int token_id = PrecisionUtil.StrToInt(v2);
                MirrorWorld.EVM.TransferNFT(v1, token_id, v3, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactions)
        {
            SetInfoPanel("GetWalletTransactions", "number", null, null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                double price = PrecisionUtil.StrToDouble(v1);
                MirrorWorld.EVM.GetTransactions(price, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsBySignature)
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () => {
                MirrorWorld.EVM.GetTransactionsBySignature(v1, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferSOL)
        {
            SetInfoPanel("TransferETH", "nonce", "gas price", "gas limit", "to", "amout", null, "Transfer", "Transfer", () => {
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MirrorWorld.EVM.TransferETH(v1, v2, v3, v4, v5, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferToken)
        {
            SetInfoPanel("TransferToken", "nonce", "gas price", "gas limit", "to", "amout", "contract", "Transfer", "Transfer", () => {
                MirrorWorld.EVM.TransferToken(v1, v2, v3, v4, v5, v6, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientOpenWallet)
        {
            notOpenDetail = true;
            MirrorWorld.EVM.OpenWallet(() => {
                MirrorWrapper.Instance.LogFlow("Wallet logout callback runs!!");
            });
        }
        else if (btnName == APINames.ClientOpenMarket)
        {
            notOpenDetail = true;
            List<string> collections = new List<string>();
            collections.Add("BXqCckKEidhJUpYrg4u2ocdiDKwJY3WujHvVDPTMf6nL");

            MirrorWorld.EVM.OpenMarket("https://jump-devnet.MirrorWorld.EVM.fun");
        }
        else if (btnName == APINames.SolMetadataGetCollectionFiltersInfo)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, null, null, "Get", "Get collection filter info", () => {
                string collection = v1;
                MirrorWorld.EVM.MetadataCollectionFilters(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsInfo)
        {
            SetInfoPanel("GetCollectionInfo", "collection", null, null, null, null, null, "Get", "Get collection info", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorWorld.EVM.MetadataCollectionsInfo(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTEvents)
        {
            SetInfoPanel("GetNFTEvents", "contract", "page", "page size", "token id", "marketplace_address", null, "Get", "Get NFT events", () => {
                string mintAddress = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);
                int tokenID = int.Parse(v4);

                MirrorWorld.EVM.MetadataNFTEvents(v1, page, pageSize, tokenID, v5, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.EVM.MetadataSearchNFTs(collections, searchString, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTRecommendSearchNFT)
        {
            SetInfoPanel("RecommendSearchNFT", "collection 1", null, null, null, null, null, "Search", "Recommend search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorWorld.EVM.MetadataRecommendSearchNFTs(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
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

                MirrorWorld.EVM.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, null, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("Unknown btnname:" + btnName);
        }

        if (!notOpenDetail)
        {
            apiInfo.SetActive(true);
            apiList.SetActive(false);
        }
    }

    private void SetInfoPanel(string apiName,string hint1,string hint2, string hint3, string hint4, string hint5,string hint6,string btnText,string content,Action action)
    {
        apiNameText.text = apiName;
        if(hint1 == null)
        {
            cell1.Hide();
        }
        else
        {
            cell1.Show(hint1,hint1);
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

    public void OnConfirmClicked()
    {
        UpdateValues();
        clickAction();
    }

    public void OnReturnClicked()
    {
        apiInfo.SetActive(false);
        apiList.SetActive(true);
    }

    private void PrintLog(string content)
    {
        contentText.text += "MirrorTest:" + content + "\n";
        GUIUtility.systemCopyBuffer = content;
    }

    private void ClearLog()
    {
        contentText.text = "";
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
        originStr = originStr.Substring(0, originStr.Length-1);
        return originStr;
    }

    private UniversalDialog ShowUniversalNotice(string title, string content, string yesText, string noText, Action yesAction, Action noAction)
    {
        MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
        GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("UniversalDialog", GameObject.Find("Canvas").transform);
        UniversalDialog dialog = dialogCanvas.GetComponent<UniversalDialog>();
        dialog.Init(title,content,yesText,noText,yesAction,noAction);
        return dialog;
    }
}
