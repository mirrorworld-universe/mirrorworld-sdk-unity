using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using UnityEngine;
using static MirrorworldSDK.Wrapper.MirrorWrapper;

public class MirrorSDK : MonoBehaviour
{
    #region settings
    [Header("AppInfo")]
    [Tooltip("You can get it on your developer management.")]
    public string apiKey = Constant.SDKDefaultAPIKeyValue;
    [Tooltip("Open debug mode")]
    public bool debugMode = false;
    [Tooltip("runtime environment")]
    public MirrorEnvPublic environment = MirrorEnvPublic.ProductionDevnet;

    [Tooltip("Temp Attr")]
    public string debugEmail = "";
    #endregion settings


    private void Awake()
    {
        if (apiKey == "" || apiKey == "your api key")
        {
            MirrorWrapper.Instance.LogFlow("Please input an api key");
            return;
        }
        Debug.Log("Unity apikey:"+apiKey);
        MirrorEnv env = MirrorEnv.ProductionDevnet;
        if(environment == MirrorEnvPublic.ProductionMainnet)
        {
            env = MirrorEnv.ProductionMainnet;
        }
        InitSDK(apiKey, gameObject, debugMode, env);

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
        MirrorWrapper.Instance.SetDebugEmail(debugEmail);
#endif
    }

    public static void InitSDK(string apiKey, GameObject gameObject, bool useDebug, MirrorEnv environment)
    {
        Debug.Log("env:"+ environment);
        //CreateNftRequest requestBody = new CreateNftRequest();

        //requestBody.name = "";
        //requestBody.symbol = "aaafb";
        //requestBody.url = "urlsafds";
        //requestBody.collection_mint = "";
        ////requestBody.mint_id = "";
        //string finalstr = JsonUtility.ToJson(requestBody);
        //Debug.Log("final value is1:" + finalstr);
        //finalstr = JsonAttrRemover.RemoveEmptyAttr(finalstr);
        //Debug.Log("final value is:" + finalstr);

        //ApproveListNFT apiParams = new ApproveListNFT();
        //apiParams.mint_address = "zxlkjkasdfasdf";
        //apiParams.price = "0.1";

        //CommonApprove<ApproveListNFT> jsonObject = new CommonApprove<ApproveListNFT>();
        //jsonObject.type = MirrorSafeOptType.TransferSPLToken;
        //jsonObject.message = "test";
        //jsonObject.value = "0";
        //MirrorWrapper.Instance.HandleValue(jsonObject, apiParams);
        //jsonObject.paramsPlaceHolder = apiParams;
        //Debug.Log("final value is:" + JsonUtility.ToJson(jsonObject));


        //Test
        environment = MirrorEnv.StagingDevNet;

        if (environment == MirrorEnv.StagingDevNet || environment == MirrorEnv.StagingMainNet)
        {
            Debug.LogError("Environment error!");
        }

        DontDestroyOnLoad(gameObject);

        MonoBehaviour monoBehaviour = gameObject.GetComponent<MonoBehaviour>();

        MirrorWrapper.Instance.InitSDK(monoBehaviour, environment, apiKey, useDebug);

        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidInitSDK(apiKey,environment);
            
            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.IOSInitSDK((int)environment,apiKey);

            MirrorWrapper.Instance.LogFlow("Mirror SDK Inited.");
#endif


    }

    /// <summary>
    /// Set if use debug mode
    /// </summary>
    /// <param name="useDebug"></param>
    public static void SetDebugMode(bool useDebug)
    {
        MirrorWrapper.Instance.SetDebug(useDebug);

#if (UNITY_ANDROID && !(UNITY_EDITOR))

            MirrorWrapper.Instance.AndroidSetDebug(useDebug);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.LogFlow("IOS is not implemented.");
#endif
    }


