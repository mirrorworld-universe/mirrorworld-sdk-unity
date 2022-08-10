using System;
using System.Collections;
using mirrorworld_sdk_unity.Runtime.Models.Request.Authentication;
using mirrorworld_sdk_unity.Runtime.Models.Response;
using mirrorworld_sdk_unity.Runtime.Models.Response.Authentication;
using mirrorworld_sdk_unity.Runtime.Services.Interfaces;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly Environment _environment;

        private readonly EnvironmentVersion _environmentVersion;

        private readonly string _baseUrlWithVersion;

        private readonly string _apiKey;
        
        public AuthenticationService(Environment environment, EnvironmentVersion environmentVersion, string apiKey)
        {
            _environment = environment;
            _environmentVersion = environmentVersion;
            _apiKey = apiKey;

            string baseUrl;

            baseUrl = environment == Environment.Staging ? Constant.StagingV1ApiBaseUrl : Constant.ProductionV1ApiBaseUrl;

            _baseUrlWithVersion = baseUrl + "v1/";

        }

        public IEnumerator LoginWithEmail(BasicEmailLoginRequest requestBody, Action<CommonResponse<LoginResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "auth/login";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator LoginWithGoogle(LoginWithGoogleRequest requestBody, Action<CommonResponse<LoginResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "auth/google";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator Signup(SignupRequest requestBody, Action<CommonResponse<SignupResponse>> callBack)
        {
            
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "auth/signup";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<SignupResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<SignupResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<SignupResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator CompleteSignup(CompleteSignupRequest requestBody, Action<CommonResponse<LoginResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "auth/complete-signup";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }
        
        public IEnumerator GetCurrentUserInfo(string accessToken, Action<CommonResponse<UserResponse>> callBack)
        {
            string endpoint = _baseUrlWithVersion + "auth/me";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "GET");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);
            
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<UserResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<UserResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator RefreshToken(string refreshToken, Action<CommonResponse<LoginResponse>> callBack)
        {
            string endpoint = _baseUrlWithVersion + "auth/refresh-token";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "GET");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            request.SetRequestHeader("x-refresh-token", refreshToken);
            
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<LoginResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<LoginResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<LoginResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator QueryUser(string email, Action<CommonResponse<UserResponse>> callBack)
        {
            
            string endpoint = _baseUrlWithVersion + "auth/user?email=" + email;
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "GET");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<UserResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<UserResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<UserResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }
    }
}
