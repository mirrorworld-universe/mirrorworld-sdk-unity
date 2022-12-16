using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IWalletService
    {
        public void GetWalletTokens(Action<CommonResponse<WalletTokenResponse>> action);

        public void GetWalletTransactions(double number, string nextBefore, Action<CommonResponse<TransferTokenResponse>> action);

        public void GetWalletTransactionsBySignatrue(string signature, Action<CommonResponse<TransferTokenResponse>> action);

        public void TransferSol(ulong amout, string publicKey,string confirmation, Action<CommonResponse<TransferSolResponse>> callBack);
        
        public void TransferSPLToken(string token_mint, int decimals, ulong amount, string to_publickey, Action<CommonResponse<TransferTokenResponse>> callBack);
        
    }
}