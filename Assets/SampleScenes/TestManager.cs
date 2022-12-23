using System;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    public GameObject apiInfo;
    public GameObject apiList;

    public TextMeshProUGUI apiNameText;
    public TextMeshProUGUI input1;
    public TextMeshProUGUI input2;
    public TextMeshProUGUI input3;
    public TextMeshProUGUI input4;
    public TextMeshProUGUI title1, title2, title3, title4;
    public GameObject inputField1;
    public GameObject inputField2;
    public GameObject inputField3;
    public GameObject inputField4;
    public TextMeshProUGUI btnText;
    public TextMeshProUGUI contentText;

    private string v1, v2, v3, v4;
    private Action clickAction;
    // Start is called before the first frame update
    void Start()
    {
        apiInfo.SetActive(false);
        apiList.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnButtonsClicked()
    {
        var btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("button name:" + btnName);

        ClearLog();
        PrintLog("wait to call...");

        bool notOpenDetail = false;

        if (btnName == "BtnStartLogin")
        {
            notOpenDetail = true;
            MirrorSDK.StartLogin((loginResponse) => {
                Debug.Log("Login result:" + loginResponse.ToString());
            });
        }
        else if (btnName == "BtnEmailLogin")
        {
            SetInfoPanel("LoginWithEmail", "email", "password", null, null, "Login", "Login with a registed email.", () => {
                MirrorSDK.LoginWithEmail(v1,v2,(res)=> {
                    PrintLog("Login result:"+JsonUtility.ToJson(res));
                });
            });
        }
        else if (btnName == "BtnFetchUser")
        {
            SetInfoPanel("FetchUser", "email", null, null, null, "FetchUser", "FetchUser", () => {
                MirrorSDK.FetchUser(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetTokens")
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, "Get", "Get your tokens", () => {
                MirrorSDK.GetTokens((res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("Get tokens result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetAccessToken")
        {
            SetInfoPanel("GetAccessToken", null, null, null, null, "GetAccessToken", "Get Access Token", () => {
                MirrorSDK.GetAccessToken();
                PrintLog("Access token is secret,you can see the result in console.");
            });
        }
        else if (btnName == "BtnLogout")
        {
            notOpenDetail = true;
            MirrorSDK.Logout(()=> {
                PrintLog("Logout success");
            });
        }
        else if (btnName == "BtnGetWallet")
        {
            SetInfoPanel("GetWallet",null,null,null,null, "GetWallet", "Get wallet",()=> {
                MirrorSDK.GetWallet((res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTDetails")
        {
            SetInfoPanel("GetNFTDetails", "mint address", null, null, null, "GetNFTDetails", "GetNFTDetails", () => {
                MirrorSDK.GetNFTDetails(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTsOwnedByAddress")
        {
            SetInfoPanel("GetNFTOwnedByAddress", "owner address", "limit", "offset", null, "Get", "Get NFTs wwned by address", () => {
                List<string> owners = new List<string>();
                owners.Add(v1);
                long limit = long.Parse(v2);
                long offset = long.Parse(v3);
                
                MirrorSDK.GetNFTsOwnedByAddress(owners, limit,offset, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnFetchNFTsByMint")
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", null, null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses",()=> {
                List<string> list = new List<string>();
                list.Add(v1);
                MirrorSDK.FetchNFTsByMintAddress(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnFetchNFTsByCreators")
        {
            SetInfoPanel("FetchNFTsByCreatorAddresses", "creator address", null, null, null, "FetchNFTsByCreatorAddresses", "FetchNFTsByCreatorAddresses", () => {
                List<string> list = new List<string>();
                list.Add(v1);

                MirrorSDK.FetchNFTsByCreatorAddresses(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnFetchNFTsByAuthens")
        {
            SetInfoPanel("FetchNFTsByUpdateAuthorities", "auth address", null, null, null, "FetchNFTsByUpdateAuthorities", "FetchNFTsByUpdateAuthorities", () => {
                List<string> list = new List<string>();
                list.Add(v1);

                MirrorSDK.FetchNFTsByUpdateAuthorities(list, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnIsLoggedIn")
        {
            SetInfoPanel("IsLoggedIn", null, null, null, null, "IsLoggedIn", "IsLoggedIn", () =>
            {
                MirrorSDK.IsLoggedIn((res)=> {
                    PrintLog("result:" + res);
                });
            }
            );
        }
        else if (btnName == "BtnCreateCollection")
        {
            SetInfoPanel("CreateVerifiedCollection", "name", "symbol", "url", null, "CreateVerifiedCollection", "CreateVerifiedCollection", () => {
                MirrorSDK.CreateVerifiedCollection(v1, v2, v3,200, null, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                }); }
                );
        }
        else if (btnName == "BtnMintNFT")
        {
            SetInfoPanel("MintNFT",  "parent collection", "name", "symbol", "url", "MintNFT", "MintNFT",()=> {
                MirrorSDK.MintNFT(v1,v2,v3,v4,null, null, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnListNFT")
        {
            SetInfoPanel("ListNFT", "mint address", "price", null, null, "ListNFT", "ListNFT", ()=> {
                double price = PrecisionUtil.StrToDouble(v2);
                MirrorSDK.ListNFT(v1,price,Confirmation.Default,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnListUpdate")
        {
            SetInfoPanel("UpdateNFTListing", "mint address", "price", null, null, "UpdateNFTListing", "UpdateNFTListing", ()=> {
                double price = PrecisionUtil.StrToDouble(v2);
                MirrorSDK.UpdateNFTListing(v1,price, Confirmation.Default, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnListCancel")
        {
            SetInfoPanel("CancelNFTListing", "mint address", "price", null, null, "CancelNFTListing", "CancelNFTListing", ()=> {
                double price = PrecisionUtil.StrToDouble(v2);
                MirrorSDK.CancelNFTListing(v1, price, Confirmation.Default, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnBuyNFT")
        {
            SetInfoPanel("BuyNFT", "mint address", "Price", null, null, "BuyNFT", "BuyNFT", ()=> {
                double price = PrecisionUtil.StrToDouble(v2);
                Debug.Log("price:"+ price);
                MirrorSDK.BuyNFT(v1,price,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnTransferNFT")
        {
            SetInfoPanel("FetchNFTsByMintAddresses", "mint address", "to wallet", null, null, "FetchNFTsByMintAddresses", "FetchNFTsByMintAddresses",()=> {
                MirrorSDK.TransferNFT(v1,v2,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetWalletTokens")
        {
            SetInfoPanel("GetWalletTokens", null, null, null, null, "GetWalletTokens", "GetWalletTokens", ()=> {
                MirrorSDK.GetTokens((res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetTransactions")
        {
            SetInfoPanel("GetWalletTransactions", "number", "next_before", null, null, "GetWalletTransactions", "GetWalletTransactions", ()=> {
                float price = PrecisionUtil.StrToFloat(v1);
                MirrorSDK.GetTransactions(price,v2,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetWalletTransactionsBySignatrue")
        {
            SetInfoPanel("GetWalletTransactionsBySignatrue", "signature", null, null, null, "GetWalletTransactionsBySignatrue", "GetWalletTransactionsBySignatrue", ()=> {
                MirrorSDK.GetWalletTransactionsBySignatrue(v1, (res) =>
                {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnTransferSol")
        {
            SetInfoPanel("TransferSol", "amount", "public key", null, null, "TransferSol", "TransferSol", ()=> {
                ulong price = PrecisionUtil.StrToULong(v1);
                MirrorSDK.TransferSol(price, v2,Confirmation.Default,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnTransferSPLToken")
        {
            SetInfoPanel("TransferSPLToken", "amount", "public key", "amount", "mint_address", "Transfer", "Transfer", ()=> {
                ulong price = PrecisionUtil.StrToULong(v1);
                int decimals = PrecisionUtil.StrToInt(v3);
                MirrorSDK.TransferSPLToken(v4, decimals, price,v2,(res)=> {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnOpenWallet")
        {
            notOpenDetail = true;
            MirrorSDK.OpenWalletPage(()=> {
                MirrorWrapper.Instance.LogFlow("Wallet logout callback runs!!");
            });
        }
        else if (btnName == "BtnOpenMarket")
        {
            notOpenDetail = true;
            List<string> collections = new List<string>();
            collections.Add("BXqCckKEidhJUpYrg4u2ocdiDKwJY3WujHvVDPTMf6nL");
            //MirrorSDK.OpenMarketPage(collections);
        }
        else if (btnName == "BtnGetCollectionFilterInfo")
        {
            SetInfoPanel("GetCollectionFilterInfo", "collection", null, null, null, "Get", "Get collection filter info", () => {
                string collection = v1;
                MirrorSDK.GetCollectionFilterInfo(v1, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTInfo")
        {
            SetInfoPanel("GetNFTInfo", "mint address", null, null, null, "Get", "Get NFT info", () => {
                string mintAddress = v1;

                MirrorSDK.GetNFTInfo(mintAddress, (res) => {
                    PrintLog("result:" + res);
                });
            });
        }
        else if (btnName == "BtnGetCollectionInfo")
        {
            SetInfoPanel("GetCollectionInfo", "collection", null, null, null, "Get", "Get collection info", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorSDK.GetCollectionInfo(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTEvents")
        {
            SetInfoPanel("GetNFTEvents", "mint address", "page", "page size", null, "Get", "Get NFT events", () => {
                string mintAddress = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);

                MirrorSDK.GetNFTEvents(mintAddress,page,pageSize, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnSearchNFTs")
        {
            SetInfoPanel("SearchNFTs", "collection 1", "search string", null, null, "Search", "Search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);
                string searchString = v2;

                MirrorSDK.SearchNFTs(collections, searchString, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnRecommendSearchNFTs")
        {
            SetInfoPanel("RecommendSearchNFT", "collection 1", null, null, null, "Search", "Recommend search NFTs", () => {
                string collection1 = v1;
                List<string> collections = new List<string>();
                collections.Add(collection1);

                MirrorSDK.RecommendSearchNFT(collections, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTs")
        {
            SetInfoPanel("GetNFTs", "collection", "page", "page size", "orderByString", "Get", "Get NFTs", () => {
                string collection = v1;
                int page = int.Parse(v2);
                int pageSize = int.Parse(v3);
                string orderByString = v4;
                bool desc = true;

                MirrorSDK.GetNFTs(collection,page,pageSize, orderByString,desc,null,(res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }
        else if (btnName == "BtnGetNFTRealPrice")
        {
            SetInfoPanel("GetNFTRealPrice", "collection", "fee", null, null, "Get", "Get NFT real price.", () => {
                string price = v1;
                int fee = int.Parse(v2);

                MirrorSDK.GetNFTRealPrice(price, fee, (res) => {
                    var body = JsonUtility.ToJson(res);
                    PrintLog("result:" + body);
                });
            });
        }

        if (!notOpenDetail)
        {
            apiInfo.SetActive(true);
            apiList.SetActive(false);
        }
    }

    private void SetInfoPanel(string apiName,string hint1,string hint2,string hint3,string hint4,string btnText,string content,Action action)
    {
        apiNameText.text = apiName;
        if(hint1 == null)
        {
            inputField1.gameObject.SetActive(false);
        }
        else
        {
            inputField1.gameObject.SetActive(true);
            input1.text = hint1;
            title1.text = hint1;
        }
        if (hint2 == null)
        {
            inputField2.gameObject.SetActive(false);
        }
        else
        {
            inputField2.gameObject.SetActive(true);
            input2.text = hint2;
            title2.text = hint2;
        }
        if (hint3 == null)
        {
            inputField3.gameObject.SetActive(false);
        }
        else
        {
            inputField3.gameObject.SetActive(true);
            input3.text = hint3;
            title3.text = hint3;
        }
        if (hint4 == null)
        {
            inputField4.gameObject.SetActive(false);
        }
        else
        {
            inputField4.gameObject.SetActive(true);
            input4.text = hint4;
            title4.text = hint4;
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
        v1 = input1.GetParsedText().Trim();
        v1 = SubLastChar(v1);
        v2 = input2.GetParsedText().Trim();
        v2 = SubLastChar(v2);
        v3 = input3.GetParsedText().Trim();
        v3 = SubLastChar(v3);
        v4 = input4.GetParsedText().Trim();
        v4 = SubLastChar(v4);
    }

    private string SubLastChar(string originStr)
    {
        if (originStr.Length == 0) return originStr;
        originStr = originStr.Substring(0, originStr.Length-1);
        return originStr;
    }
}
