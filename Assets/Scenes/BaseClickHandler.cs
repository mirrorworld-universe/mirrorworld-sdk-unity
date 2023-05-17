using UnityEngine;
using System.Collections;
using TMPro;
using System;
using MirrorworldSDK.Wrapper;
using MirrorworldSDK.UI;

public class BaseClickHandler
{
    public GameObject apiInfo;
    public GameObject apiList;
    protected TextMeshProUGUI apiNameText, btnText, contentText;
    protected ParamCell cell1, cell2, cell3, cell4, cell5, cell6;
    protected string v1, v2, v3, v4, v5, v6;
    protected Action clickAction;

    protected void PrintLog(string content)
    {
        contentText.text += "MirrorTest:" + content + "\n";
        GUIUtility.systemCopyBuffer = content;
    }

    protected void ClearLog()
    {
        contentText.text = "";
    }

    protected UniversalDialog ShowUniversalNotice(string title, string content, string yesText, string noText, Action yesAction, Action noAction)
    {
        MonoBehaviour monoBehaviour = MirrorWrapper.Instance.GetMonoBehaviour();
        GameObject dialogCanvas = ResourcesUtils.Instance.LoadPrefab("UniversalDialog", GameObject.Find("Canvas").transform);
        UniversalDialog dialog = dialogCanvas.GetComponent<UniversalDialog>();
        dialog.Init(title, content, yesText, noText, yesAction, noAction);
        return dialog;
    }
}
