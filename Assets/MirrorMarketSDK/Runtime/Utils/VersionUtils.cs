using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionUtils
{
    public static bool IsUnityLowerThan2018()
    {
        //// 将当前Unity编辑器的版本号转换为Version对象
        //System.Version currentVersion = new System.Version(Application.unityVersion);
        //// 将Unity2018的版本号转换为Version对象
        //System.Version targetVersion = new System.Version("2018.999.999f1");
        //// 比较两个版本号是否相等
        ////bool isEqual = currentVersion == targetVersion;
        //// 比较当前版本号是否小于Unity2018的版本号
        //bool isLower = currentVersion < targetVersion;
        //// 比较当前版本号是否小于或等于Unity2018的版本号
        ////bool isLowerOrEqual = currentVersion <= targetVersion;
        //// 在控制台输出比较结果
        ////Debug.Log("Is current version equal to Unity2018? " + isEqual);
        ////Debug.Log("Is current version lower than Unity2018? " + isLower);
        ////Debug.Log("Is current version lower or equal to Unity2018? " + isLowerOrEqual);
        //return isLower;

        return IsVersionLower(Application.unityVersion, "2018.999.999f1");
    }

    private static List<string> SplitVersion(string versionData)
    {
        string[] list = versionData.Split('.');
        List<string> listArray = new List<string>();
        foreach (string v in list)
        {
            listArray.Add(v);
        }

        return listArray;
    }

    private static bool IsVersionLower(string version1, string version2)
    {
        string[] list1 = version1.Split('.');
        string[] list2 = version2.Split('.');

        if(list1.Length != list2.Length)
        {
            LogUtils.LogWarn("Version length is different, can not compare!");
            return false;
        }

        for (int i=0;i<list1.Length;i++)
        {
            string v = list1[i];
            int vNum = int.Parse(v);

            int tarNum = int.Parse(list2[i]);
            if (vNum == tarNum) continue;
            if (vNum < tarNum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }
}
