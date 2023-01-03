//using System;
//using System.Collections;
//using System.Collections.Generic;
//using MirrorworldSDK;
//using NUnit.Framework;
//using UnityEngine;
//using UnityEngine.TestTools;

//namespace Tests
//{
//    public class TestUnitClass
//    {
//        //specified params
//        private string testEmail = "squall19871987@163.com";
//        private string testPw = "yuebaobao";
//        private string anotherWallet = "HkGWQxFspfcaHQbbnnwwGrUDGyKFTYmFgSrB6p238Tqz";

//        //logic
//        private GameObject sdkObject;


//        [UnityTest]
//        public IEnumerator TestGetWalletAssets()
//        {
//            //Include: Login/GetWalletTokens()/GetWalletTransactions()

//            InitMirror();
//            int i = 0;
//            MirrorSDK.LoginWithEmail(testEmail,testPw,(loginRes)=> {
//                TestLog("LoginWithEmail success!");
//                i++;
//                MirrorSDK.GetTokens((tokenRes)=> {
//                    TestLog("GetWalletTokens success!");
//                    i++;
//                });
//                float number = 1;
//                string nextBefore = "nextBefore";
//                MirrorSDK.GetTransactions(number,nextBefore,(tokenRes) => {
//                    TestLog("GetWalletTransactions success!");
//                    i++;
//                });
//            });

//            while (i != 3)
//            {
//                yield return null;
//            }
//        }

//        [UnityTest]
//        public IEnumerator GetTransactionBySignature()
//        {
//            //Need to airpot SOL first.
//            //Include: Login/TransferSOLToAnotherAddress()/GetWalletTransactionBySignatrue()

//            InitMirror();
//            int i = 0;
//            MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {

//                TestLog("LoginWithEmail success!");
//                i++;

//                string signature = "4sExqYsdXZPKnGsjGWsS3y8yhGRc3BDHBo93oLHR6SCTFDgdvw2Lgj52efXmSQyam2zwhxGS6C9k5EHV449VvoQS";
//                MirrorSDK.GetWalletTransactionsBySignatrue(signature, (getTransRes) =>
//                {
//                    TestLog("GetWalletTransactionsBySignatrue success!");
//                    i++;
//                });
//            });

//            while (i != 2)
//            {
//                yield return null;
//            }
//        }

//        [UnityTest]
//        public IEnumerator TransferSolAndGetThisTransaction()
//        {
//            //Need to airpot SOL first.
//            //Include: Login/TransferSOLToAnotherAddress()/GetWalletTransactionBySignatrue()

//            InitMirror();
//            int i = 0;
//            MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {

//                TestLog("LoginWithEmail success!");
//                ulong amount = (ulong)10;
//                i++;

//                MirrorSDK.TransferSol(amount, anotherWallet,Confirmation.Finalized,(transRes)=> {
               
//                    TestLog("TransferSol success!");
//                    string signature = transRes.data.tx_signature;
//                    i++;

//                    //MirrorSDK.GetWalletTransactionsBySignatrue(signature,(getTransRes)=> {
//                    //    TestLog("GetWalletTransactionsBySignatrue success!");
//                    //    i++;
//                    //});
//                });
//            });

//            while (i != 2)
//            {
//                yield return null;
//            }

//        }

//        [UnityTest]
//        public IEnumerator TransferNFTToAnotherSolWallet()
//        {
//            //Include: GenerateAnNFTFLow/TransferNFTToAnother
//            InitMirror();
//            string email = "squall19871987@163.com";
//            string password = "yuebaobao";
//            int i = 5;
//            MirrorSDK.LoginWithEmail(email, password, (loginRes) => {
//                TestLog("LoginWithEmail success!");
//                i--;
//                string name = "UnitySDKTestTopCollection";
//                string symbol = "UnitySDK";
//                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

//                MirrorSDK.CreateVerifiedCollection(name, symbol, url,200,Confirmation.Finalized, (topRes) =>
//                {
//                    TestLog("CreateVerifiedCollection success!");
//                    i--;
//                    string parentCollection = topRes.data.mint_address;

//                    MirrorSDK.MintNFT(parentCollection, "nft name", symbol, url, Confirmation.Finalized, "testid", (nftRes) => {
//                        TestLog("MintNFT success!");
//                        i--;
//                        string nftAddress = nftRes.data.mint_address;

//                        MirrorSDK.TransferNFT(nftAddress, anotherWallet, (transRes) => {
//                            TestLog("TransferNFT success!");
//                            i--;

//                        });
//                    });
//                });
//            });

//            while (i != 0)
//            {
//                yield return null;
//            }
//        }

//        [Test]
//        public void ListNFTAndCancelIt()
//        {
//            //Include: GenerateFlow/List/Update/Cancel
//            InitMirror();

//            string email = "squall19871987@163.com";
//            string password = "yuebaobao";

//            MirrorSDK.LoginWithEmail(email, password, (loginRes) => {
//                string name = "UnitySDKTestTopCollection2";
//                string symbol = "UnitySDK";
//                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

//                MirrorSDK.CreateVerifiedCollection(name, symbol, url, 200,Confirmation.Finalized, (topRes) =>
//                {
//                    string parentCollection = topRes.data.mint_address;
//                    string subName = "SubUnitySDKCollection2";

//                    MirrorSDK.MintNFT(parentCollection, subName, symbol, url, Confirmation.Finalized, "testid", (nftRes) => {
//                        string nftAddress = nftRes.data.mint_address;
//                        float price = (float)1.1f;

