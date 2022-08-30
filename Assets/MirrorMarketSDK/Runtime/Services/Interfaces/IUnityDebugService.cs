using System;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IUnityDebugService
    {
        public void GetLoginSession(Action<CommonResponse<GetLoginSessionResponse>> action);

        public void CompleteLoginWithSession(Action<CommonResponse<UserResponse>> action);
    }
}