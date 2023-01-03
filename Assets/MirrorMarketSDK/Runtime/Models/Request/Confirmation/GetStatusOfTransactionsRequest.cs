using UnityEngine;
using System.Collections;

using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetStatusOfTransactionsRequest
    {
        public List<string> signatures;
    }
}
