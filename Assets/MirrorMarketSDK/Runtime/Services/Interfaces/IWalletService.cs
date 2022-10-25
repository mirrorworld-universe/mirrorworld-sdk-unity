using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IWalletService
    {
        public void GetWalletTokens(Action<CommonResponse<WalletTokenResponse>> action);

        public void GetWalletTransactions(float number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action);

        public void GetWalletTransactionsBySignatrue(string signature, Action<CommonResponse<TransferTokenResponse>> action);

        public void TransferSol(ulong amout, string publicKey,string confirmation, Action<CommonResponse<TransferSolResponse>> callBack);
        
        public void TransferSPLToken(ulong amout, string publicKey, Action<CommonResponse<TransferTokenResponse>> callBack);
        
    }
}