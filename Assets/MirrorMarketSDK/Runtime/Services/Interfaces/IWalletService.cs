using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IWalletService
    {
        public IEnumerator TransferSol(TransferSolResponse requestBody, string accessToken, Action<CommonResponse<TransferSolResponse>> callBack);
        
        public IEnumerator TransferToken(TransferTokenRequest requestBody, string accessToken, Action<CommonResponse<TransferTokenResponse>> callBack);
        
        public IEnumerator GetWalletTokens(string accessToken, Action<CommonResponse<WalletTokenResponse>> callBack);
    }
}