using UnityEngine;
using System;

public class IMGUIDialog : MonoBehaviour
{
    // 对话框的标题
    private string title = "Hello, World!";
    // 对话框的内容
    private string content = "This is a dialog box created by IMGUI.";
    // 对话框的按钮数量，最多为2
    private int buttonCount = 2;
    // 对话框的第一个按钮的标签
    private string button1Label = "OK";
    // 对话框的第二个按钮的标签
    private string button2Label = "Cancel";

    // 对话框的第二个按钮的回调函数
    private Action button2Action = () => Debug.Log("You clicked Cancel!");
    // 对话框的第一个按钮的回调函数
    private Action button1Action = () => Debug.Log("You clicked OK!");

    public void InitDialog(string content,string btn1Text,string btn2Text,Action btn1Act,Action btn2Act)
    {
        this.title = title;
        this.content = content;
        if (string.IsNullOrEmpty(btn1Text) || string.IsNullOrEmpty(btn2Text))
        {
            buttonCount = 1;
        }
        else
        {
            buttonCount = 2;
        }
        this.button1Label = btn1Text;
        this.button2Label = btn2Text;
        this.button1Action = btn1Act;
        this.button2Action = btn2Act;
    }

    public void SetContent(string content)
    {
        this.content = content;
    }

    //void OnGUI()
    //{
    //    // 计算对话框的位置和大小，使其居中显示
    //    float width = 300;
    //    float height = 200;
    //    float x = (Screen.width - width) / 2;
    //    float y = (Screen.height - height) / 2;
    //    Rect rect = new Rect(x, y, width, height);

    //    // 绘制对话框的背景框
    //    GUI.Box(rect, title);

    //    // 绘制对话框的内容
    //    GUI.Label(new Rect(x + 10, y + 30, width - 20, height - 80), content);

    //    // 根据按钮数量，绘制对话框的按钮，并添加点击事件
    //    if (buttonCount == 1)
    //    {
    //        if (GUI.Button(new Rect(x + (width - 80) / 2, y + height - 40, 80, 30), button1Label))
    //        {
    //            button1Action();
    //        }
    //    }
    //    else if (buttonCount == 2)
    //    {
    //        if (GUI.Button(new Rect(x + (width - 200) / 4, y + height - 40, 80, 30), button1Label))
    //        {
    //            button1Action();
    //        }
    //        if (GUI.Button(new Rect(x + (width - 200) * 3 / 4 + 80, y + height - 40, 80, 30), button2Label))
    //        {
    //            button2Action();
    //        }
    //    }

    //    gameObject.transform.localScale = new Vector3(4,4,4);
    //}

    void OnGUI()
    {
        // 计算对话框的位置和大小，使其居中显示
        float width = 600;
        float height = 400;
        float x = (Screen.width - width) / 2;
        float y = (Screen.height - height) / 2;
        Rect rect = new Rect(x, y, width, height);

        // 绘制对话框的背景框
        GUIStyle titleFont = new GUIStyle();
        // 设置字体大小为40像素
        titleFont.fontSize = 40;
        titleFont.normal.textColor = Color.white;
        GUI.Box(rect, "");

        // 绘制对话框的内容
        // 创建一个新的GUIStyle对象
        GUIStyle contentFont = new GUIStyle();
        // 设置字体大小为40像素
        contentFont.fontSize = 40;
        contentFont.normal.textColor = Color.white;
        contentFont.wordWrap = true;
        GUI.Label(new Rect(x + 20, y + 60, width - 20, height - 60), content, contentFont);

        // 根据按钮数量，绘制对话框的按钮，并添加点击事件
        var buttonWidth = 160;
        var buttonHeight = 60;

        // 创建一个新的GUIContent对象
        //GUIContent btnContent = new GUIContent();
        //// 为content设置一些属性
        //btnContent.text = button1Label;
        //// 设置文字为"Hello World"
        ////content.image = Resources.Load<Texture2D>(“icon”);
        ////// 设置图片为Resources文件夹下的icon图片
        ////content.tooltip = “This is a button”;
        //// 设置提示为"This is a button"
        ////// 在屏幕上显示一个按钮，使用content作为内容
        //GUI.Button(new Rect(0, 0, 100, 50), btnContent);

        if (buttonCount == 1)
        {
            if (GUI.Button(new Rect(x + (width - 80) / 2, y + height - 80, buttonWidth, buttonHeight), button1Label, GetGUIStyle()))
            {
                button1Action();
            }
        }
        else if (buttonCount == 2)
        {
            if (GUI.Button(new Rect(x + (width - 200) / 4, y + height - 80, buttonWidth, buttonHeight), button1Label, GetGUIStyle()))
            {
                button1Action();
            }
            if (GUI.Button(new Rect(x + (width - 200) * 3 / 4 + 80, y + height - 80, buttonWidth, buttonHeight), button2Label, GetGUIStyle()))
            {
                button2Action();
            }
        }
    }

    private GUIStyle GetGUIStyle()
    {
        GUIStyle contentFont = new GUIStyle(GUI.skin.button);
        // 设置字体大小为40像素
        contentFont.fontSize = 40;
        return contentFont;
    }
}