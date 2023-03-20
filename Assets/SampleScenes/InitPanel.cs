using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;

public class InitPanel : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TextMeshProUGUI inputAPIKey;

    private int selectedChain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOptionSelected()
    {
        int value = dropdown.value;
        MirrorWrapper.Instance.LogFlow("Select :"+value);
    }

    public void OnInitBtnClicked()
    {
        string apiKey = inputAPIKey.GetParsedText().Trim();
        if (apiKey == "")
        {
            ShowUniversalNotice("API Key","Please input API Key.","OK",null,null,null);
            return;
        }

        if(selectedChain == 1)
        {

        }

        //MirrorSDK.InitSDK(apiKey,gameObject,true,MirrorworldSDK.MirrorEnv.Devnet);
    }

    private UniversalDialog ShowUniversalNotice(string title, string content, string yesText, string noText, Action yesAction, Action noAction)
    {
        MonoBehaviour monoBehaviour = this;
        GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("UniversalDialog", GameObject.Find("Canvas").transform);
        UniversalDialog dialog = dialogCanvas.GetComponent<UniversalDialog>();
        dialog.Init(title, content, yesText, noText, yesAction, noAction);
        return dialog;
    }
}
