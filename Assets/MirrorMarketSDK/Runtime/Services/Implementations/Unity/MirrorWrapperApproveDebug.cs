using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using MirrorworldSDK.Models;

using System.Runtime.InteropServices;

namespace MirrorworldSDK.Wrapper
{
    public partial class MirrorWrapper
    {
        private readonly string urlApproveGetSecretToken = "auth/developer/secret-access-key";

        public void LoginAsDeveloper(Action<bool> action)
        {
            //if(accessToken != null && accessToken != "")
            //{
            //    LogFlow("Exsist access token already, no need to login");

            //    action(true);

            //    return;
            //}
            ApproveDebugKeyParams param = new ApproveDebugKeyParams();
            param.valid_for_duration = 3600000;
            string data = JsonUtility.ToJson(param);

            string url = GetActionRoot() + urlApproveGetSecretToken;
            monoBehaviour.StartCoroutine(Post(url, data, (result) => {
                LogFlow("Require approve debug secrete token result:" + result);
                CommonResponse<ApproveDebugResponse> response = JsonUtility.FromJson<CommonResponse<ApproveDebugResponse>>(result);
                if (response.code == (long)MirrorResponseCode.Success)
                {
                    string secreteToken = response.data.secret_access_key;
                    GetCurrentUserInfo((res)=> {
                        UserResponse user = res.data;
                        SaveKeyParams(secreteToken,null,user);
                        UpdateRefreshToken("");
                        action(true);
                    });
                }
                else
                {
                    LogFlow("GetAccessSecretKey failed.");
                    action(false);
                }
            }));
        }
    }
}

