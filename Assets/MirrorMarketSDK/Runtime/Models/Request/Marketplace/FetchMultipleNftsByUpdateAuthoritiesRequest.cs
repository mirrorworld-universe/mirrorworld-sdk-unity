using System;
using System.Collections.Generic;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class FetchMultipleNftsByUpdateAuthoritiesRequest
    {
        public List<string> update_authorities;

        public long limit;

        public long offset;
    }
}