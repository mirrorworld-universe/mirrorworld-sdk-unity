//
//  MirrorBridegForC.h
//  MirrorWorldSDK
//
//  Created by ZMG on 2022/11/3.
//

#import <Foundation/Foundation.h>

@interface MirrorBridegForC : NSObject


extern "C"
{
    extern void IOSInitSDK(int environment,int chain, char *apikey);
}

extern "C"
{
    typedef void (*IOSLoginCallback) (const char *object);
    extern void IOSStartLogin(IOSLoginCallback callback);
}

extern "C"
{
    typedef void (*iOSWalletLogOutCallback)(const char *object);
    typedef void (*iOSWalletLoginTokenCallback)(const char *object);

    extern void IOSOpenWallet(const char *url,iOSWalletLogOutCallback callback,iOSWalletLoginTokenCallback walletLoginCallback);
}

extern "C"
{
    extern void IOSOpenMarketPlace(char *url);
}

extern "C"
{
    extern void IOSOpenUrl(const char *object);
    typedef void (*IOSSecurityAuthCallback)(const char *object);
    extern void IOSOpenUrlSetCallBack(IOSSecurityAuthCallback callback);
    
    
    
    typedef void (*IOSSecurityCallback)(const char *object);
    extern void IOSGetSecurityToken(char *params,IOSSecurityCallback callback);
}



@end


