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
            MirrorWorld.StartLogin((loginResponse) => {
                Debug.Log("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MirrorWorld.GuestLogin((loginResponse) => {
                Debug.Log("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

                UniversalDialog dialog = null;
                dialog = ShowUniversalNotice("Guest Login", "Guest login success! your account is :" + loginResponse.user.email, "OK", null,
                () => {
                    dialog.DestroyDialog();
                },
                null);
            });
        }
        else if (btnName == APINames.ClientLoginWithEmail)
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, null, null, "Login", "Login with a registed email.", () => {
                MirrorWorld.LoginWithEmail(v1, v2, (res) => {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () => {
                MWSolana.GetTokens((res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("Get tokens result:" + body);
                });
            });
        }
        else
        if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MirrorWorld.Logout(() => {
                PrintLog("Logout success");
            });
        }
        else if (btnName == APINames.SolAssetSearchQueryNFT)
        {
            SetInfoPanel("QueryNFT", "mint address", null, null, null, null, null, "GetNFTDetails", "GetNFTDetails", () => {
                MWSolana.QueryNFT(v1, (res) => {
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

                MWSolana.SearchNFTsByOwner(owners, limit, offset, (res) => {
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
                MWSolana.SearchNFTsByMintAddress(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MirrorWorld.IsLogged((res) => {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == APINames.SolAssetMintCollection)
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", "seller fee basis points", null, null, "CreateVerifiedCollection", "CreateVerifiedCollection", () => {
                int seller_fee_basis_points = (int)PrecisionUtil.StrToDouble(v4);
                MWSolana.MintCollection(v1, v2, v3, seller_fee_basis_points, null, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            }
                );
        }
        else if (btnName == APINames.SolAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "parent collection", "name", "symbol", "url", null, null, "MintNFT", "MintNFT", () => {
                int amount = PrecisionUtil.StrToInt(v6);
                string mint_id = "demo_test_id";
                MWSolana.MintNFT(v1, v2, v3, v4, Confirmation.Default, mint_id, v5, amount, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetMintUpdateNFTProperties)
        {
            SetInfoPanel("UpdateNFTProperties", "mint address", "name", "updateAuthority", "json url", null, null, "MintNFT", "MintNFT", () => {
                MWSolana.UpdateNFT(v1, v2, "newsymbol", v3, v4, 200, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionListNFT)
        {
            SetInfoPanel("ListNFT", "mint address", "price", "auction_house", null, null, null, "ListNFT", "ListNFT", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MWSolana.ListNFT(v1, price, v3, Confirmation.Default, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionCancelListing)
        {
            SetInfoPanel("CancelNFTListing", "mint address", "price", "auction_house", null, null, null, "CancelNFTListing", "CancelNFTListing", () => {
                double price = PrecisionUtil.StrToDouble(v2);
                MWSolana.CancelListing(v1, price, v3, Confirmation.Default, approveFinished, (res) =>
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
                MWSolana.BuyNFT(v1, price, v3, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionTransferNFT)
        {
            SetInfoPanel("Transfer NFT", "mint address", "to wallet", null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () => {
                MWSolana.TransferNFT(v1, v2, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactions)
        {
            SetInfoPanel("GetWalletTransactions", "number", "next_before", null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () => {
                float price = PrecisionUtil.StrToFloat(v1);
                MWSolana.GetTransactions(price, v2, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsBySignature)
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () => {
                MWSolana.GetTransactionsBySignature(v1, (res) =>
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

                MWSolana.CheckTransactionsStatus(signatures, (res) =>
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
                MWSolana.CheckMintingStatus(mintAddresses, (res) =>
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
                MWSolana.TransferSol(price, v2, Confirmation.Default, approveFinished, (res) => {
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
                MWSolana.TransferToken(v4, decimals, price, v2, approveFinished, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnOpenWallet")
        {
            notOpenDetail = true;
            MirrorWorld.OpenWallet(() => {
                MirrorWrapper.Instance.LogFlow("Wallet logout callback runs!!");
            });
        }
        else if (btnName == "BtnOpenMarket")
        {
            notOpenDetail = true;
            List<string> collections = new List<string>();
            collections.Add("BXqCckKEidhJUpYrg4u2ocdiDKwJY3WujHvVDPTMf6nL");

            MirrorWorld.OpenMarket("https://jump-devnet.mirrorworld.fun");
        }
        else if (btnName == APINames.SolMetadataGetCollectionFiltersInfo)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, null, null, "Get", "Get collection filter info", () => {
                string collection = v1;
                MWSolana.MetadataCollectionFilters(v1, (res) => {
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

                MWSolana.MetadataCollectionsInfo(collections, (res) => {
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

                MWSolana.MetadataNFTEvents(mintAddress, page, pageSize, (res) => {
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

                MWSolana.MetadataSearchNFTs(collections, searchString, (res) => {
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

                MWSolana.MetadataRecommendSearchNFTs(collections, (res) => {
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

                MWSolana.MetadataNFTsByUnabridgedParams(collection, page, pageSize, orderByString, desc, null, (res) => {
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
        v6 = cell4.GetInput();
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
