using UnityEngine;
using System.Collections;
using MirrorworldSDK.Wrapper;

public class LogUtils
{
    public static void LogFlow(string content)
    {
        MirrorWrapper.Instance.LogFlow(content);
    }

    public static void LogWarn(string content)
    {
        MirrorWrapper.Instance.LogWarn(content);
    }
}
