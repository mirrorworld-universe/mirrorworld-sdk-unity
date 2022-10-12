using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class FetchMultipleNftsByCreatorsRequest
    {
        public List<string> creators;

        public long limit;

        public long offset;
    }
}