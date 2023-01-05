using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK.Wrapper;
using UnityEngine;
using UnityEngine.UI;

public class UniversalDialog : MonoBehaviour
{
    public Text titleText;
    public Text contentText;
    public Text btnYesText;
    public Text btnNoText;
    public Button btnYes;
    public Button btnNo;

    private Action yesAction;
    private Action noAction;

    public void Init(string title,string content,string yesText,string noText,Action yesAction,Action noAction)
    {
        titleText.text = title;
        contentText.text = content;
        this.yesAction = yesAction;
        this.noAction = noAction;
        btnYesText.text = yesText;
        btnNoText.text = noText;
        btnYes.gameObject.SetActive(yesAction != null);
        btnNo.gameObject.SetActive(noAction != null);
    }

    public void DestroyDialog()
    {
        MirrorWrapper.Instance.LogFlow("Universal dialog destroied.");
        GameObject.Destroy(gameObject);
    }

    public void OnBtnYesClicked()
    {
        if(yesAction != null)
        {
            yesAction();
        }
    }

    public void OnBtnNoClicked()
    {
        if (noAction != null)
        {
            noAction();
        }
    }
}
