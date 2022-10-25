//using System;
//using System.Collections;
//using MirrorworldSDK.Models;
//
//using UnityEngine.Networking;

//namespace MirrorworldSDK.Implementations
//{
//    public class AuthenticationService
//    {
//        private readonly string urlLoginWithEmail = Constant.ApiRoot + "auth/login";
//        private readonly string urlLoginWithGoogle = Constant.ApiRoot + "auth/google";

//        public IEnumerator LoginWithEmail(string apiKey, BasicEmailLoginRequest requestBody, Action<CommonResponse<LoginResponse>> callBack)
//        {
//            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

//            UnityWebRequest request = new UnityWebRequest(urlLoginWithEmail, "POST");

//            Utils.SetContentTypeHeader(request);
//            Utils.SetAcceptHeader(request);
//            Utils.SetApiKeyHeader(request, apiKey);

//            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//            request.downloadHandler = new DownloadHandlerBuffer();

//            yield return request.SendWebRequest();

//            string rawResponseBody = request.downloadHandler.text;

//            CommonResponse<LoginResponse> responseBody;

//            if (request.result != UnityWebRequest.Result.Success)
//            {
//                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
//            }
//            else
//            {
//                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
//                responseBody.HttpStatusCode = request.responseCode;

//            }

//            callBack(responseBody);
//        }

//        public IEnumerator LoginWithGoogle(string apiKey, LoginWithGoogleRequest requestBody)
//        {
//            var rawRequestBody = JsonConvert.SerializeObject(requestBody);
            
//            UnityWebRequest request = new UnityWebRequest(urlLoginWithGoogle, "POST");
            
//            Utils.SetContentTypeHeader(request);
//            Utils.SetAcceptHeader(request);
//            Utils.SetApiKeyHeader(request, apiKey);

//            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//            request.downloadHandler = new DownloadHandlerBuffer();
        
//            yield return request.SendWebRequest();
        
//            string rawResponseBody = request.downloadHandler.text;

//            CommonResponse<LoginResponse> responseBody;

//            if (request.result != UnityWebRequest.Result.Success)
//            {
//                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
//            }
//            else
//            {
//                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
//                responseBody.HttpStatusCode = request.responseCode;
                
//            }

//            yield return responseBody;
//        }

//        //    public IEnumerator Signup(SignupRequest requestBody, Action<CommonResponse<SignupResponse>> callBack)
//        //    {

//        //        var rawRequestBody = JsonConvert.SerializeObject(requestBody);

//        //        string endpoint = rootUrl + "auth/signup";

//        //        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");

//        //        Utils.SetContentTypeHeader(request);
//        //        Utils.SetAcceptHeader(request);
//        //        Utils.SetApiKeyHeader(request, apiKey);

//        //        byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//        //        request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//        //        request.downloadHandler = new DownloadHandlerBuffer();

//        //        yield return request.SendWebRequest();

//        //        string rawResponseBody = request.downloadHandler.text;

//        //        CommonResponse<SignupResponse> responseBody;

//        //        if (request.result != UnityWebRequest.Result.Success)
//        //        {
//        //            responseBody = Utils.CustomErrorResponse<SignupResponse>(request.responseCode, request.error, rawResponseBody);
//        //        }
//        //        else
//        //        {
//        //            responseBody = JsonConvert.DeserializeObject<CommonResponse<SignupResponse>>(rawResponseBody);
//        //            responseBody.HttpStatusCode = request.responseCode;

//        //        }

//        //        callBack(responseBody);
//        //    }

//        //    public IEnumerator CompleteSignup(CompleteSignupRequest requestBody, Action<CommonResponse<LoginResponse>> callBack)
//        //    {
//        //        var rawRequestBody = JsonConvert.SerializeObject(requestBody);

//        //        string endpoint = rootUrl + "auth/complete-signup";

//        //        UnityWebRequest request = new UnityWebRequest(endpoint, "POST");

//        //        Utils.SetContentTypeHeader(request);
//        //        Utils.SetAcceptHeader(request);
//        //        Utils.SetApiKeyHeader(request, apiKey);

