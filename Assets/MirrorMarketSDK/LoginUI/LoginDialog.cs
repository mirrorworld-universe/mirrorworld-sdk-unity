using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using UnityEngine;
using UnityEngine.UI;

public class LoginDialog : MonoBehaviour
{
    public Text titleText;

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
        contentText.text = "Checking login status...";

        MirrorSDK.CompleteLoginWithSession((success)=>{
            if (success)
            {
                MirrorWrapper.Instance.LoginAsDeveloper((loginSuccess) => {
                    contentText.text = "Login success!";

                    Destroy(gameObject);
                    //MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
                    //GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("UniversalDialog", monoBehaviour.transform);
                    //UniversalDialog dialog = dialogCanvas.GetComponent<UniversalDialog>();
                    //dialog.Init("Developer Login Success.", "You can do auto login testing whenever your game is opened.", "Ok", "", () => {
                    //    dialog.DestroyDialog();
                    //}, null);
                });
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
