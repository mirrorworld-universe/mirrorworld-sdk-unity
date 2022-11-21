//
//  MirrorBrideg.m
//  MirrorWorldSDK
//
//  Created by ZMG on 2022/11/3.
//

#import "MirrorBrideg.h"




@implementation MirrorBrideg


extern "C"
{
    extern void IOSInitSDK(int environment,char *apikey){
        MWEnvironment env = MWEnvironmentMainNet;
        if (environment == 0){env = MWEnvironmentStagingDevNet; }
        if (environment == 1){env = MWEnvironmentStagingMainNet; }
        if (environment == 3){ env = MWEnvironmentDevNet; }
        NSString *key = [NSString stringWithFormat:@"%s",apikey];
        [[MirrorWorldSDK share] initSDKWithEnv:env apiKey:key];
    }
}

extern "C"
{
    typedef void (*IOSLoginCallback) (const char *object);
    extern void IOSStartLogin(IOSLoginCallback callback){
        [[MirrorWorldSDK share] StartLoginOnSuccess:^(NSDictionary<NSString *,id> * userinfo) {
            
            NSData *jsonData = [NSJSONSerialization dataWithJSONObject:userinfo options:NSJSONWritingPrettyPrinted error:nil];
            NSString *user = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
            const char *cString = [user UTF8String];
            callback(cString);
        } onFail:^{
            
        }];
    }
}


extern "C"
{
    typedef void (*iOSWalletLogOutCallback)(const char *object);
    extern void IOSOpenWallet(iOSWalletLogOutCallback callback){
        [[MirrorWorldSDK share] OpenWalletOnLogout:^{
            callback("wallet is logout.");
        }];
    }

}



@end
