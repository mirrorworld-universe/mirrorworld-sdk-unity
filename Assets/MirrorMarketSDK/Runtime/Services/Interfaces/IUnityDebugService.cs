using System;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IUnityDebugService
    {
        public void GetLoginSession(string emailAddress, Action<bool> action);

        public void CompleteLoginWithSession(string session, Action<CommonResponse<LoginResponse>> action);
    }
}