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

        public IEnumerator LoginWithEmail(BasicEmailLogin requestBody, Action<CommonResponse<LoginResponse>> callBack)
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
    }
}
