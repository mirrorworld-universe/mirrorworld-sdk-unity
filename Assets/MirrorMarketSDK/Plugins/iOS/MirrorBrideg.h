//
//  MirrorBrideg.h
//  MirrorWorldSDK
//
//  Created by ZMG on 2022/11/3.
//

#import <Foundation/Foundation.h>
#import "MirrorWorldSDK/MirrorWorldSDK-Swift.h"


@interface MirrorBrideg : NSObject


extern "C"
{
    extern void IOSInitSDK(int environment,char *apikey);
}

extern "C"
{
    typedef void (*IOSLoginCallback) (const char *object);
    extern void IOSStartLogin(IOSLoginCallback callback);
}

extern "C"
{

    ///
//    extern void OpenWallet();
    /// wallet log out callback
    typedef void (*iOSWalletLogOutCallback)(const char *object);
    extern void IOSOpenWallet(iOSWalletLogOutCallback callback);
}




@end


