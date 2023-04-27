using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParamCell : MonoBehaviour
{
    public GameObject cellObject;
    public TMP_InputField totalInputField;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI hintText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Show(string title, string hint)
    {
        cellObject.SetActive(true);
        totalInputField.text = "";
        titleText.text = title;
        hintText.text = hint;
    }

    public void Hide()
    {
        cellObject.SetActive(false);

    }

    public string GetInput()
    {
        return inputText.GetParsedText().Trim();
    }
}
