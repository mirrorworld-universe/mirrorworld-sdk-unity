using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorWorld;
using TMPro;
using MirrorworldSDK;
using MirrorworldSDK.UI;
using System.Collections.Generic;

public class BNBClickHandler
{
    public GameObject apiInfo;
    public GameObject apiList;
    private TextMeshProUGUI apiNameText, btnText, contentText;
    private ParamCell cell1, cell2, cell3, cell4, cell5, cell6;
    private string v1, v2, v3, v4, v5, v6;
    private Action clickAction;

    public void Init(GameObject apiInfo, GameObject apiList, TextMeshProUGUI apiNameText, TextMeshProUGUI btnText, TextMeshProUGUI contentText,
ParamCell cell1, ParamCell cell2, ParamCell cell3, ParamCell cell4, ParamCell cell5, ParamCell cell6)
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

    public void handleEVMDemoClick(string btnName)
    {
        ClearLog();
        PrintLog("wait to call...");

        bool notOpenDetail = false;

        Action approveFinished = () =>
        {
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
            MWSDK.BNB.StartLogin((loginResponse) =>
            {
                MWSDK.DebugLog("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MWSDK.BNB.GuestLogin((loginResponse) =>
            {
                MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) =>
                {

                    MWSDK.DebugLog("GuestLogin result:" + JsonUtility.ToJson(loginResponse));

                    UniversalDialog dialog = null;
                    dialog = ShowUniversalNotice("Guest Login", "Guest login success! But a guest account can not visit sensitive APIs.", "OK", null,
                    () =>
                    {
                        LogUtils.LogFlow("Click OK");
                        dialog.DestroyDialog();
                    },
                    null);
                });
            });
        }
        else if (btnName == APINames.ClientLoginWithEmail)
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, null, null, "Login", "Login with a registed email.", () =>
            {
                MWSDK.BNB.LoginWithEmail(v1, v2, (res) =>
                {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientQueryUser)
        {
            SetInfoPanel("QueryUser", "email", null, null, null, null, null, "Query", "Query user info.", () =>
            {
                MWSDK.BNB.QueryUser(v1, (res) =>
                {
                    PrintLog("Query result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () =>
            {
                MWSDK.BNB.Wallet.GetTokens((res) =>
                {
                    PrintLog("Get tokens result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTokensByWallet)
        {
            SetInfoPanel("GetWalletTokens", "wallet address", null, null, null, null, null, "Get", "Get your tokens", () =>
            {
                MWSDK.BNB.Wallet.GetTokensByWalletByWallet(v1, (res) =>
                {
                    PrintLog("Get tokens result:" + res);
                });
            });
        }
        else if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MWSDK.BNB.Logout(() =>
            {
                UniversalDialog dialog = null;
                Action yesAction = () =>
                {
                    dialog.DestroyDialog();
                };
                dialog = ShowUniversalNotice("Logout", "Your logged account has logged out.", "Got it", "", yesAction, null);
            });
        }
        else if (btnName == APINames.SolAssetSearchQueryNFT)
        {
            SetInfoPanel("QueryNFT", "mint address", "token id", null, null, null, null, "GetNFTDetails", "GetNFTDetails", () =>
            {
                MWSDK.BNB.Asset.QueryNFT(v1, v2, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTsByOwner)
        {
            SetInfoPanel("GetNFTOwnedByAddress", "owner address", "limit", "cursor", null, null, null, "Get", "Get NFTs wwned by address", () =>
            {
                int limit = int.Parse(v2);

                MWSDK.BNB.Asset.SearchNFTsByOwner(v1, limit, v3, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetSearchSearchNFTs)
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", "token id", null, null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses", () =>
            {
                List<EVMSearchNFTsByAddressesReqToken> list = new List<EVMSearchNFTsByAddressesReqToken>();
                EVMSearchNFTsByAddressesReqToken token = new EVMSearchNFTsByAddressesReqToken();
                token.token_address = v1;
                token.token_id = int.Parse(v2);
                list.Add(token);
                MWSDK.BNB.Asset.SearchNFTsByMintAddress(list, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MWSDK.BNB.IsLogged((res) =>
                {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == APINames.SolAssetMintCollection)
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", null, null, null, "CreateVerifiedCollection", "CreateVerifiedCollection", () =>
            {
                int seller_fee_basis_points = (int)PrecisionUtil.StrToDouble(v4);
                MWSDK.BNB.Asset.MintCollection(v1, v2, v3, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            }
                );
        }
        else if (btnName == APINames.SolAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "parent collection", "token_id", "to_wallet", "mint amount", null, null, "MintNFT", "MintNFT", () =>
            {
                int amount = PrecisionUtil.StrToInt(v4);
                int token_id = int.Parse(v2);
                MWSDK.BNB.Asset.MintNFT(v1, token_id, v3, amount, Confirmation.Default, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionListNFT)
        {
            SetInfoPanel("ListNFT", "collection address", "token id", "price", "marketplace address", null, null, "ListNFT", "ListNFT", () =>
            {
                int token_id = PrecisionUtil.StrToInt(v2);
                double price = PrecisionUtil.StrToDouble(v3);
                MWSDK.BNB.Asset.ListNFT(v1, token_id, price, v4, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionCancelListing)
        {
            SetInfoPanel("CancelNFTListing", "collection address", "token id", "marketplace_address", null, null, null, "CancelNFTListing", "CancelNFTListing", () =>
            {
                int token_id = PrecisionUtil.StrToInt(v2);
                MWSDK.BNB.Asset.CancelListing(v1, token_id, v3, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionBuyNFT)
        {
            SetInfoPanel("Buy NFT", "mint address", "Price", "token id", "marketplace_address", null, null, "BuyNFT", "BuyNFT", () =>
            {
                double price = PrecisionUtil.StrToDouble(v2);
                int token_id = PrecisionUtil.StrToInt(v3);
                MWSDK.BNB.Asset.BuyNFT(v1, price, token_id, v4, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolAssetAuctionTransferNFT)
        {
            SetInfoPanel("Transfer NFT", "collection address", "token id", "to wallet address", null, null, null, "Transfer", "Transfer", () =>
            {
                int token_id = PrecisionUtil.StrToInt(v2);
                MWSDK.BNB.Asset.TransferNFT(v1, token_id, v3, approveFinished, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactions)
        {
            SetInfoPanel("GetWalletTransactions", "number", null, null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () =>
            {
                double price = PrecisionUtil.StrToDouble(v1);
                MWSDK.BNB.Wallet.GetTransactions(price, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsByWallet)
        {
            SetInfoPanel("GetWalletTransactions", "wallet address", "limit", null, null, null, null, "GetWalletTransactions", "GetWalletTransactions", () =>
            {
                int limit = PrecisionUtil.StrToInt(v2);
                MWSDK.BNB.Wallet.GetTransactionsByWallet(v1, limit, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletGetTransactionsBySignature)
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () =>
            {
                MWSDK.BNB.Wallet.GetTransactionsBySignature(v1, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferETH)
        {
            SetInfoPanel("Transfer ETH", "nonce", "gas price", "gas limit", "to", "amout", null, "Transfer", "Transfer", () =>
            {
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MWSDK.BNB.Wallet.TransferETH(v1, v2, v3, v4, v5, approveFinished, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferBNB)
        {
            SetInfoPanel("Transfer BNB", "nonce", "gas price", "gas limit", "to", "amout", null, "Transfer", "Transfer", () =>
            {
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MWSDK.BNB.Wallet.TransferBNB(v1, v2, v3, v4, v5, approveFinished, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferMatic)
        {
            SetInfoPanel("Transfer Matic", "nonce", "gas price", "gas limit", "to", "amout", null, "Transfer", "Transfer", () =>
            {
                int price = (int)PrecisionUtil.StrToDouble(v1);
                MWSDK.BNB.Wallet.TransferMatic(v1, v2, v3, v4, v5, approveFinished, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolWalletTransferToken)
        {
            SetInfoPanel("TransferToken", "nonce", "gas price", "gas limit", "to", "amout", "contract", "Transfer", "Transfer", () =>
            {
                MWSDK.BNB.Wallet.TransferToken(v1, v2, v3, v4, v5, v6, approveFinished, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.ClientOpenWallet)
        {
            notOpenDetail = true;
            MWSDK.BNB.OpenWallet(() =>
            {
                MWSDK.DebugLog("Wallet logout callback runs!!");
            });
        }
        else if (btnName == APINames.ClientOpenMarket)
        {
            notOpenDetail = true;
            List<string> collections = new List<string>();
            collections.Add("BXqCckKEidhJUpYrg4u2ocdiDKwJY3WujHvVDPTMf6nL");

            MWSDK.BNB.OpenMarket("https://jump-devnet.mirrorworld.fun");
        }
        else if (btnName == APINames.SolMetadataGetCollectionFiltersInfo)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, null, null, "Get", "Get collection filter info", () =>
            {
                string collection = v1;
                MWSDK.BNB.Metadata.GetCollectionFilters(v1, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsSummary)
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection 1", "collection 2", null, null, null, null, "Get", "Get collection filter info", () =>
            {
                List<string> cols = new List<string>();
                if (string.IsNullOrWhiteSpace(v1)) cols.Add(v1);
                if (string.IsNullOrWhiteSpace(v2)) cols.Add(v2);
                if (string.IsNullOrWhiteSpace(v1) && string.IsNullOrWhiteSpace(v2))
                {
                    PrintLog("Please input something.");
                    return;
                }
                MWSDK.BNB.Metadata.GetCollectionsSummary(cols, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataGetCollectionsInfo)
        {
            SetInfoPanel("GetCollectionInfo", "collection", null, null, null, null, null, "Get", "Get collection info", () =>
            {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MWSDK.BNB.Metadata.GetCollectionsInfo(collections, (res) =>
                {

                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTEvents)
        {
            SetInfoPanel("GetNFTEvents", "contract", "page", "page size", "token id", "marketplace_address", null, "Get", "Get NFT events", () =>
            {
                string mintAddress = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);
                int tokenID = int.Parse(v4);

                MWSDK.BNB.Metadata.GetNFTEvents(v1, page, pageSize, tokenID, v5, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTSearchNFT)
        {
            SetInfoPanel("SearchNFTs", "collection 1", "search string", null, null, null, null, "Search", "Search NFTs", () =>
            {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);
                string searchString = v2;

                MWSDK.BNB.Metadata.SearchNFTs(collections, searchString, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTRecommendSearchNFT)
        {
            SetInfoPanel("RecommendSearchNFT", "collection 1", null, null, null, null, null, "Search", "Recommend search NFTs", () =>
            {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MWSDK.BNB.Metadata.RecommendSearchNFTs(collections, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTInfo)
        {
            SetInfoPanel("GetNFTs", "contract", "token id", null, null, null, null, "Get", "Get NFTs", () =>
            {
                //int tokenID = PrecisionUtil.StrToInt(v2);

                MWSDK.BNB.Metadata.GetNFTInfo(v1, v2, (res) =>
                {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == APINames.SolMetadataNFTsInfo)
        {
            SetInfoPanel("GetNFTs", "collection", "page", "page size", "orderByString", null, null, "Get", "Get NFTs", () =>
            {
                string collection = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);
                string orderByString = v4;
                bool desc = true;

                MWSDK.BNB.Metadata.GetNFTs(collection, page, pageSize, orderByString, desc, null, (res) =>
                {
                    PrintLog("result:" + res);
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
