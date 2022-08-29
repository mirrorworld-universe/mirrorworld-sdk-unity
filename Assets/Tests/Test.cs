using System;
using System.Collections;
using MirrorworldSDK;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestUnitClass
    {
        [UnityTest]
        public IEnumerator TestGetWalletAssets()
        {
            //Include: Login/GetWalletTokens()/GetWalletTransactions()

            MonoBehaviour mono = InitMirror2();
            string email = "squall19871987@163.com";
            string password = "yuebaobao";
            int i = 0;
            MirrorSDK.LoginWithEmail(email,password,(loginRes)=> {
                i++;
                MirrorSDK.GetWalletTokens((tokenRes)=> {
                    Debug.Log("GetWalletTokens success!");
                    i++;
                });
                decimal number = 1;
                string nextBefore = "nextBefore";
                MirrorSDK.GetWalletTransactions(number,nextBefore,(tokenRes) => {
                    Debug.Log("GetWalletTransactions success!");
                    i++;
                });
            });

            while (i != 3)
            {
                yield return null;
            }
        }

        //[Test]
        //public void TransferSolAndGetThisTransaction()
        //{
        //    //Need to airpot SOL first.
        //    //Include: Login/TransferSOLToAnotherAddress()/GetWalletTransactionBySignatrue()

        //}

        [Test]
        public void TransferNFTToAnotherSolWallet()
        {
            //Include: GenerateAnNFTFLow/TransferNFTToAnother
            InitMirror();
            string email = "squall19871987@163.com";
            string password = "yuebaobao";
            MirrorSDK.LoginWithEmail(email, password, (loginRes) => {
                string name = "UnitySDKTestTopCollection";
                string symbol = "UnitySDK";
                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

                MirrorSDK.CreateVerifiedCollection(name, symbol, url, (topRes) =>
                {
                    string parentCollection = topRes.Data.MintAddress;
                    string subName = "SubUnitySDKCollection";

                    MirrorSDK.CreateVerifiedSubCollection(parentCollection,subName, symbol,url,(subRes)=> {
                        string subCollection = subRes.Data.MintAddress;
                        string nftName = "UnitySDKTestNFT";

                        MirrorSDK.MintNFT(subCollection, nftName, symbol, url, (nftRes) => {
                            string nftAddress = nftRes.Data.MintAddress;
                            string anotherWallet = "HkGWQxFspfcaHQbbnnwwGrUDGyKFTYmFgSrB6p238Tqz";

                            MirrorSDK.TransferNFT(nftAddress, anotherWallet,(transRes)=> {

                            });
                        });
                    });
                });
            });
        }

        [Test]
        public void ListNFTAndCancelIt()
        {
            //Include: GenerateFlow/List/Update/Cancel
            InitMirror();

            string email = "squall19871987@163.com";
            string password = "yuebaobao";

            MirrorSDK.LoginWithEmail(email, password, (loginRes) => {
                string name = "UnitySDKTestTopCollection2";
                string symbol = "UnitySDK";
                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

                MirrorSDK.CreateVerifiedCollection(name, symbol, url, (topRes) =>
                {
                    string parentCollection = topRes.Data.MintAddress;
                    string subName = "SubUnitySDKCollection2";

                    MirrorSDK.CreateVerifiedSubCollection(parentCollection, subName, symbol, url, (subRes) => {
                        string subCollection = subRes.Data.MintAddress;
                        string nftName = "UnitySDKTestNFT2";

                        MirrorSDK.MintNFT(subCollection, nftName, symbol, url, (nftRes) => {
                            string nftAddress = nftRes.Data.MintAddress;
                            decimal price = (decimal)1.1f;

                            MirrorSDK.ListNFT(nftAddress, price, (listRes) => {
                                decimal newPrice = (decimal)1.2f;

                                MirrorSDK.UpdateNFTListing(nftAddress, newPrice, (updateRes) => {

                                    MirrorSDK.CancelNFTListing(nftAddress, newPrice, (cancelRes) => {
                                        
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }

        [Test]
        public void BuyTest()
        {
            //Include: Login/GenerateFlow/FetchByMint/ListNFT/Login/Buy

        }

        private void InitMirror()
        {
            GameObject mirrorObj = new GameObject("MirrorSDK", typeof(MirrorSDK));
            string apiKey = "WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ";
            bool debugMode = true;
            MirrorworldSDK.MirrorEnv environment = MirrorworldSDK.MirrorEnv.StagingDevnet;

            MirrorSDK.InitSDK(apiKey, mirrorObj, debugMode, environment);
        }

        private MonoBehaviour InitMirror2()
        {
            GameObject mirrorObj = new GameObject("MirrorSDK", typeof(MirrorSDK));
            string apiKey = "WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ";
            bool debugMode = true;
            MirrorEnv environment = MirrorEnv.StagingDevnet;

            MirrorSDK.InitSDK(apiKey, mirrorObj, debugMode, environment);
            return mirrorObj.GetComponent<MonoBehaviour>();
        }
    }
}
