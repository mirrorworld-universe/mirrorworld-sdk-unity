

using MirrorworldSDK.Models;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string apiKey = "";
        private bool debugMode = false;
        private MirrorEnv environment;
        private MirrorChain chain;

        private string accessToken = "";
        private string refreshToken = "";
        public string authToken = "";
        private UserResponse tmpUser = null;

        //logic
        private MonoBehaviour monoBehaviour;
        private bool inited = false;

        public void InitSDK(MonoBehaviour monoBehaviour,MirrorEnv environment,MirrorChain chain, string apiKey,bool useDebug)
        {
            if (inited)
            {
                LogFlow("Unity wrapper can't be inited again.");
                return;
            }
            inited = true;
            this.environment = environment;
            this.monoBehaviour = monoBehaviour;
            SetChain(chain);
            SetAPIKey(apiKey);
            SetDebug(useDebug);
            LogFlow("Unity wrapper inited.");
        }

        public string GetAccessToken()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                LogUtils.LogWarn("There is no access token yet, did you finished login?");
                return "No_Access_Token";
            }
            return accessToken;
        }

        public void SetChain(MirrorChain chain)
        {
            LogFlow("Set chain to " + chain);

            this.chain = chain;
        }

        public void SetEnvironment(MirrorEnv env)
        {
            LogFlow("Set env to " + env);

            this.environment = env;
        }

        public MirrorEnv GetEnvironment()
        {
            if (!inited)
            {
                LogUtils.LogWarn("MWSDK has not been inited!");
            }
            return this.environment;
        }

        public MirrorChain GetChain()
        {
            return chain;
        }

        public void SetDebug(bool debugMode)
        {
            this.debugMode = debugMode;
        }

        private void SetAPIKey(string apiKey)
        {
            LogFlow("Set unity sdk api key"+apiKey);
            this.apiKey = apiKey;
        }

        public MonoBehaviour GetMonoBehaviour()
        {
            return monoBehaviour;
        }

        private void SetLoginResponse(LoginResponse response)
        {
            SaveStringToLocal(localKeyRefreshToken, response.refresh_token);
            accessToken = response.access_token;
        }

        public void LogFlow(string content)
        {
            if (debugMode)
            {
                Debug.Log("MirrorSDKUnity:" + content);
            }
        }

        public void LogWarn(string content)
        {
            if (debugMode)
            {
                Debug.LogWarning("MirrorSDK Warn:" + content);
            }
        }

        public UserResponse GetCurrentUser()
        {
            return tmpUser;
        }

    }

}