using UnityEngine;
using System.Collections;
using System;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class MirrorMarketNFTEvent
    {
        public string mint_address;

        public string event_type;

        public string price;

        public string from_address;

        public string to_address;

        public string event_date;

        public string date_tag;

        public string signature;
    }
}