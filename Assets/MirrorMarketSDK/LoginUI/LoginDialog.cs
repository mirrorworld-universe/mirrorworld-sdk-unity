using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK;
using UnityEngine;
using UnityEngine.UI;

public class LoginDialog : MonoBehaviour
{
    public Text contentText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBtnLoggedClicked()
    {
        MirrorSDK.CompleteLoginWithSession((success)=>{
            if (success)
            {
                contentText.text = "Login success!";

                Destroy(gameObject);
            }
            else
            {
                contentText.text = "Login have no response,please try again.";
            }
        });
    }

    public void OnBtnCalcelClicked()
    {
        MirrorSDK.LoginDebugClear();

        Destroy(gameObject);
    }
}
