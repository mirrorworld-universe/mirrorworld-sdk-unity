
namespace MirrorworldSDK
{
    public enum MirrorEnv
    {
        StagingDevNet = 0,
        StagingMainNet,
        ProductionMainnet,
        ProductionDevnet
    }

    public enum MirrorEnvPublic
    {
        ProductionMainnet = 2,
        ProductionDevnet
    }

    public enum EnvironmentVersion
    {
        V1
    }

    public enum MirrorResponseCode
    {
        Success = 0,
        LocalFailed = 400,
        PayFailed = 100001,
        AccountLocked = 100002,
    }

    public class Confirmation
    {
        public static string Default = null;
        public static string Finalized = "finalized";
        public static string Confirmed = "confirmed";
        public static string Processed = "processed";
    }
}