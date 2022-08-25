
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine;

public class Test : MonoBehaviour
{
    private readonly string myOwnNFT = "3Ni8o1y4BmhB7qyzDtwRosfvaLdjr5jmyh4jRM1MkZje";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnInitSDKClicked()
    {
        //MirrorSDK.InitSDK("WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ");
        //MirrorSDK.SetDebugMode(true);

        GameObject mirrorObj = new GameObject("MirrorSDK", typeof(MirrorSDK));
        string apiKey = "your api key";
        bool debugMode = true;
        Environment environment = Environment.StagingDevnet;

        MirrorSDK.InitSDK(apiKey, mirrorObj, debugMode, environment);
    }

    public void onBtnStartLoginClicked()
    {
        MirrorSDK.StartLogin();
    }

    public void onBtnClicked()
    {
        var btnName = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        Debug.Log("button name:" + btnName);

        if(btnName == "BtnGetWallet")
        {
            MirrorSDK.GetWalletAddress((result) => {
                Debug.Log(result);
            });
        }
        else if (btnName == "BtnGetAccessToken")
        {
            MirrorSDK.GetAccessToken();
        }
        else if(btnName == "BtnQueryUser")
        {
            string email = "squall19871987@163.com";
            MirrorSDK.QueryUser(email,(user)=> {
                Debug.Log("QueryUser " + email + " result is:" + user.Email);
            });
        }
        else if(btnName == "BtnFetchSingleNFT")
        {
            MirrorSDK.GetNFTDetails(myOwnNFT, (response) => {
                Debug.Log("Fetch success:" + response);
            });
        }
        else if (btnName == "BtnFetchNFTsByMint")
        {
            List<string> addresses = new List<string>();
            addresses.Add(myOwnNFT);
            MirrorSDK.FetchNFTsByMintAddress(addresses,(response)=> {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("FetchNFTsByMintAddress:"+response);
            });
        }
        else if (btnName == "BtnFetchNFTsByCreators")
        {
            List<string> creators = new List<string>();
            creators.Add("GCeY1zY2QFz1iYekbsX1jQjtJnjyxWXtBhxAJPrvG3Bg");
            MirrorSDK.FetchNFTsByCreators(creators, (nfts) =>
            {
                Debug.Log("FetchNFTsByCreators result is:" + nfts.nfts);
            });
        }
        else if (btnName == "BtnFetchNFTsByAuthens")
        {
            List<string> authens = new List<string>();
            authens.Add("4eMGGR6qyvhrSSrHJBjaYkXZpM5kNwbzRQq9q89NfvPC");
            MirrorSDK.FetchNftsByUpdateAuthorities(authens, (res) =>
            {
                var rawRequestBody = JsonConvert.SerializeObject(res);
                Debug.Log("FetchNFTsByUpdateAuthorityAddress result is:" + rawRequestBody);
            });
        }
        else if(btnName == "BtnIsLoggedIn")
        {
            MirrorSDK.IsLoggedIn((isLoggedIn) => {
                Debug.Log("isLoggedIn:" + isLoggedIn);
            });
        }
        else if (btnName == "BtnCreateCollection")
        {
            string name = "Unity.Test.NFT";
            string symbol = "Unity";
            string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

            MirrorSDK.CreateVerifiedCollection(name,symbol,url,(res) => {
                var rawRequestBody = JsonConvert.SerializeObject(res);
                Debug.Log("BtnCreateCollection:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnMintNFT")
        {
            string name = "Unity.Test.NFT";
            string symbol = "Unity";
            string collection = "8EEx4P6srPapg757Wdu9zUBV9hFdRjbVxgvqJq5ynQgn";
            string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

            MirrorSDK.MintNFT(collection, name, symbol, url, (res) => {
                var rawRequestBody = JsonConvert.SerializeObject(res);
                Debug.Log("MintNFT:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnCreateSubCollection")
        {
            string name = "Unity.Test.NFT";
            string symbol = "Unity";
            string collection = "FvGvFKMRs99mEAU5htCvJN8f24zsqTFHbRFUW9zrtCsM";
            string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

            MirrorSDK.CreateVerifiedSubCollection(collection,name,symbol,url,(res) => {
                var rawRequestBody = JsonConvert.SerializeObject(res);
                Debug.Log("CreateVerifiedSubCollection:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnListNFT")
        {
            string mintAddress = "8EEx4P6srPapg757Wdu9zUBV9hFdRjbVxgvqJq5ynQgn";
            decimal price = 1;

            MirrorSDK.ListNFT(mintAddress,price,(response)=> {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("ListNFT:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnListUpdate")
        {
            string mintAddress = "8EEx4P6srPapg757Wdu9zUBV9hFdRjbVxgvqJq5ynQgn";
            decimal price = 1;

            MirrorSDK.UpdateNFTListing(mintAddress, price, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("UpdateNFTListing:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnListCancel")
        {
            string mintAddress = "8EEx4P6srPapg757Wdu9zUBV9hFdRjbVxgvqJq5ynQgn";
            decimal price = 1;

            MirrorSDK.CancelNFTListing(mintAddress, price, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("CancelNFTListing:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnBuyNFT")
        {
            string mintAddress = "";
            decimal price = 1;

            MirrorSDK.BuyNFT(mintAddress, price, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("BuyNFT:" + rawRequestBody);
            });
        }
        else if (btnName == "BtnTransferNFT")
        {
            string mintAddress = "";
            string anotherWallet = "";

            MirrorSDK.TransferNFT(mintAddress, anotherWallet, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("CancelNFTListing:" + rawRequestBody);
            });
        }
        else if(btnName == "BtnGetWalletTokens")
        {
            MirrorSDK.GetWalletTokens((response) => {
                long resCode = response.Code;
                WalletTokenResponse res = response.Data;
                string error = response.Error;
                string message = response.Message;
                string status = response.Status;
            });
        }
        else if (btnName == "BtnGetWalletTransactions")
        {
            decimal number = 1;
            string nextBefore = "next_before";
            MirrorSDK.GetWalletTransactions(number,nextBefore,(response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("GetWalletTransactions" + rawRequestBody);
            });
        }
        else if (btnName == "BtnGetWalletTransactionsBySignatrue")
        {
            string signature = "test_signature";
            MirrorSDK.GetWalletTransactionsBySignatrue(signature, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("GetWalletTransactionsBySignatrue" + rawRequestBody);
            });
        }
        else if (btnName == "BtnTransferSol")
        {
            ulong amount = 10;
            string anotherWallet = "wallet address";
            MirrorSDK.TransferSol(amount, anotherWallet, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("TransferSol" + rawRequestBody);
            });
        }
        else if (btnName == "BtnTransferToken")
        {
            ulong amount = 10;
            string publicKey = "wallet address";
            MirrorSDK.TransferSPLToken(amount, publicKey, (response) => {
                var rawRequestBody = JsonConvert.SerializeObject(response);
                Debug.Log("TransferToken" + rawRequestBody);
            });
        }
    }
}
