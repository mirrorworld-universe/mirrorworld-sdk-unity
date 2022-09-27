using System;
using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public class AndroidBridgeUtils
    {
        public AndroidJavaObject GetAndroidMirrorEnv(MirrorworldSDK.MirrorEnv env)
        {
            AndroidJavaObject androidEnv = GetAndroidMirrorEnv("StagingDevNet");

            if (env == MirrorEnv.StagingDevNet)
            {
                androidEnv = GetAndroidMirrorEnv("StagingDevNet");
            }
            else if(env == MirrorEnv.StagingMainNet)
            {
                androidEnv = GetAndroidMirrorEnv("StagingMainNet");
            }
            else if (env == MirrorEnv.ProductionMainnet)
            {
                androidEnv = GetAndroidMirrorEnv("MainNet");
            }
            else if (env == MirrorEnv.ProductionMainnet)
            {
                androidEnv = GetAndroidMirrorEnv("DevNet");
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("Can not find unity env:" + env + ".Will use staging environment.");
            }

            return androidEnv;
        }

        private AndroidJavaObject GetAndroidMirrorEnv(string enumParamName)
        {
            AndroidJavaObject ajc = new AndroidJavaClass("com.mirror.sdk.constant.MirrorEnv");

            AndroidJavaObject androidEnv = ajc.GetStatic<AndroidJavaObject>(enumParamName);

            Debug.Log("GetAndroidMirrorEnv "+ enumParamName+" is:"+androidEnv);

            return androidEnv;
        }
    }
}