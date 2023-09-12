# Unity MirrorSDK iOS Config

**Build your xCode project**
Find File->Build and settings->iOS->Switch Platform->Build

**Add Mirror World Framework**
Open the build xCode project.
Select your project root->TARGETS/Unity-iPhone->Build pharses->Copy Files
Change the destination to "Frameworks" and click "+" button to add MirrorWorldSDK.framework to your project.

**Edit UnityAppController.mm**
First, add this import to head of the file:
```swift
#import <MirrorWorldSDK/MirrorWorldSDK-Swift.h>
```

Second, add this to openUrl function:
```swift
[[MirrorWorldSDK share] handleOpenWithUrl:url];
```

So, finnaly your openUrl function may looked like this:
```swift
- (BOOL)application:(UIApplication*)app openURL:(NSURL*)url options:(NSDictionary<NSString*, id>*)options
{
    id sourceApplication = options[UIApplicationOpenURLOptionsSourceApplicationKey], annotation = options[UIApplicationOpenURLOptionsAnnotationKey];

    NSMutableDictionary<NSString*, id>* notifData = [NSMutableDictionary dictionaryWithCapacity: 3];
    if (url)
    {
        notifData[@"url"] = url;
        UnitySetAbsoluteURL(url.absoluteString.UTF8String);
    }
    if (sourceApplication) notifData[@"sourceApplication"] = sourceApplication;
    if (annotation) notifData[@"annotation"] = annotation;

    AppController_SendNotificationWithArg(kUnityOnOpenURL, notifData);
    [[MirrorWorldSDK share] handleOpenWithUrl:url];
    return YES;
}
```