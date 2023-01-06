using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class MirrorMarketNFTObj
    {
        public string name;

        public string image;

        public string mint_address;

        public string collection;

        public string owner_address;

        public bool listed;
    }
}