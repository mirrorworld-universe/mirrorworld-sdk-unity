
using MirrorworldSDK;
using ThinkingAnalytics;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ThinkingAnalyticsAPI.CalibrateTime(1212);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onBtnInitSDKClicked()
    {
        MirrorSDK.InitSDK("WsPRi3GQz0FGfoSklYUYzDesdKjKvxdrmtQ");
        MirrorSDK.SetDebugMode(true);
    }

    public void onBtnStartLoginClicked()
    {
        MirrorSDK.StartLogin();
    }

    public void onBtnGetWalletAddressClicked()
    {
        MirrorSDK.GetWalletAddress((result)=>{
            Debug.Log(result);
            });
    }
}
