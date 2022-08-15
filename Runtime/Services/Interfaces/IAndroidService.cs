using System;

namespace mirrorworld_sdk_unity.Runtime.Services.Interfaces
{
    public interface IAndroidService
    {
        public void InitSDKWithParams(string apiKey, bool isDebug);

        public void SetAPIKey(string key);

        public void SetDebugMode(bool useDebug);

        public void StartLogin();

        public void StartLoginWithCallback(Action<string> callback);

        public void GetWalletAddress(Action<string> callback);
    }
}