using System;
using System.Collections;
using System.Collections.Generic;
using MirrorworldSDK;
using MirrorworldSDK.UI;
using MirrorworldSDK.Wrapper;
using TMPro;
using UnityEngine;

public class InitPanel : MonoBehaviour
{
    public TMP_Dropdown dropdown;

    //Logic
    public Action<MirrorChain> EnterNextPage;

    private int selectedChain;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnOptionSelected()
    {
        selectedChain = dropdown.value;
    }

    public void OnInitBtnClicked()
    {
        MirrorChain chain = MirrorChain.Solana;

        if(selectedChain == 0)
        {
            chain = MirrorChain.Solana;
        }
        else if (selectedChain == 1)
        {
            chain = MirrorChain.Ethereum;
        }
        else if (selectedChain == 2)
        {
            chain = MirrorChain.Polygon;
        }
        else if (selectedChain == 3)
        {
            chain = MirrorChain.BNB;
        }
        else
        {
            Debug.LogError("MirrorSDK: unknwon selected item index:"+selectedChain);
        }

        MirrorSDK.SetChain(chain);

        if(EnterNextPage != null)
        {
            EnterNextPage(chain);
        }
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
