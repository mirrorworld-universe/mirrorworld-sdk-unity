using mirrorworld_sdk_unity.Runtime.Models.Response;
using UnityEngine.Networking;

namespace MirrorworldSDK
{
    public static class Utils
    {
        public static CommonResponse<TData> CustomErrorResponse<TData>(long httpStatusCode, string error, string message)
        {
            return new CommonResponse<TData>()
            {
                Data = default(TData),
                Code = 0,
                Status = "fail",
                Message = message,
                Error = error,
                HttpStatusCode = httpStatusCode
        
            };
        }

        public static void SetAuthorizationHeader(UnityWebRequest req, string accessToken)
        {
            req.SetRequestHeader("Authorization", "Bearer " + accessToken);
        }
        
        public static void SetApiKeyHeader(UnityWebRequest req, string apiKey)
        {
            req.SetRequestHeader("x-api-key", apiKey);
        }
        
        public static void SetAcceptHeader(UnityWebRequest req)
        {
            req.SetRequestHeader("Accept", "application/json");
        }
        
        public static void SetContentTypeHeader(UnityWebRequest req)
        {
            req.SetRequestHeader("Content-Type", "application/json");
        }
    }
}