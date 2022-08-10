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
        
        public IEnumerator CreateSubCollection(CreateSubCollectionRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
        
        public IEnumerator CreateNft(CreateNftRequest requestBody, string accessToken, Action<CommonResponse<MintResponse>> callBack);
    }
}