    /// <summary>
    /// Open login page.
    /// </summary>
    /// <param name="action"></param>
    public static void StartLogin(Action<LoginResponse> action)
    {
        MirrorWrapper.Instance.LogFlow("Start login logic...");

#if (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)

        MirrorWrapper.Instance.LogFlow("Start login in unity...");

        MirrorWrapper.Instance.IsLoggedIn((logged)=> {
            if (logged)
            {
                LoginResponse loginResponse = MirrorWrapper.Instance.GetFakeLoginResponse();

                if (action != null) action(loginResponse);
            }
            else
            {
                MirrorWrapper.Instance.GetLoginSession(MirrorWrapper.Instance.debugEmail, (startSuccess) => {

                    MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();

                    GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("DialogCanvas", monoBehaviour.transform);

                    MirrorWrapper.Instance.LogFlow("Open login page result:" + startSuccess);

                }, action);
            }
        });
#elif UNITY_ANDROID && !(UNITY_EDITOR)

            MirrorWrapper.Instance.LogFlow("Start login in android...");

            MirrorWrapper.Instance.AndroidStartLogin(action);

#elif UNITY_IOS && !(UNITY_EDITOR)

        MirrorWrapper.iOSLoginAction = action;
            MirrorWrapper.Instance.LogFlow("Start login in iOS...");
            IOSLoginCallback handler = new IOSLoginCallback(MirrorWrapper.iOSloginCallback);
            IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);
            MirrorWrapper.IOSStartLogin(fp);
#endif

    }

    /// <summary>
    /// Login with email,this email must registed.
    /// </summary>
    /// <param name="emailAddress"></param>
    /// <param name="password"></param>
    /// <param name="callBack"></param>
    public static void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
    {
        MirrorWrapper.Instance.LoginWithEmail(emailAddress, password, callBack);
    }

    public static void Logout(Action logoutAction)
    {
        MirrorWrapper.Instance.Logout(logoutAction);
    }

    public static void GetWallet(Action<UserResponse> callback)
    {
        UserResponse user = MirrorWrapper.Instance.GetCurrentUser();
        if (user != null)
        {
            MirrorWrapper.Instance.LogFlow("Have old current user,use old data.");
            callback(user);
        }
        else
        {
            MirrorWrapper.Instance.LogFlow("No old current user,try to get data.");
            MirrorWrapper.Instance.GetCurrentUserInfo((response) => {
                callback(response.data);
            });
        }
    }

    public static void GetAccessToken(Action<bool> action)
    {
        MirrorWrapper.Instance.GetAccessToken(action);
    }

    public static void FetchUser(string email, Action<CommonResponse<UserResponse>> callback)
    {
        MirrorWrapper.Instance.FetchUser(email, (response) =>
        {
            callback(response);
        });
    }

    public static void IsLoggedIn(Action<bool> action)
    {
        MirrorWrapper.Instance.IsLoggedIn(action);
    }

    #region mint

    public static void MintNFT(string parentCollection, string nFTName, string nFTSymbol, string nFTJsonUrl, string confirmation, string mint_id,Action<CommonResponse<MintResponse>> callBack)
    {
        ApproveMintNFT requestParams = new ApproveMintNFT();
        requestParams.collection_mint = parentCollection;
        requestParams.name = nFTName;
        requestParams.symbol = nFTSymbol;
        requestParams.url = nFTJsonUrl;

        if (confirmation == null || confirmation == "")
        {
            confirmation = Confirmation.Confirmed;
        }
        requestParams.confirmation = confirmation;

        MirrorWrapper.Instance.GetSecurityToken<ApproveMintNFT>(MirrorSafeOptType.MintNFT,"mint nft", requestParams,()=> {
            MirrorWrapper.Instance.MintNFT(parentCollection, nFTName, nFTSymbol, nFTJsonUrl, confirmation, mint_id, callBack);
        });
    }

    public static void CreateVerifiedCollection(string collectionName, string collectionSymbol, string collectionInfoUrl,int seller_fee_basis_points, string confirmation, Action<CommonResponse<MintResponse>> callBack)
    {
        ApproveCreateCollection requestParams = new ApproveCreateCollection();
        requestParams.name = collectionName;
        requestParams.symbol = collectionSymbol;
        requestParams.url = collectionInfoUrl;
        requestParams.confirmation = confirmation;
        requestParams.seller_fee_basis_points = seller_fee_basis_points;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.CreateCollection, "create collection", requestParams, () => {
            MirrorWrapper.Instance.CreateVerifiedCollection(collectionName, collectionSymbol, collectionInfoUrl, confirmation, callBack);
        });
    }

    #endregion

    #region marketplace

    public static void GetNFTDetails(string mintAddress, Action<CommonResponse<SingleNFTResponse>> action)
    {
        MirrorWrapper.Instance.GetNFTDetails(mintAddress, action);
    }

    public static void GetActivityOfSingleNFT(string mintAddress, Action<CommonResponse<ActivityOfSingleNftResponse>> action)
    {
        MirrorWrapper.Instance.GetActivityOfSingleNFT(mintAddress, action);
    }

    public static void GetNFTsOwnedByAddress(List<string> owners, long limit, long offset, Action<CommonResponse<MultipleNFTsResponse>> callBack)
    {
        MirrorWrapper.Instance.GetNFTsOwnedByAddress(owners,limit,offset, callBack);
    }

    public static void FetchNFTsByMintAddress(List<string> mintAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNFTsByMintAddresses(mintAddresses, action);
    }

    public static void FetchNFTsByCreatorAddresses(List<string> creators, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNftsByCreatorAddresses(creators, action);
    }

    public static void FetchNFTsByUpdateAuthorities(List<string> updateAuthorityAddresses, Action<CommonResponse<MultipleNFTsResponse>> action)
    {
        MirrorWrapper.Instance.FetchNftsByUpdateAuthorities(updateAuthorityAddresses, action);
    }

    public static void ListNFT(string mintAddress, double price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorSDK.ListNFT(mintAddress, price, "", confirmation, callBack);
    }

    public static void ListNFT(string mint_address, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        ApproveListNFT requestParams = new ApproveListNFT();
        requestParams.mint_address = mint_address;
        requestParams.price = price.ToString();
        requestParams.confirmation = confirmation;
        requestParams.auction_house = auction_house;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.ListNFT, "list nft", requestParams, () => {
            MirrorWrapper.Instance.ListNFT(mint_address, price, auction_house, confirmation, callBack);
        });
    }

    public static void CancelNFTListing(string mintAddress, double price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorSDK.CancelNFTListing(mintAddress, price, "", confirmation, callBack);
    }

    public static void CancelNFTListing(string mint_address, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        ApproveListNFT requestParams = new ApproveListNFT();
        requestParams.mint_address = mint_address;
        requestParams.price = price.ToString();
        requestParams.confirmation = confirmation;
        requestParams.auction_house = auction_house;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.CancelListing, "cancel list nft", requestParams, () => {
            MirrorWrapper.Instance.CancelNFTListing(mint_address, price, auction_house, confirmation, callBack);
        });
    }

    public static void UpdateNFTListing(string mintAddress, double price, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorSDK.UpdateNFTListing(mintAddress, price, "", confirmation, callBack);
    }


    public static void UpdateNFTListing(string mint_address, double price, string auction_house, string confirmation, Action<CommonResponse<ListingResponse>> callBack)
    {
        ApproveListNFT requestParams = new ApproveListNFT();
        requestParams.mint_address = mint_address;
        requestParams.price = price.ToString();
        requestParams.confirmation = confirmation;
        requestParams.auction_house = auction_house;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.UpdateListing, "update list nft", requestParams, () => {
            MirrorWrapper.Instance.UpdateNFTListing(mint_address, price, auction_house, confirmation, callBack);
        });
    }

    public static void BuyNFT(string mintAddress, double price, Action<CommonResponse<ListingResponse>> callBack)
    {
        MirrorSDK.BuyNFT(mintAddress, price, "", callBack);
    }

    public static void BuyNFT(string mint_address, double price, string auction_house, Action<CommonResponse<ListingResponse>> callBack)
    {
        ApproveListNFT requestParams = new ApproveListNFT();
        requestParams.mint_address = mint_address;
        requestParams.price = price.ToString();
        requestParams.auction_house = auction_house;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.BuyNFT, "buy nft", requestParams, () => {
            MirrorWrapper.Instance.BuyNFT(mint_address, price, auction_house, callBack);
        });
    }

    public static void TransferNFT(string mint_address, string to_wallet_address, Action<CommonResponse<ListingResponse>> callBack)
    {
        ApproveTransferNFT requestParams = new ApproveTransferNFT();
        requestParams.mint_address = mint_address;
        requestParams.to_wallet_address = to_wallet_address;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.TransferNFT, "transfer nft", requestParams, () => {
            MirrorWrapper.Instance.TransferNFT(mint_address, to_wallet_address, callBack);
        });
    }

    #endregion

    #region Wallet
    public static void GetTokens(Action<CommonResponse<WalletTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTokens(action);
    }
    public static void GetTransactions(double number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTransactions(number, nextBefore, action);
    }
    public static void GetWalletTransactionsBySignatrue(string signature, Action<CommonResponse<TransferTokenResponse>> action)
    {
        MirrorWrapper.Instance.GetWalletTransactionsBySignatrue(signature, action);
    }
    public static void TransferSol(ulong amount, string to_publickey, string confirmation, Action<CommonResponse<TransferSolResponse>> callBack)
    {
        ApproveTransferSOL requestParams = new ApproveTransferSOL();
        requestParams.to_publickey = to_publickey;
        requestParams.amount = amount;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.TransferSol, "transfer sol", requestParams, () => {
            MirrorWrapper.Instance.TransferSol(amount, to_publickey, confirmation, callBack);
        });
    }
    public static void TransferSPLToken(string token_mint, int decimals,ulong amount, string to_publickey, Action<CommonResponse<TransferTokenResponse>> callBack)
    {
        ApproveTransferSPLToken requestParams = new ApproveTransferSPLToken();
        requestParams.to_publickey = to_publickey;
        requestParams.amount = amount;
        requestParams.token_mint = token_mint;
        requestParams.decimals = decimals;

        MirrorWrapper.Instance.GetSecurityToken(MirrorSafeOptType.TransferSPLToken, "transfer spl token", requestParams, () => {
            MirrorWrapper.Instance.TransferSPLToken(token_mint,decimals,amount,to_publickey,callBack);
        });
    }
    #endregion

    #region unity debug flow

    public static void CompleteLoginWithSession(Action<bool> action)
    {
        string token = MirrorWrapper.Instance.GetDebugSession();

        if (token == "")
        {
            MirrorWrapper.Instance.LogFlow("Please start debug login first.");

            return;
        }

        MirrorWrapper.Instance.CompleteLoginWithSession(token, (loginRes) => {

            if (loginRes.code != (long)MirrorResponseCode.Success)
            {
                MirrorWrapper.Instance.LogFlow("Login failed.");

                action(false);
            }
            else
            {
                action(true);
            }
        });
    }

    public static void LoginDebugClear()
    {
        MirrorWrapper.Instance.LoginDebugClear();
    }

    #endregion

    #region market ui
    public static void OpenWalletPage(Action walletLogoutAction)
    {
        string walletUrl = Instance.GetWalletUrl();

        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenWalletPage(walletLogoutAction);
        }
        else
        {

#if (UNITY_ANDROID && !(UNITY_EDITOR))

             MirrorWrapper.Instance.AndroidOpenWallet(walletUrl, walletLogoutAction);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.Instance.walletLogoutAction = walletLogoutAction;
            //MirrorWrapper.OpenWallet();
            iOSWalletLogOutCallback handler = new iOSWalletLogOutCallback(MirrorWrapper.iOSWalletCallBack);
            IntPtr fp = Marshal.GetFunctionPointerForDelegate(handler);

            iOSWalletLoginTokenCallback handler2 = new iOSWalletLoginTokenCallback(MirrorWrapper.iOSWalletLoginCallback);
            IntPtr fp2 = Marshal.GetFunctionPointerForDelegate(handler2);

            MirrorWrapper.IOSOpenWallet(fp, fp2);
#endif
        }
    }

    public static void OpenMarketPage(string marketUrl)
    {
        string url = Instance.GetMarketUrl(marketUrl);

        if (MirrorUtils.IsEditor())
        {
            MirrorWrapper.Instance.DebugOpenMarketPage(url);
        }
        else
        {
#if (UNITY_ANDROID && !(UNITY_EDITOR))

             MirrorWrapper.Instance.AndroidOpenMarket(url);

#elif (UNITY_IOS && !(UNITY_EDITOR))

            MirrorWrapper.IOSOpenMarketPlace(url);
#endif
        }
    }
    #endregion

    #region market ui apis
    public static void GetCollectionFilterInfo(string collection, Action<CommonResponse<GetCollectionFilterInfoResponse>> callBack)
    {
        MirrorWrapper.Instance.GetCollectionFilterInfo(collection,callBack);
    }
    public static void GetNFTInfo(string mintAddress, Action<string> callBack)
    {
        MirrorWrapper.Instance.GetNFTInfo(mintAddress, callBack);
    }
    public static void GetCollectionInfo(List<string> collections, Action<CommonResponse<GetCollectionInfoResponse>> callback)
    {
        MirrorWrapper.Instance.GetCollectionInfo(collections, callback);
    }
    public static void GetNFTEvents(string mintAddress, int page, int pageSize, Action<CommonResponse<GetNFTEventsResponse>> callback)
    {
        MirrorWrapper.Instance.GetNFTEvents(mintAddress, page, pageSize, callback);
    }
    public static void SearchNFTs(List<string> collections, string searchString, Action<CommonResponse<SearchNFTsRequest>> callback)
    {
        MirrorWrapper.Instance.SearchNFTs(collections, searchString, callback);
    }
    public static void RecommendSearchNFT(List<string> collections, Action<CommonResponse<List<MirrorMarketNFTObj>>> callback)
    {
        MirrorWrapper.Instance.RecommendSearchNFT(collections, callback);
    }
    public static void GetNFTs(string collection, int page, int pageSize, string orderByString, bool desc, List<GetNFTsRequestFilter> filters, Action<CommonResponse<GetNFTsResponse>> callback)
    {
        MirrorWrapper.Instance.GetNFTsByUnabridgedParams(collection, page, pageSize, orderByString,desc, filters,callback);
    }
    public static void GetNFTRealPrice(string price, int fee, Action<CommonResponse<GetNFTRealPriceResponse>> callback)
    {
        MirrorWrapper.Instance.GetNFTRealPrice(price,fee, callback);
    }
    #endregion
}