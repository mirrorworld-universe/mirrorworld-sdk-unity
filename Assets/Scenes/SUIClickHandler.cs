using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Wrapper;
using MirrorWorld;
using TMPro;
using MirrorworldSDK;
using MirrorworldSDK.UI;
using System.Collections.Generic;
using MirrorWorldResponses;
using MirrorworldSDK.Models;

public class SUIClickHandler : BaseClickHandler
{
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

    public void handleSUIDemoClick(string btnName)
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
            MWSDK.StartLogin((loginResponse) =>
            {
                MWSDK.DebugLog("Login result:" + JsonUtility.ToJson(loginResponse));
            });
        }
        else if (btnName == APINames.ClientGuestLogin)
        {
            notOpenDetail = true;
            MWSDK.GuestLogin((loginResponse) =>
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
        else if (btnName == APINames.ClientQueryUser)
        {
            SetInfoPanel("QueryUser", "email", null, null, null, null, null, "Query", "Query user info.", () =>
            {
                MWSDK.QueryUser(v1, (res) =>
                {
                    PrintLog("Query result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientLoginWithEmail)
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, null, null, "Login", "Login with a registed email.", () =>
            {
                MWSDK.LoginWithEmail(v1, v2, (res) =>
                {
                    PrintLog("Login result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientLogout)
        {
            notOpenDetail = true;
            MWSDK.Logout(() =>
            {
                UniversalDialog dialog = null;
                Action yesAction = () =>
                {
                    dialog.DestroyDialog();
                };
                dialog = ShowUniversalNotice("Logout", "Your logged account has logged out.", "Got it", "", yesAction, null);
            });
        }
        else if (btnName == APINames.ClientIsLogged)
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MWSDK.IsLogged((res) =>
                {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        //Asset
        else if (btnName == APINames.SUIAssetGetMintedCollections)
        {
            SetInfoPanel("GetMintedCollections", null, null, null, null, null, null, "Get", "Get your collections", () =>
            {
                MWSDK.SUI.Asset.GetMintedCollections((res) =>
                {
                    CommonResponse<List<SUIResGetMintedCollectionsObj>> resObj = res;
                    string message = resObj.message;
                    List<SUIResGetMintedCollectionsObj> data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetGetMintedNFTOnCollections)
        {
            SetInfoPanel("GetMintedNFTOnCollection", "collection_address", null, null, null, null, null, "Get", "Get NFTs", () =>
            {
                MWSDK.SUI.Asset.GetMintedNFTOnCollection(v1, (res) =>
                {
                    CommonResponse<List<SUIResGetMintedNFTOnCollectionObj>> resObj = res;
                    string message = resObj.message;
                    List<SUIResGetMintedNFTOnCollectionObj> data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetMintCollection)
        {
            SetInfoPanel("MintCollection", "name", "symbol", "description", null, null, null, "Mint", "Mint Collection", () =>
            {
                MWSDK.SUI.Asset.MintCollection(v1, v2, v3, null, (res) =>
                {
                    CommonResponse<SUIResMintCollection> resObj = res;
                    string message = resObj.message;
                    SUIResMintCollection data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetMintNFT)
        {
            SetInfoPanel("MintNFT", "collection_address", "name", "description", "image_url", null, null, "Mint", "Mint NFT", () =>
            {
                MWSDK.SUI.Asset.MintNFT(v1, v2, v3, v3, null, null, (res) =>
                {
                    CommonResponse<SUIResMintNFT> resObj = res;
                    string message = resObj.message;
                    SUIResMintNFT data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetQueryNFT)
        {
            SetInfoPanel("QueryNFT", "nft_object_id", null, null, null, null, null, "Query", "Query NFT", () =>
            {
                MWSDK.SUI.Asset.QueryNFT(v1, (res) =>
                {
                    CommonResponse<List<SUIResQueryNFT>> resObj = res;
                    string message = resObj.message;
                    List<SUIResQueryNFT> data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetSearchNFTsByOwner)
        {
            SetInfoPanel("SearchNFTsByOwner", "owner_address", null, null, null, null, null, "Seach", "Search NFTs", () =>
            {
                MWSDK.SUI.Asset.SearchNFTsByOwner(v1, (res) =>
                {
                    CommonResponse<List<SUIResQueryNFT>> resObj = res;
                    string message = resObj.message;
                    List<SUIResQueryNFT> data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIAssetSearchNFT)
        {
            SetInfoPanel("QueryNFTs", "nft_object_id 1", "nft_object_id 2", null, null, null, null, "Seach", "Search NFTs", () =>
            {
                List<string> nft_object_ids = new List<string>();
                if (!string.IsNullOrWhiteSpace(v1)) nft_object_ids.Add(v1);
                if (!string.IsNullOrWhiteSpace(v2)) nft_object_ids.Add(v2);
                MWSDK.SUI.Asset.SearchNFTs(nft_object_ids, (res) =>
                {
                    CommonResponse<List<SUIResQueryNFT>> resObj = res;
                    string message = resObj.message;
                    List<SUIResQueryNFT> data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        //Wallet
        else if (btnName == APINames.SUIWalletTokens)
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, null, null, "Get", "Get your tokens", () =>
            {
                MWSDK.SUI.Wallet.GetTokens((res) =>
                {
                    CommonResponse<SUIResGetTokens> resObj = res;
                    string message = resObj.message;
                    SUIResGetTokens data = resObj.data;

                    PrintLog("Get tokens result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUIWalletGetTransactionByDigest)
        {
            SetInfoPanel("SUIWalletGetTransactionByDigest", "digest", null, null, null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", () =>
            {
                MWSDK.SUI.Wallet.GetTransactionByDigest(v1, (res) =>
                {
                    CommonResponse<SUIResGetTransactionByDigest> resObj = res;
                    long http_status_code = resObj.http_status_code;
                    string message = resObj.message;
                    SUIResGetTransactionByDigest data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUITransferSUI)
        {
            SetInfoPanel("Transfer SUI", "to public", "amount", null, null, null, null, "TransferSUI", "Transfer", () =>
            {
                int amount = (int)PrecisionUtil.StrToDouble(v2);
                MWSDK.SUI.Wallet.TransferSUI(v1, amount,approveFinished, (res) =>
                {
                    CommonResponse<SUIResTransferSUI> resObj = res;
                    long http_status_code = resObj.http_status_code;
                    string message = resObj.message;
                    SUIResTransferSUI data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.SUITransferToken)
        {
            SetInfoPanel("TransferToken", "to public", "amount", "token", null, null, null, "Transfer", "Transfer", () =>
            {
                int amount = (int)PrecisionUtil.StrToDouble(v2);
                MWSDK.SUI.Wallet.TransferToken(v1, amount, v3, approveFinished, (res) =>
                {
                    CommonResponse<SUIResTransferToken> resObj = res;
                    long http_status_code = resObj.http_status_code;
                    string message = resObj.message;
                    SUIResTransferToken data = resObj.data;

                    PrintLog("result:" + JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == APINames.ClientOpenWallet)
        {
            notOpenDetail = true;
            MWSDK.OpenWallet(() =>
            {
                MWSDK.DebugLog("Wallet logout callback runs!!");
            });
        }
        else if (btnName == APINames.ClientOpenMarket)
        {
            notOpenDetail = true;
            List<string> collections = new List<string>();
            collections.Add("BXqCckKEidhJUpYrg4u2ocdiDKwJY3WujHvVDPTMf6nL");

            MWSDK.OpenMarket("https://jump-devnet.mirrorworld.fun");
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
