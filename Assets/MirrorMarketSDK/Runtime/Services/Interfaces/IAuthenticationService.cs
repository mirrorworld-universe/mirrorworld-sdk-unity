using System;
using System.Collections;
using MirrorworldSDK.Models;

namespace MirrorworldSDK.Interfaces
{
    public interface IAuthenticationService
    {
        public void IsLoggedIn(Action<bool> action);

        public void GuestLogin(Action<LoginResponse> action);

        public void LoginWithEmail(string emailAddress, string password, Action<CommonResponse<LoginResponse>> callBack);
        
        //public IEnumerator LoginWithGoogle(LoginWithGoogleRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);
        
        //public IEnumerator Signup(SignupRequest requestBody, Action<CommonResponse<SignupResponse>> callBack);
        
        //public IEnumerator CompleteSignup(CompleteSignupRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);

        public void GetCurrentUserInfo(Action<CommonResponse<UserResponse>> callBack);

        public void GetAccessToken(Action<bool> action);
        
        public void QueryUser(string email, Action<string> callBack);
    }
}