using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IAuthenticationService
    {
        public IEnumerator LoginWithEmail(BasicEmailLoginRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);
        
        public IEnumerator LoginWithGoogle(LoginWithGoogleRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);
        
        public IEnumerator Signup(SignupRequest requestBody, Action<CommonResponse<SignupResponse>> callBack);
        
        public IEnumerator CompleteSignup(CompleteSignupRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);

        public IEnumerator GetCurrentUserInfo(string accessToken, Action<CommonResponse<UserResponse>> callBack);

        public IEnumerator RefreshToken(string refreshToken, Action<CommonResponse<LoginResponse>> callBack);
        
        public IEnumerator QueryUser(string email, Action<CommonResponse<UserResponse>> callBack);
    }
}