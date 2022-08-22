using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IAuthenticationService
    {
        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack);
        
        //public IEnumerator LoginWithGoogle(LoginWithGoogleRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);
        
        //public IEnumerator Signup(SignupRequest requestBody, Action<CommonResponse<SignupResponse>> callBack);
        
        //public IEnumerator CompleteSignup(CompleteSignupRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);

        public void GetCurrentUserInfo(string accessToken, Action<CommonResponse<UserResponse>> callBack);

        public void RefreshToken(string refreshToken, Action<CommonResponse<LoginResponse>> callBack);
        
        public void QueryUser(string email, Action<CommonResponse<UserResponse>> callBack);
    }
}