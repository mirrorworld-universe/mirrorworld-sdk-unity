using UnityEngine;

namespace MirrorworldSDK.Wrapper
{
    public class AndroidBridgeUtils
    {
        public AndroidJavaObject GetAndroidMirrorEnv(MirrorworldSDK.MirrorEnv env)
        {
            AndroidJavaClass ajc = new AndroidJavaClass("com.mirror.sdk.constant.MirrorEnv");

            AndroidJavaObject androidEnv = ajc.CallStatic<AndroidJavaObject>("Staging");

            if (env == MirrorEnv.Staging)
            {
                androidEnv = ajc.CallStatic<AndroidJavaObject>("Staging");
            }
            else if (env == MirrorEnv.ProductionMainnet)
            {
                androidEnv = ajc.CallStatic<AndroidJavaObject>("MainNet");
            }
            else if (env == MirrorEnv.ProductionMainnet)
            {
                androidEnv = ajc.CallStatic<AndroidJavaObject>("DevNet");
            }
            else
            {
                MirrorWrapper.Instance.LogFlow("Can not find unity env:" + env + ".Will use staging environment.");
            }

            return androidEnv;
        }
    }
}