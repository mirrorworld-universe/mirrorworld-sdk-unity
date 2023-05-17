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
    public List<GameObject> dropGOs;

    //Logic
    public Action<MirrorChain> EnterNextPage;

    private int selectedChain;
    private int selectedNet;

    public void Show()
    {
        gameObject.SetActive(true);
        ChangeNetOptions();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnOptionSelected()
    {
        selectedChain = dropdown.value;
        ChangeNetOptions();
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
        else if (selectedChain == 4)
        {
            chain = MirrorChain.SUI;
        }
        else
        {
            Debug.LogError("MirrorSDK: unknwon selected item index:"+selectedChain);
        }

        MirrorEnv env;
        if (selectedNet == 0)
        {
            env = MirrorEnv.Mainnet;
        }
        else
        if (selectedNet == 1)
        {
            env = MirrorEnv.Devnet;
        }
        else
        {
            env = MirrorEnv.Devnet;
            Debug.LogError("MirrorSDK: Unknwon selectedNet:"+selectedNet);
        }

        MirrorSDK.SetChain(chain);
        MirrorSDK.SetEnvironment(env);

        if(EnterNextPage != null)
        {
            EnterNextPage(chain);
        }
    }

    public void OnNetOptionSelected()
    {
        TMP_Dropdown tMP_Dropdown = dropGOs[selectedChain].transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        selectedNet = tMP_Dropdown.value;
    }

    private void ChangeNetOptions()
    {
        for(int i = 0; i < dropGOs.Count; i++)
        {
            if(i == selectedChain)
            {
                dropGOs[i].SetActive(true);
            }
            else
            {
                dropGOs[i].SetActive(false);
            }
        }
        TMP_Dropdown tMP_Dropdown = dropGOs[selectedChain].transform.Find("Dropdown").GetComponent<TMP_Dropdown>();
        selectedNet = tMP_Dropdown.value;
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