//                        MirrorSDK.ListNFT(nftAddress, price, Confirmation.Finalized, (listRes) => {
//                            float newPrice = (float)1.2f;

//                            MirrorSDK.UpdateNFTListing(nftAddress, newPrice, Confirmation.Finalized, (updateRes) => {

//                                MirrorSDK.CancelNFTListing(nftAddress, newPrice, Confirmation.Finalized, (cancelRes) => {

//                                });
//                            });
//                        });
//                    });
//                });
//            });
//        }

//        //[UnityTest]
//        //public IEnumerator FetchNFTByAllKind()
//        //{
//        //    //Include: Login/GenerateFlow/FetchByMint/FetchByAuth/FecthByCreator/FetchByOwner
//        //    InitMirror();

//        //    int i = 0;
//        //    MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {
//        //        string name = "UnitySDKTestTopCollection3";
//        //        string symbol = "UnitySDK";
//        //        string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

//        //        i++;
//        //        MirrorSDK.CreateVerifiedCollection(name, symbol, url, Confirmation.Finalized, (topRes) =>
//        //        {
//        //            TestLog("CreateVerifiedCollection success!");
//        //            string parentCollection = topRes.data.mint_address;
//        //            string subName = "SubUnitySDKCollection3";

//        //            i++;
//        //            MirrorSDK.CreateVerifiedSubCollection(parentCollection, subName, symbol, url, Confirmation.Finalized, (subRes) => {
//        //                TestLog("CreateVerifiedSubCollection success!");
//        //                string subCollection = subRes.data.mint_address;
//        //                string nftName = "UnitySDKTestNFT3";

//        //                i++;
//        //                MirrorSDK.MintNFT(subCollection, nftName, symbol, url, Confirmation.Finalized, "testid", (nftRes) => {
//        //                    TestLog("MintNFT success!");
//        //                    string nftAddress = nftRes.data.mint_address;
//        //                    string nftCreator = nftRes.data.creator_address;
//        //                    string nftAuthAddress = nftRes.data.update_authority;

//        //                    List<string> mintAddressList = new List<string>();
//        //                    mintAddressList.Add(nftAddress);

//        //                    MirrorSDK.FetchNFTsByMintAddress(mintAddressList, (fetchRes) => {
//        //                        TestLog("FetchNFTsByMintAddress result "+fetchRes.code);
//        //                        if (fetchRes.code != (long)MirrorResponseCode.Success)
//        //                        {
//        //                            LogAssert.Expect(LogType.Assert,"FetchNFTsByMintAddress failed!");
//        //                        }
//        //                        i++;
//        //                    });

//        //                    MirrorSDK.GetNFTDetails(nftAddress,(fetchRes)=> {
//        //                        TestLog("GetNFTDetails result " + fetchRes.code);
//        //                        if (fetchRes.code != (long)MirrorResponseCode.Success)
//        //                        {
//        //                            LogAssert.Expect(LogType.Assert, "GetNFTDetails failed!");
//        //                        }
//        //                        i++;
//        //                    });


//        //                    MirrorSDK.GetActivityOfSingleNFT(subCollection, (fetchRes) =>{
//        //                        TestLog("GetActivityOfSingleNFT result " + fetchRes.code);
//        //                        if (fetchRes.code != (long)MirrorResponseCode.Success)
//        //                        {
//        //                            LogAssert.Expect(LogType.Assert, "GetActivityOfSingleNFT failed!");
//        //                        }
//        //                        i++;
//        //                    });

//        //                    //Not implemented on devnet
//        //                    //List<string> creatorList = new List<string>();
//        //                    //creatorList.Add(nftCreator);

//        //                    //MirrorSDK.FetchNFTsByCreatorAddresses(creatorList, (fetchRes) => {
//        //                    //    TestLog("FetchNFTsByCreatorAddresses result " + fetchRes.Code);
//        //                    //    if (fetchRes.Code != (long)MirrorResponseCode.Success)
//        //                    //    {
//        //                    //        LogAssert.Expect(LogType.Assert, "FetchNFTsByCreatorAddresses failed!");
//        //                    //    }
//        //                    //    i++;
//        //                    //});

//        //                    //Not implemented on devnet
//        //                    //List<string> authList = new List<string>();
//        //                    //authList.Add(nftAuthAddress);

//        //                    //MirrorSDK.FetchNFTsByUpdateAuthorities(authList, (fetchRes) => {
//        //                    //    if (fetchRes.Code != (long)MirrorResponseCode.Success)
//        //                    //    {
//        //                    //        LogAssert.Expect(LogType.Assert, "FetchNFTsByUpdateAuthorities failed!");
//        //                    //    }
//        //                    //    i++;
//        //                    //});
//        //                });
//        //            });
//        //        });
//        //    });
//        //    while (i != 6)
//        //    {
//        //        yield return null;
//        //    }
//        //}

//        private void InitMirror()
//        {
//            if(sdkObject == null)
//            {
//                sdkObject = new GameObject("MirrorSDK", typeof(MirrorSDK));
//            }

//            string apiKey = "WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ";

//            bool debugMode = true;

//            MirrorEnv environment = MirrorEnv.StagingDevNet;

//            MirrorSDK.InitSDK(apiKey, sdkObject, debugMode, environment);
//        }

//        private void TestLog(string content)
//        {
//            Debug.Log("MirrorSDKUnitTest:" + content);
//        }
//    }
//}
