using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class FetchMultipleNftsByOwnersRequest
    {
        public List<string> owners;

        public long limit;

        public long offset;
    }
}