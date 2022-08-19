#if  (!(UNITY_IOS) || UNITY_EDITOR) && (!(UNITY_ANDROID) || UNITY_EDITOR)
using System;
using UnityEngine;

namespace MirrorworldSDK
{
    public partial class MirrorSDK
    {
        private string localKeyRefreshToken = "local_key_refresh_token";

        private void SaveStringToLocal(string key,string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        private string GetStringFromLocal(string key)
        {
            return PlayerPrefs.GetString(key);
        }
    }

}
#endif