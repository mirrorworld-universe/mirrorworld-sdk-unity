﻿using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class ApproveTransferSPLToken
    {
        public string to_publickey;
        public ulong amount;
        public string token_mint;
        public int decimals;
    }
}
    
