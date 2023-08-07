using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VersionUtils
{
    public static bool IsUnityLowerThan2018()
    {
        // 将当前Unity编辑器的版本号转换为Version对象
        System.Version currentVersion = new System.Version(Application.unityVersion);
        // 将Unity2018的版本号转换为Version对象
        System.Version targetVersion = new System.Version("2018.999.999f1");
        // 比较两个版本号是否相等
        //bool isEqual = currentVersion == targetVersion;
        // 比较当前版本号是否小于Unity2018的版本号
        bool isLower = currentVersion < targetVersion;
        // 比较当前版本号是否小于或等于Unity2018的版本号
        //bool isLowerOrEqual = currentVersion <= targetVersion;
        // 在控制台输出比较结果
        //Debug.Log("Is current version equal to Unity2018? " + isEqual);
        //Debug.Log("Is current version lower than Unity2018? " + isLower);
        //Debug.Log("Is current version lower or equal to Unity2018? " + isLowerOrEqual);
        return isLower;
    }
}
