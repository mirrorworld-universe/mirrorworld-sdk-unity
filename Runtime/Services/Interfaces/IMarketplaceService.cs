using System;
using System.Collections;
using mirrorworld_sdk_unity.Runtime.Models.Request.Marketplace;
using mirrorworld_sdk_unity.Runtime.Models.Response;
using mirrorworld_sdk_unity.Runtime.Models.Response.Marketplace;

namespace mirrorworld_sdk_unity.Runtime.Services.Interfaces
{
    public interface IMarketplaceService
    {
        public IEnumerator CreateCollection(CreateCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator CreateSubCollection(CreateSubCollectionRequest requestBody, string mintAddress, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator FetchSingleNftDetails(string mintAddress, Action<CommonResponse<SingleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByMintAddresses(FetchMultipleNftsByMintAddressesRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByCreators(FetchMultipleNftsByCreatorsRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByUpdateAuthorities(FetchMultipleNftsByUpdateAuthoritiesRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
        
        public IEnumerator FetchMultipleNftsByOwners(FetchMultipleNftsByOwnersRequest requestBody, Action<CommonResponse<MultipleNftDetailResponse>> callBack);
    }
}