using System;
using System.Collections;
using MirrorworldSDK;
using MirrorworldSDK.Models;
using Newtonsoft.Json;
using UnityEngine.Networking;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlLoginWithEmail = Constant.StagingV1ApiBaseUrl + "auth/login";
        private readonly string urlLoginWithGoogle = Constant.StagingV1ApiBaseUrl + "auth/google";

        public IEnumerator LoginWithEmail(string apiKey, BasicEmailLoginRequest requestBody)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            UnityWebRequest request = new UnityWebRequest(urlLoginWithEmail, "POST");

            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, apiKey);

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

            yield return responseBody;
        }

        public IEnumerator LoginWithGoogle(string apiKey, LoginWithGoogleRequest requestBody)
        {
            var rawRequestBody = JsonConvert.SerializeObject(requestBody);

            UnityWebRequest request = new UnityWebRequest(urlLoginWithGoogle, "POST");

            Utils.SetContentTypeHeader(request);
            Utils.SetAcceptHeader(request);
            Utils.SetApiKeyHeader(request, apiKey);

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

            yield return responseBody;
        }
    }
}

