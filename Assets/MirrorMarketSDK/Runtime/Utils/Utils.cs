
using System.Text.RegularExpressions;
using MirrorworldSDK.Models;
using UnityEngine;
using UnityEngine.Networking;

namespace MirrorworldSDK
{
    public static class MirrorUtils
    {
        public static bool debugEditor = false;
        public static bool debugIOS = false;

        public static CommonResponse<TData> CustomErrorResponse<TData>(long httpStatusCode, string error, string message)
        {
            return new CommonResponse<TData>()
            {
                data = default(TData),
                code = 0,
                status = "fail",
                message = message,
                error = error,
                http_status_code = httpStatusCode
        
            };
        }

        public static void SetAuthorizationHeader(UnityWebRequest req, string accessToken)
        {
            if (accessToken != "" && accessToken != null)
            {
                req.SetRequestHeader("Authorization", "Bearer " + accessToken);
            }
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

        public static void SetRefreshToken(UnityWebRequest req, string refreshToken)
        {
            req.SetRequestHeader("x-refresh-token", refreshToken);
        }

        public static void SetXAuthToken(UnityWebRequest req, string authToken)
        {
            if(authToken != "" && authToken != null)
            {
                req.SetRequestHeader("x-authorization-token", authToken);
            }
        }

        public static bool IsEditor()
        {
            return Application.platform == RuntimePlatform.LinuxEditor ||
                Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.WindowsEditor;
        }

        public static string GetNoSymbolString(string oriString)
        {
            const string regex = @"\\(u|x)[[a-z\d]{1,4}";

            var sanitisedUserName = Regex.Replace(oriString, regex, string.Empty);

            return sanitisedUserName;
        }
    }
}