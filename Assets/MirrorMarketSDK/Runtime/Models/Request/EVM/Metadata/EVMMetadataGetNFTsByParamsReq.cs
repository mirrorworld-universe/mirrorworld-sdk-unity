using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Wrapper
{
    [Serializable]
    public class EVMMetadataGetNFTsByParamsReq
    {
        public string contract;
        public int sale;
        public int page;
        public int page_size;
        public EVMGetNFTsByParamsReqOrder order;
        //optional
        public string marketplace_address;
        public List<EVMGetNFTsByParamsReqFilter> filter;
    }

    [Serializable]
    public class EVMGetNFTsByParamsReqOrder
    {
        public string order_by;
        public bool desc;
    }

    [Serializable]
    public class EVMGetNFTsByParamsReqFilter
    {
        public string filter_name;
        public string filter_type;
        public List<int> filter_value;
    }
}
