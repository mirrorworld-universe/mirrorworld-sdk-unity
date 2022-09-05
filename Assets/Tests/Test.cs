using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestUnitClass
    {
        //specified params
        private string testEmail = "squall19871987@163.com";
        private string testPw = "yuebaobao";
        private string anotherWallet = "HkGWQxFspfcaHQbbnnwwGrUDGyKFTYmFgSrB6p238Tqz";

        //logic
        private GameObject sdkObject;


        [UnityTest]
        public IEnumerator TestGetWalletAssets()
        {
            //Include: Login/GetWalletTokens()/GetWalletTransactions()

            InitMirror();
            int i = 0;
            MirrorSDK.LoginWithEmail(testEmail,testPw,(loginRes)=> {
                TestLog("LoginWithEmail success!");
                i++;
                MirrorSDK.GetWalletTokens((tokenRes)=> {
                    TestLog("GetWalletTokens success!");
                    i++;
                });
                decimal number = 1;
                string nextBefore = "nextBefore";
                MirrorSDK.GetWalletTransactions(number,nextBefore,(tokenRes) => {
                    TestLog("GetWalletTransactions success!");
                    i++;
                });
            });

            while (i != 3)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator GetTransaction()
        {
            //Need to airpot SOL first.
            //Include: Login/TransferSOLToAnotherAddress()/GetWalletTransactionBySignatrue()
            string signature = "4sExqYsdXZPKnGsjGWsS3y8yhGRc3BDHBo93oLHR6SCTFDgdvw2Lgj52efXmSQyam2zwhxGS6C9k5EHV449VvoQS";
            InitMirror();
            int i = 0;
            MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {

                TestLog("LoginWithEmail success!");
                ulong amount = (ulong)10;
                i++;


                MirrorSDK.GetWalletTransactionsBySignatrue(signature, (getTransRes) => {
                    TestLog("GetWalletTransactionsBySignatrue success!");
                    i++;
                });
            });

            while (i != 2)
            {
                yield return null;
            }
        }


        [UnityTest]
        public IEnumerator TransferSolAndGetThisTransaction()
        {
            //Need to airpot SOL first.
            //Include: Login/TransferSOLToAnotherAddress()/GetWalletTransactionBySignatrue()
            string signature = null ;
            InitMirror();
            int i = 0;
            MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {

                TestLog("LoginWithEmail success!");
                ulong amount = (ulong)10;
                i++;

                MirrorSDK.TransferSol(amount, anotherWallet,Confirmation.Finalized,(transRes)=> {
               
                    TestLog("TransferSol success!");
                     signature = transRes.Data.TxSignature;
                    i++;
                });
            });

            while (i != 2)
            {
                yield return null;
            }

            yield return new WaitForSeconds(10);

            MirrorSDK.GetWalletTransactionsBySignatrue(signature, (getTransRes) => {
                TestLog("GetWalletTransactionsBySignatrue success!");
                i++;
            });
            while (i != 3)
            {
                yield return null;
            }
        }

        [UnityTest]
        public IEnumerator TransferNFTToAnotherSolWallet()
        {
            //Include: GenerateAnNFTFLow/TransferNFTToAnother
            InitMirror();
            string email = "squall19871987@163.com";
            string password = "yuebaobao";
            int i = 5;
            MirrorSDK.LoginWithEmail(email, password, (loginRes) => {
                TestLog("LoginWithEmail success!");
                i--;
                string name = "UnitySDKTestTopCollection";
                string symbol = "UnitySDK";
                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

                MirrorSDK.CreateVerifiedCollection(name, symbol, url,Confirmation.Finalized, (topRes) =>
                {
                    TestLog("CreateVerifiedCollection success!");
                    i--;
                    string parentCollection = topRes.Data.MintAddress;
                    string subName = "SubUnitySDKCollection";

                    MirrorSDK.CreateVerifiedSubCollection(parentCollection,subName, symbol,url, Confirmation.Finalized, (subRes)=> {
                        TestLog("CreateVerifiedSubCollection success!");
                        i--;
                        if(subRes.Code != (long)MirrorResponseCode.Success)
                        {
                            TestLog(subRes.Error);
                        }
                        string subCollection = subRes.Data.MintAddress;
                        string nftName = "UnitySDKTestNFT";

                        MirrorSDK.MintNFT(subCollection, nftName, symbol, url, Confirmation.Finalized, (nftRes) => {
                            TestLog("MintNFT success!");
                            i--;
                            string nftAddress = nftRes.Data.MintAddress;

                            MirrorSDK.TransferNFT(nftAddress, anotherWallet,(transRes)=> {
                                TestLog("TransferNFT success!");
                                i--;

                            });
                        });
                    });
                });
            });

            while (i != 0)
            {
                yield return null;
            }
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

                MirrorSDK.CreateVerifiedCollection(name, symbol, url, Confirmation.Finalized, (topRes) =>
                {
                    string parentCollection = topRes.Data.MintAddress;
                    string subName = "SubUnitySDKCollection2";

                    MirrorSDK.CreateVerifiedSubCollection(parentCollection, subName, symbol, url, Confirmation.Finalized, (subRes) => {
                        string subCollection = subRes.Data.MintAddress;
                        string nftName = "UnitySDKTestNFT2";

                        MirrorSDK.MintNFT(subCollection, nftName, symbol, url, Confirmation.Finalized, (nftRes) => {
                            string nftAddress = nftRes.Data.MintAddress;
                            decimal price = (decimal)1.1f;

                            MirrorSDK.ListNFT(nftAddress, price, Confirmation.Finalized, (listRes) => {
                                decimal newPrice = (decimal)1.2f;

                                MirrorSDK.UpdateNFTListing(nftAddress, newPrice, Confirmation.Finalized, (updateRes) => {

                                    MirrorSDK.CancelNFTListing(nftAddress, newPrice, Confirmation.Finalized, (cancelRes) => {
                                        
                                    });
                                });
                            });
                        });
                    });
                });
            });
        }

        [UnityTest]
        public IEnumerator FetchNFTByAllKind()
        {
            //Include: Login/GenerateFlow/FetchByMint/FetchByAuth/FecthByCreator/FetchByOwner
            InitMirror();

            int i = 0;
            MirrorSDK.LoginWithEmail(testEmail, testPw, (loginRes) => {
                string name = "UnitySDKTestTopCollection3";
                string symbol = "UnitySDK";
                string url = "https://mirror-nft.s3.us-west-2.amazonaws.com/assets/111.json";

                i++;
                MirrorSDK.CreateVerifiedCollection(name, symbol, url, Confirmation.Finalized, (topRes) =>
                {
                    TestLog("CreateVerifiedCollection success!");
                    string parentCollection = topRes.Data.MintAddress;
                    string subName = "SubUnitySDKCollection3";

                    i++;
                    MirrorSDK.CreateVerifiedSubCollection(parentCollection, subName, symbol, url, Confirmation.Finalized, (subRes) => {
                        TestLog("CreateVerifiedSubCollection success!");
                        string subCollection = subRes.Data.MintAddress;
                        string nftName = "UnitySDKTestNFT3";

                        i++;
                        MirrorSDK.MintNFT(subCollection, nftName, symbol, url, Confirmation.Finalized, (nftRes) => {
                            TestLog("MintNFT success!");
                            string nftAddress = nftRes.Data.MintAddress;
                            string nftCreator = nftRes.Data.CreatorAddress;
                            string nftAuthAddress = nftRes.Data.UpdateAuthority;

                            List<string> mintAddressList = new List<string>();
                            mintAddressList.Add(nftAddress);

                            MirrorSDK.FetchNFTsByMintAddress(mintAddressList, (fetchRes) => {
                                TestLog("FetchNFTsByMintAddress result "+fetchRes.Code);
                                if (fetchRes.Code != (long)MirrorResponseCode.Success)
                                {
                                    LogAssert.Expect(LogType.Assert,"FetchNFTsByMintAddress failed!");
                                }
                                i++;
                            });

                            MirrorSDK.GetNFTDetails(nftAddress,(fetchRes)=> {
                                TestLog("GetNFTDetails result " + fetchRes.Code);
                                if (fetchRes.Code != (long)MirrorResponseCode.Success)
                                {
                                    LogAssert.Expect(LogType.Assert, "GetNFTDetails failed!");
                                }
                                i++;
                            });


                            MirrorSDK.GetActivityOfSingleNFT(subCollection, (fetchRes) =>{
                                TestLog("GetActivityOfSingleNFT result " + fetchRes.Code);
                                if (fetchRes.Code != (long)MirrorResponseCode.Success)
                                {
                                    LogAssert.Expect(LogType.Assert, "GetActivityOfSingleNFT failed!");
                                }
                                i++;
                            });

                            //Not implemented on devnet
                            //List<string> creatorList = new List<string>();
                            //creatorList.Add(nftCreator);

                            //MirrorSDK.FetchNFTsByCreatorAddresses(creatorList, (fetchRes) => {
                            //    TestLog("FetchNFTsByCreatorAddresses result " + fetchRes.Code);
                            //    if (fetchRes.Code != (long)MirrorResponseCode.Success)
                            //    {
                            //        LogAssert.Expect(LogType.Assert, "FetchNFTsByCreatorAddresses failed!");
                            //    }
                            //    i++;
                            //});

                            //Not implemented on devnet
                            //List<string> authList = new List<string>();
                            //authList.Add(nftAuthAddress);

                            //MirrorSDK.FetchNFTsByUpdateAuthorities(authList, (fetchRes) => {
                            //    if (fetchRes.Code != (long)MirrorResponseCode.Success)
                            //    {
                            //        LogAssert.Expect(LogType.Assert, "FetchNFTsByUpdateAuthorities failed!");
                            //    }
                            //    i++;
                            //});
                        });
                    });
                });
            });
            while (i != 6)
            {
                yield return null;
            }
        }

        private void InitMirror()
        {
            if(sdkObject == null)
            {
                sdkObject = new GameObject("MirrorSDK", typeof(MirrorSDK));
            }

            string apiKey = "WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ";

            bool debugMode = true;

            MirrorEnv environment = MirrorEnv.Staging;

            MirrorSDK.InitSDK(apiKey, sdkObject, debugMode, environment);
        }

        private void TestLog(string content)
        {
            Debug.Log("MirrorSDKUnitTest:" + content);
        }
    }
}
