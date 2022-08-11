using mirrorworld_sdk_unity.Runtime.Services.Interfaces;

namespace mirrorworld_sdk_unity.Runtime.Services.Implementations
{
    public class WalletService : IWalletService
    {
        private readonly Environment _environment;

        private readonly EnvironmentVersion _environmentVersion;

        private readonly string _baseUrlWithVersion;

        private readonly string _apiKey;
        
        public WalletService(Environment environment, EnvironmentVersion environmentVersion, string apiKey)
        {
            _environment = environment;
            _environmentVersion = environmentVersion;
            _apiKey = apiKey;

            string baseUrl;

            baseUrl = environment == Environment.Staging ? Constant.StagingV1ApiBaseUrl : Constant.ProductionV1ApiBaseUrl;

            _baseUrlWithVersion = baseUrl + "v1/";

        }
    }
}