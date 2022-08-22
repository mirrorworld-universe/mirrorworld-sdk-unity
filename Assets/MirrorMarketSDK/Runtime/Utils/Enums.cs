namespace MirrorworldSDK
{
    public enum Environment
    {
        Staging,
        Production
    }
    
    public enum EnvironmentVersion
    {
        V1
    }

    public enum MirrorResponseCode
    {
        Success = 0,
        PayFailed = 100001,
        AccountLocked = 100002,

    }
}