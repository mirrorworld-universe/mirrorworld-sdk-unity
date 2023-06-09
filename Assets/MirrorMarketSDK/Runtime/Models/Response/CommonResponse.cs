
using System;
using MirrorWorldResponses;

namespace MirrorworldSDK.Models
{
    [Serializable]
    public class CommonResponse<T>
    {
        public T data;

        public long code;

        public string error;

        public string status;

        public string message;

        public long http_status_code;

        public static implicit operator CommonResponse<T>(CommonResponse<SUIResMintNFT> v)
        {
            throw new NotImplementedException();
        }
    }
}