//        //        byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//        //        request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//        //        request.downloadHandler = new DownloadHandlerBuffer();

//        //        yield return request.SendWebRequest();

//        //        string rawResponseBody = request.downloadHandler.text;

//        //        CommonResponse<LoginResponse> responseBody;

//        //        if (request.result != UnityWebRequest.Result.Success)
//        //        {
//        //            responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
//        //        }
//        //        else
//        //        {
//        //            responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
//        //            responseBody.HttpStatusCode = request.responseCode;

//        //        }

//        //        callBack(responseBody);
//        //    }

//        //    public IEnumerator GetCurrentUserInfo(string accessToken, Action<CommonResponse<UserResponse>> callBack)
//        //    {
//        //        string endpoint = rootUrl + "auth/me";

//        //        UnityWebRequest request = new UnityWebRequest(endpoint, "GET");

//        //        Utils.SetContentTypeHeader(request);
//        //        Utils.SetAcceptHeader(request);
//        //        Utils.SetApiKeyHeader(request, apiKey);
//        //        Utils.SetAuthorizationHeader(request, accessToken);

//        //        request.downloadHandler = new DownloadHandlerBuffer();

//        //        yield return request.SendWebRequest();

//        //        string rawResponseBody = request.downloadHandler.text;

//        //        CommonResponse<UserResponse> responseBody;

//        //        if (request.result != UnityWebRequest.Result.Success)
//        //        {
//        //            responseBody = Utils.CustomErrorResponse<UserResponse>(request.responseCode, request.error, rawResponseBody);
//        //        }
//        //        else
//        //        {
//        //            responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(rawResponseBody);
//        //            responseBody.HttpStatusCode = request.responseCode;

//        //        }

//        //        callBack(responseBody);
//        //    }

//        //    public IEnumerator RefreshToken(string refreshToken, Action<CommonResponse<LoginResponse>> callBack)
//        //    {
//        //        string endpoint = rootUrl + "auth/refresh-token";

//        //        UnityWebRequest request = new UnityWebRequest(endpoint, "GET");

//        //        Utils.SetContentTypeHeader(request);
//        //        Utils.SetAcceptHeader(request);
//        //        Utils.SetApiKeyHeader(request, apiKey);
//        //        request.SetRequestHeader("x-refresh-token", refreshToken);

//        //        request.downloadHandler = new DownloadHandlerBuffer();

//        //        yield return request.SendWebRequest();

//        //        string rawResponseBody = request.downloadHandler.text;

//        //        CommonResponse<LoginResponse> responseBody;

//        //        if (request.result != UnityWebRequest.Result.Success)
//        //        {
//        //            responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
//        //        }
//        //        else
//        //        {
//        //            responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
//        //            responseBody.HttpStatusCode = request.responseCode;

//        //        }

//        //        callBack(responseBody);
//        //    }

//        //    public IEnumerator QueryUser(string email, Action<CommonResponse<UserResponse>> callBack)
//        //    {
//        //        string endpoint = rootUrl + "auth/user?email=" + email;

//        //        UnityWebRequest request = new UnityWebRequest(endpoint, "GET");

//        //        Utils.SetContentTypeHeader(request);
//        //        Utils.SetAcceptHeader(request);
//        //        Utils.SetApiKeyHeader(request, apiKey);

//        //        request.downloadHandler = new DownloadHandlerBuffer();

//        //        yield return request.SendWebRequest();

//        //        string rawResponseBody = request.downloadHandler.text;

//        //        CommonResponse<UserResponse> responseBody;

//        //        if (request.result != UnityWebRequest.Result.Success)
//        //        {
//        //            responseBody = Utils.CustomErrorResponse<UserResponse>(request.responseCode, request.error, rawResponseBody);
//        //        }
//        //        else
//        //        {
//        //            responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(rawResponseBody);
//        //            responseBody.HttpStatusCode = request.responseCode;

//        //        }

//        //        callBack(responseBody);
//        //    }
//    }
//}
