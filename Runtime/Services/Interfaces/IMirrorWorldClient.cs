namespace mirrorworld_sdk_unity.Runtime.Services.Interfaces
{
    public interface IMirrorWorldClient
    {
        public IAuthenticationService Authentication { get; }
        
        public IMarketplaceService Marketplace { get; }
        
        public IWalletService Wallet { get; }
        
        public IAndroidService Android { get; }
    }
}