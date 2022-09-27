
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK
{
    public static class Utils
    {
        public static bool debugEditor = false;
        public static bool debugIOS = false;

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

        public static void SetRefreshToken(UnityWebRequest req,string refreshToken)
        {
            req.SetRequestHeader("x-refresh-token", refreshToken);
        }

        public static bool IsEditor()
        {
            return Application.platform == RuntimePlatform.LinuxEditor ||
                Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.WindowsEditor;
        }
    }
}