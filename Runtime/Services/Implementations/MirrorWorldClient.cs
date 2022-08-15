using mirrorworld_sdk_unity.Runtime.Services.Interfaces;

namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
{
    public class MirrorWorldClient : IMirrorWorldClient
    {
        private readonly Environment _environment;

        private readonly EnvironmentVersion _environmentVersion;

        private readonly string _baseUrlWithVersion;

        private readonly string _apiKey;
        
        private IAuthenticationService _authentication;
        
        private IMarketplaceService _marketplace;

        private IWalletService _wallet;
        
        private IAndroidService _android;
        
        public MirrorWorldClient(Environment environment, EnvironmentVersion environmentVersion, string apiKey)
        {
            _environment = environment;
            _environmentVersion = environmentVersion;
            _apiKey = apiKey;

            string baseUrl;

            baseUrl = environment == Environment.Staging ? Constant.StagingV1ApiBaseUrl : Constant.ProductionV1ApiBaseUrl;

            _baseUrlWithVersion = baseUrl + "v1/";

        }

        public IAuthenticationService Authentication
        {
            get { return _authentication ??= new AuthenticationService(_environment, _environmentVersion, _apiKey); }
        }
        
        public IMarketplaceService Marketplace
        {
            get { return _marketplace ??= new MarketplaceService(_environment, _environmentVersion, _apiKey); }
        }

        public IWalletService Wallet
        {
            get { return _wallet ??= new WalletService(_environment, _environmentVersion, _apiKey); }
        }
        
        public IAndroidService Android
        {
            get { return _android ??= new AndroidService(); }
        }
    }
}