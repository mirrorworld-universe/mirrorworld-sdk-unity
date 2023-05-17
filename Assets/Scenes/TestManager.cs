using System;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;
using MirrorWorld;
using MirrorWorldResponses;

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

        //string response = "{\"status\":\"success\",\"data\":{\"id\":44,\"seed\":\"c8qhvhpOlC5hP4I3or-UX\",\"hash\":\"0xa39b44ee7c788fcaf32c755816225e569ac99f0c40a24b81b982491318081c5b\",\"signature\":\"0xef4cbef46636217bca4e2db63e2634276bcdc5a843d9d51ce35273d4965fdd4d45cac2548034eccbbec02df2bacaab66d38fb4f7d81720ed9481371fcd13c0571b\",\"contract_address\":\"0x5e7f819F1678f46B50e7C13208B7B56804fD020e\",\"contract_type\":\"erc1155\",\"url\":\"https://metadata-assets.mirrorworld.fun/mirror_jump/metadata/1.json\",\"name\":\"aaaa\",\"symbol\":\"ssss\",\"creator_address\":\"0xdB5986d3F431bFdc53b6C796DD96431b6a0B11B4\",\"track_mint\":true,\"burn_mint\":true,\"mint_enabled\":true,\"mint_start_id\":0,\"mint_end_id\":0,\"mint_amount\":0,\"status\":\"done\",\"transaction_hash\":\"0x70c7a81e1b4acf5b89f3401a9b5d8823defa61d4664121158e365209180508d3\",\"client_id\":\"8T5DIY19ujf5PHMpRqgBzyGdxFaIqBaidWfA.niFz1Kj3.mirrorworld.fun\",\"mint_id\":null,\"createdAt\":\"2023-05-06T09:06:09.774Z\",\"updatedAt\":\"2023-05-06T09:06:17.421Z\"},\"code\":0,\"message\":\"Successfully minted collection\"}";
        //CommonResponse<EVMResMintCollection> responseBody = JsonUtility.FromJson<CommonResponse<EVMResMintCollection>>(response);
        //Debug.Log(responseBody);
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
        else if (selectedChain == MirrorChain.SUI)
        {
            InitSUIAPIList();
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
        AddAPIButton(lineAssetMint, APINames.AssetCreateMarketplace);
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
        AddAPIButton(lineAssetMint, APINames.AssetCreateMarketplace);
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
        if (chain == MirrorChain.Ethereum)
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

    private void InitSUIAPIList()
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
        //Transform lineAssetAuction = AddAPILine(apiParent, "Asset/Auction");
        //AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionBuyNFT);
        //AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionCancelListing, 22);
        //AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionListNFT);
        //AddAPIButton(lineAssetAuction, APINames.SolAssetAuctionTransferNFT);
        //Asset/Confirmation
        //Asset/Mint
        //Transform lineAssetMint = AddAPILine(apiParent, "Asset/Mint");
        //AddAPIButton(lineAssetMint, APINames.SolAssetMintCollection);
        //AddAPIButton(lineAssetMint, APINames.SolAssetMintNFT);
        //AddAPIButton(lineAssetMint, APINames.AssetCreateMarketplace);
        //Asset/Search
        //Transform lineAssetSearch = AddAPILine(apiParent, "Asset/Search");
        //AddAPIButton(lineAssetSearch, APINames.SolAssetSearchQueryNFT);
        //AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTs);
        //AddAPIButton(lineAssetSearch, APINames.SolAssetSearchSearchNFTsByOwner, 20);
        //Wallet
        Transform lineWallet = AddAPILine(apiParent, "Wallet");
        AddAPIButton(lineWallet, APINames.SUIWalletGetTransactionByDigest, 18);
        AddAPIButton(lineWallet, APINames.SUIWalletTokens);
        AddAPIButton(lineWallet, APINames.SUITransferSUI);
        AddAPIButton(lineWallet, APINames.SUITransferToken);
        //Metadata/Collection
        //Transform lineMetadataCollections = AddAPILine(apiParent, "Metadata/Collection");
        //AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsInfo, 18);
        //AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionFiltersInfo, 18);
        //AddAPIButton(lineMetadataCollections, APINames.SolMetadataGetCollectionsSummary, 18);
        //Metadata/NFT
        //Transform lineMetadataNFT = AddAPILine(apiParent, "Metadata/NFT");
        //AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTInfo);
        //AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTsInfo);
        //AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTEvents);
        //AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTSearchNFT);
        //AddAPIButton(lineMetadataNFT, APINames.SolMetadataNFTRecommendSearchNFT, 18);
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
    private SUIClickHandler suiHandler = new SUIClickHandler();
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
        else if (selectedChain == MirrorChain.SUI)
        {
            suiHandler.Init(apiInfo, apiList, apiNameText, btnText, contentText, cell1, cell2, cell3, cell4, cell5, cell6);
            suiHandler.handleSUIDemoClick(btnName);
            clickAction = () =>
            {
                suiHandler.OnConfirmClicked();
            };
        }
        else
        {
            MWSDK.DebugLog("Unknwon chain!");
        }
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
