using System;
using System.Collections;
using mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace;
using mirrorworld_sdk_unity.Runtime.Models.Response;
using mirrorworld_sdk_unity.Runtime.Models.Response.Authentication;
using mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace;
using mirrorworld_sdk_unity.Runtime.Services.Interfaces;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
{
    public class MarketplaceService : IMarketplaceService
    {
        private readonly Environment _environment;

        private readonly EnvironmentVersion _environmentVersion;

        private readonly string _baseUrlWithVersion;

        private readonly string _apiKey;
        
        public MarketplaceService(Environment environment, EnvironmentVersion environmentVersion, string apiKey)
        {
            _environment = environment;
            _environmentVersion = environmentVersion;
            _apiKey = apiKey;

            string baseUrl;

            baseUrl = environment == Environment.Staging ? Constant.StagingV1ApiBaseUrl : Constant.ProductionV1ApiBaseUrl;

            _baseUrlWithVersion = baseUrl + "v1/";

        }

        public IEnumerator CreateCollection(CreateCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/mint/collection";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MintResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MintResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MintResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator CreateSubCollection(CreateSubCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/mint/sub-collection";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MintResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MintResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MintResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/mint/nft";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);
            Utils.SetAuthorizationHeader(request, accessToken);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MintResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MintResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MintResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator FetchSingleNftDetails(string mintAddress, Action<CommonResponse<SingleNftDetailResponse>> callBack)
        {
            string endpoint = _baseUrlWithVersion + "solana/nft/" + mintAddress;
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "GET");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<SingleNftDetailResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<SingleNftDetailResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<SingleNftDetailResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator FetchMultipleNftsByMintAddresses(FetchMultipleNftsByMintAddressesRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/nft/mints";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MultipleNftDetailResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MultipleNftDetailResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNftDetailResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator FetchMultipleNftsByCreators(FetchMultipleNftsByCreatorsRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/nft/creators";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MultipleNftDetailResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MultipleNftDetailResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNftDetailResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator FetchMultipleNftsByUpdateAuthorities(FetchMultipleNftsByUpdateAuthoritiesRequest requestBody,
            Action<CommonResponse<MultipleNftDetailResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/nft/update-authorities";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MultipleNftDetailResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MultipleNftDetailResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNftDetailResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }

        public IEnumerator FetchMultipleNftsByOwners(FetchMultipleNftsByOwnersRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            string endpoint = _baseUrlWithVersion + "solana/nft/owners";
            
            UnityWebRequest request = new UnityWebRequest(endpoint, "POST");
            
            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, _apiKey);

            byte[] rawRequestBodyToSend = new System.Text.UTF8Encoding().GetBytes(rawRequestBody);
            request.uploadHandler = new UploadHandlerRaw(rawRequestBodyToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
        
            yield return request.SendWebRequest();
        
            string rawResponseBody = request.downloadHandler.text;

            CommonResponse<MultipleNftDetailResponse> responseBody;

            if (request.result != UnityWebRequest.Result.Success)
            {
                responseBody = Utils.CustomErrorResponse<MultipleNftDetailResponse>(request.responseCode, request.error, rawResponseBody);
            }
            else
            {
                responseBody = JsonConvert.DeserializeObject<CommonResponse<MultipleNftDetailResponse>>(rawResponseBody);
                responseBody.HttpStatusCode = request.responseCode;
                
            }

            callBack(responseBody);
        }
    }
}