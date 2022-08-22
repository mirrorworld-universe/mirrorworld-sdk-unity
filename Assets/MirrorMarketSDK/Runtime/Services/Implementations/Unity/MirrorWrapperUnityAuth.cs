#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using System.Collections;
using MirrorworldSDK;
using MirrorworldSDK.Interfaces;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper : IAuthenticationService
    {
        private readonly string urlLoginWithEmail = Constant.ApiRoot + "auth/login";
        private readonly string urlRefreshToken = Constant.UserRoot + "auth/refresh-token";

        public void GetCurrentUserInfo(string accessToken, Action<CommonResponse<UserResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack)
        {
            BasicEmailLoginRequest requestBody = new BasicEmailLoginRequest();
            requestBody.Email = emailAddress;
            requestBody.Password = password;
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            monoBehaviour.StartCoroutine(Post(urlLoginWithEmail, rawRequestBody, (rawResponseBody) =>
            {
                LogFlow("rawResponseBody:" + rawResponseBody);

                CommonResponse<LoginResponse> responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                saveKeyParams(responseBody.Data.AccessToken, responseBody.Data.RefreshToken, responseBody.Data.UserResponse);

                callBack(responseBody);
            }));
        }

        public void QueryUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            throw new NotImplementedException();
        }

        public void RefreshToken(string refreshToken, Action<CommonResponse<LoginResponse>> callBack)
        {
            throw new NotImplementedException();
        }
    }
}
#endif