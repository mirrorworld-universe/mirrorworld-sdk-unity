using System;
namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private string apiKey;
        private bool useDebug = false;

        public MirrorWrapper()
        {
        }

        public void SetApiKey(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public void SetDebug(bool useDebug)
        {
            this.useDebug = useDebug;
        }

        public bool GetDebug()
        {
            return useDebug;
        }

        //singleton
        protected static MirrorWrapper _instance = null;
        public static MirrorWrapper Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new MirrorWrapper();
                }
                return _instance;
            }
        }
    }

}
