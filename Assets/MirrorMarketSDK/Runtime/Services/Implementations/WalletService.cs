//using System;
//using System.Collections;
//using MirrorworldSDK;
//using MirrorworldSDK.Interfaces;
//using MirrorworldSDK.Models;
//using Newtonsoft.Json;
//using UnityEngine.Networking;

//namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
//{
//    public class WalletService : IWalletService
//    {
//        private readonly string _baseUrlWithVersion;

//        private readonly string _apiKey;
        
//        public WalletService(string apiKey)
//        {
//            _apiKey = apiKey;

//            string baseUrl;

//            baseUrl = Constant.ApiRoot;

//            _baseUrlWithVersion = baseUrl + "v1/";

//        }

//        public IEnumerator TransferSol(TransferSolResponse requestBody, string accessToken, Action<CommonResponse<TransferSolResponse>> callBack)
//        {
//            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

//            string endpoint = _baseUrlWithVersion + "wallet/transfer-sol";
            
//            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
//            Utils.SetContentTypeHeader(request);
//            Utils.SetAcceptHeader(request);
//            Utils.SetApiKeyHeader(request, _apiKey);
//            Utils.SetAuthorizationHeader(request, accessToken);

//            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//            request.downloadHandler = new DownloadHandlerBuffer();
        
//            yield return request.SendWebRequest();
        
//            string rawResponseBody = request.downloadHandler.text;

//            CommonResponse<TransferSolResponse> responseBody;

//            if (request.result != UnityWebRequest.Result.Success)
//            {
//                responseBody = Utils.CustomErrorResponse<TransferSolResponse>(request.responseCode, request.error, rawResponseBody);
//            }
//            else
//            {
//                responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferSolResponse>>(rawResponseBody);
//                responseBody.HttpStatusCode = request.responseCode;
                
//            }

//            callBack(responseBody);
//        }

//        public IEnumerator TransferToken(TransferTokenRequest requestBody, string accessToken, Action<CommonResponse<TransferTokenResponse>> callBack)
//        {
//            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

//            string endpoint = _baseUrlWithVersion + "wallet/transfer-token";
            
//            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
//            Utils.SetContentTypeHeader(request);
//            Utils.SetAcceptHeader(request);
//            Utils.SetApiKeyHeader(request, _apiKey);
//            Utils.SetAuthorizationHeader(request, accessToken);

//            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
//            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
//            request.downloadHandler = new DownloadHandlerBuffer();
        
//            yield return request.SendWebRequest();
        
//            string rawResponseBody = request.downloadHandler.text;

//            CommonResponse<TransferTokenResponse> responseBody;

//            if (request.result != UnityWebRequest.Result.Success)
//            {
//                responseBody = Utils.CustomErrorResponse<TransferTokenResponse>(request.responseCode, request.error, rawResponseBody);
//            }
//            else
//            {
//                responseBody = JsonConvert.DeserializeObject<CommonResponse<TransferTokenResponse>>(rawResponseBody);
//                responseBody.HttpStatusCode = request.responseCode;
                
//            }

//            callBack(responseBody);
//        }

//        public IEnumerator GetWalletTokens(string accessToken, Action<CommonResponse<WalletTokenResponse>> callBack)
//        {
//            string endpoint = _baseUrlWithVersion + "wallet/tokens";
            
//            UnityWebRequest request = new UnityWebRequest(endpoint, "GET");
            
//            Utils.SetContentTypeHeader(request);
//            Utils.SetAcceptHeader(request);
//            Utils.SetApiKeyHeader(request, _apiKey);
//            Utils.SetAuthorizationHeader(request, accessToken);

//            request.downloadHandler = new DownloadHandlerBuffer();
        
//            yield return request.SendWebRequest();
        
//            string rawResponseBody = request.downloadHandler.text;

//            CommonResponse<WalletTokenResponse> responseBody;

//            if (request.result != UnityWebRequest.Result.Success)
//            {
//                responseBody = Utils.CustomErrorResponse<WalletTokenResponse>(request.responseCode, request.error, rawResponseBody);
//            }
//            else
//            {
//                responseBody = JsonConvert.DeserializeObject<CommonResponse<WalletTokenResponse>>(rawResponseBody);
//                responseBody.HttpStatusCode = request.responseCode;
                
//            }

//            callBack(responseBody);
//        }
//    }
//}