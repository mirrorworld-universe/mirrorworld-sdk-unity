using System;
namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        public MirrorWrapper()
        {

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
