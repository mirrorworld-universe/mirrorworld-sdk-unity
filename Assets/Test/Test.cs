using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //if(Application.platform == RuntimePlatform.Android)
        //{
        //    AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        //    AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        //    //AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdk3.MirrorSDKJava");
        //    //AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance", jo);
        //    //javaObject.Call("InitSDK");

        //    AndroidJavaClass javaClass = new AndroidJavaClass("com.mirror.sdkjava.MirrorSDKJava");
        //    AndroidJavaObject javaObject = javaClass.CallStatic<AndroidJavaObject>("getInstance");
        //    javaObject.Call("InitSDK",jo);
        //    javaObject.Call("SetAppID", "WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ");
        //    javaObject.Call("SetDebug", true);
        //    javaObject.Call("StartLogin");
        //}
        //else
        //{
        //    Debug.LogWarning("Don't support platform");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnInitSDKClicked()
    {
        MirrorSDK.InitSDKWithParams("WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ", true);
    }

    public void onBtnStartLoginClicked()
    {
        MirrorSDK.StartLoginWithCallback((result)=> {
            Debug.Log("adfasd  "+result);
        });
    }

    public void onBtnGetWalletAddressClicked()
    {
        MirrorSDK.GetWalletAddress((result)=>{
            Debug.Log(result);
            });
    }
}
