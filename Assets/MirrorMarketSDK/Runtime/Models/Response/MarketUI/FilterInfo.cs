using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class FilterInfo
    {
        public string filter_name;
        public string filter_type;
        public List<string> filter_value;
    }
}
    