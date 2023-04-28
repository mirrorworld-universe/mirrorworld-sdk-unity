using System;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;
using MirrorWorld;

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
            InitEVMAPIList(selectedChain);
        }
        else
        {
            MWSDK.DebugLog("Unknown selected chain:"+selectedChain);
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

    private void InitEVMAPIList(MirrorChain chain)
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
        if(chain == MirrorChain.Ethereum)
        {
            AddAPIButton(lineWallet, APINames.SolWalletTransferETH);
        }
        else if (chain == MirrorChain.Polygon)
        {
            AddAPIButton(lineWallet, APINames.SolWalletTransferMatic);
        }
        else if (chain == MirrorChain.BNB)
        {
            AddAPIButton(lineWallet, APINames.SolWalletTransferBNB);
        }
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

    private SolanaClickHandler solanaHandler = new SolanaClickHandler();
    private PolygonClickHandler polygonHandler = new PolygonClickHandler();
    private EthereumClickHandler ethereumHandler = new EthereumClickHandler();
    private BNBClickHandler bnbHandler = new BNBClickHandler();
    public void OnButtonsClicked()
    {
        var btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        MWSDK.DebugLog("button name:" + btnName);

        if(selectedChain == MirrorChain.Solana)
        {
            solanaHandler.Init(apiInfo,apiList,apiNameText,btnText,contentText,cell1,cell2,cell3,cell4,cell5,cell6);
            solanaHandler.handleDemoClick(btnName);
            clickAction = () =>
            {
                solanaHandler.OnConfirmClicked();
            };
        }
        else if (selectedChain == MirrorChain.Ethereum)
        {
            ethereumHandler.Init(apiInfo, apiList, apiNameText, btnText, contentText, cell1, cell2, cell3, cell4, cell5, cell6);
            ethereumHandler.handleEVMDemoClick(btnName);
            clickAction = () =>
            {
                ethereumHandler.OnConfirmClicked();
            };
        }
        else if (selectedChain == MirrorChain.Polygon)
        {
            polygonHandler.Init(apiInfo, apiList, apiNameText, btnText, contentText, cell1, cell2, cell3, cell4, cell5, cell6);
            polygonHandler.handleEVMDemoClick(btnName);
            clickAction = () =>
            {
                polygonHandler.OnConfirmClicked();
            };
        }
        else if (selectedChain == MirrorChain.BNB)
        {
            bnbHandler.Init(apiInfo, apiList, apiNameText, btnText, contentText, cell1, cell2, cell3, cell4, cell5, cell6);
            bnbHandler.handleEVMDemoClick(btnName);
            clickAction = () =>
            {
                bnbHandler.OnConfirmClicked();
            };
        }
        else
        {
            MWSDK.DebugLog("Unknwon chain!");
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
