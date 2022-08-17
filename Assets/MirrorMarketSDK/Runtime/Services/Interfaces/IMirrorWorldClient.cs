namespace MirrorworldSDK.Interfaces
{
    public interface IMirrorWorldClient
    {
        public IAuthenticationService Authentication { get; }
        
        public IMarketplaceService Marketplace { get; }
        
        public IWalletService Wallet { get; }
    }
}