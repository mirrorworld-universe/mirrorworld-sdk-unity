
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
        ProductionDevnet = 3,
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

    public class MirrorSafeOptType
    {
        public static string MintNFT = "mint_nft";
        public static string TransferSol = "transfer_sol";
        public static string TransferSPLToken = "transfer_spl_token";
        public static string CreateCollection = "create_collection";
        public static string ListNFT = "list_nft";
        public static string BuyNFT = "buy_nft";
        public static string CancelListing = "cancel_listing";
        public static string UpdateListing = "update_listing";
        public static string TransferNFT = "transfer_nft";
        public static string CreateMarketplace = "create_marketplace";
        public static string UpdateMarketplace = "update_marketplace";
        public static string Interaction = "interaction";
    }

}