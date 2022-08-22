
using System.Collections.Generic;
using MirrorworldSDK;
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
            MirrorSDK.FetchSingleNFT(myOwnNFT, (response) => {
                Debug.Log("Fetch success:" + response);
            });
        }
        else if (btnName == "BtnFetchNFTsByMint")
        {
            List<string> addresses = new List<string>();
            addresses.Add(myOwnNFT);
            MirrorSDK.FetchNFTsByMintAddress(addresses,(response)=> {
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
            MirrorSDK.FetchNFTsByUpdateAuthorityAddress(authens, (nfts) =>
            {
                Debug.Log("FetchNFTsByUpdateAuthorityAddress result is:" + nfts.nfts);
            });
        }

    }
}
