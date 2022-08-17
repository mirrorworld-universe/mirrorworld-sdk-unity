using System;
using System.Collections;
using mirrorworld_sdk_unity.Runtime.Models.Request.Authentication;
using mirrorworld_sdk_unity.Runtime.Models.Response;
using mirrorworld_sdk_unity.Runtime.Models.Response.Authentication;

namespace mirrorworld_sdk_unity.Runtime.Services.Interfaces
{
    public interface IAuthenticationService
    {
        public IEnumerator LoginWithEmail(BasicEmailLogin requestBody, Action<CommonResponse<LoginResponse>> callBack);
        public IEnumerator LoginWithGoogle(LoginWithGoogleRequest requestBody, Action<CommonResponse<LoginResponse>> callBack);
    }
}