using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MirrorworldSDK.UI
{
    public class ResourcesUtils : Singleton<ResourcesUtils>
    {

        public GameObject LoadPrefab(string prefabPath, Transform parent)
        {
            GameObject pre = (GameObject)Resources.Load(prefabPath);
            GameObject instancess = GameObject.Instantiate(pre, parent) as GameObject;
            return instancess;
        }
    }
}
