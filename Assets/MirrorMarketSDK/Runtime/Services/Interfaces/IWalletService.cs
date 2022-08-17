using System;
using System.Collections;
using mirrorworld_sdk_unity.Runtime.Models.Request.Wallet;
using mirrorworld_sdk_unity.Runtime.Models.Response;
using mirrorworld_sdk_unity.Runtime.Models.Response.Wallet;

namespace mirrorworld_sdk_unity.Runtime.Services.Interfaces
{
    public interface IWalletService
    {
        public IEnumerator TransferSol(TransferSolResponse requestBody, string accessToken, Action<CommonResponse<TransferSolResponse>> callBack);
        
        public IEnumerator TransferToken(TransferTokenRequest requestBody, string accessToken, Action<CommonResponse<TransferTokenResponse>> callBack);
        
        public IEnumerator GetWalletTokens(string accessToken, Action<CommonResponse<WalletTokenResponse>> callBack);
    }
}