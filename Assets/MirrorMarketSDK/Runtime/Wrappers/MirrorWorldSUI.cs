using UnityEngine;
using System.Collections;
using System;
using MirrorworldSDK.Models;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;
using MirrorWorldResponses;
using MirrorworldSDK;

namespace MirrorWorld
{
    public class MirrorWorldSUI
    {
        public SUIWallet Wallet = new SUIWallet();
        
    }

    public class SUIWallet
    {
        public void GetTransactionByDigest(string digest, Action<CommonResponse<SUIResGetTransactionByDigest>> action)
        {
            MWSUIWrapper.GetTransactionsByDigest(digest, action);
        }

        public void GetTokens(Action<CommonResponse<SUIResGetTokens>> action)
        {
            MWSUIWrapper.GetTokens(action);
        }

        public void TransferSUI(string to_publickey, int amount, Action approveFinished, Action<CommonResponse<SUIResTransferSUI>> callBack)
        {
            MWSUIWrapper.TransferSUI(to_publickey, amount, approveFinished, callBack);
        }

        public void TransferToken(string to_publickey, int amount, string token, Action approveFinished, Action<CommonResponse<SUIResTransferToken>> callBack)
        {
            MWSUIWrapper.TransferToken(to_publickey, amount, token, approveFinished, callBack);
        }
    }
}
