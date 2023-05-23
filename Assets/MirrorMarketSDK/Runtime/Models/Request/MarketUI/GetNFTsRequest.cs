using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class GetNFTsRequest
    {
        public string collection;

        public int page;

        public int page_size;

        public GetNFTsRequestOrder order;

        public int sale;

        public List<GetNFTsRequestFilter> filter;
    }


    [Serializable]
    public class GetNFTsRequestOrder
    {
        public string order_by;

        public bool desc;
    }

    [Serializable]
    public class GetNFTsRequestFilter
    {
        public string filter_name;

        public string filter_type;

        public List<object> filter_value;
    }
}