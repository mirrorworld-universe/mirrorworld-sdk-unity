using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace MirrorWorldResponses
{
    [System.Serializable]
    public class EVMResMetadataNFTEvents
    {
        public int total_page;
        public int page_size;
        public List<EVMMetadataNFTEvent> events;
    }

    [System.Serializable]
    public class EVMMetadataNFTEvent
    {
        public string contract;
        public int token_id;
        public string payment_token;
        public string marketplace_address;
        public string event_type;
        public string price;
        public string from_address;
        public string to_address;
        public DateTime event_date;
        public string signature;
        public string date_tag;
    }
}
