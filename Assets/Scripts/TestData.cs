//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Numerics;
//using MirrorWorld;
//using UnityEngine;

//public class TestData : MonoBehaviour
//{
//    // Start is called before the first frame update
//    void Start()
//    {
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }

//    void SpinGacha(Action<string[]> action)
//    {
//        var web3 = new Web3("Your RPC Provider with goerli network");
//        var nonce = web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(walletAddress, BlockParameter.CreatePending()).Result.Value;
//        string nonceHex = "0x" + nonce.ToByteArray().ToHex();
//        string gasPrice = "0x" + web3.Eth.GasPrice.SendRequestAsync().Result.Value.ToString("x");
//        string gasLimit = "0x" + new BigInteger(1500000).ToString("x");
//        string to = "0xD19510F2338831f8912AE760179cF70D95D791F2";
//        string value = "0x00";
//        string data = "0xa5b6ea8f0000000000000000000000000000000000000000000000000000000000000000";
//        MWSDK.Ethereum.Wallet.SignTransactionAndSend(nonceHex, gasPrice, gasLimit, to, value, data, async (response) => {
//            Debug.Log(response);
//        });
//    }
//}
