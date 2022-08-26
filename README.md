# Mirror World Unity SDK(v0.1.0)

Mirror World's Official Unity SDK  

## Getting started
Create a developer account at https://app.mirrorworld.fun. Create project and create an API Key.  

## Import SDK

1. One way,download assets [Mirror World Unity SDK](https://github.com/mirrorworld-universe/mirrorworld-sdk-unity/releases/tag/v0.1.0).  
import it to your project `Assets > Import Package > Custom Package` and select the package you just downloaded.
2. Besides, you can also search for the package in Unity Assets Store with the key word "com.mirror.sdk" and import it.

## Usage

#### Configration with adding prefab manually
![image](https://github.com/mirrorworld-universe/mirrorworld-sdk-unity/blob/master/CaseImage/case_prefab_example.jpg)

Explanation of beyond image:
- Api Key
Input your aky key which requested on https://docs.mirrorworld.fun/ .  
- Debug Mode
If checking this, you could see all the flow and error notice on console.
- Environment
Choose the environment you want to use.
- Debug Email and Password
In pre-release beta, you can only login SDK with this function. Input your email and password on https://docs.mirrorworld.fun/ .

#### Configration dynamic
We can init Mirror World SDK with the following code:  
```c#
GameObject mirrorObj = new GameObject("MirrorSDK", typeof(MirrorSDK));
string apiKey = "your api key";
bool debugMode = true;
Environment environment = Environment.StagingDevnet;

MirrorSDK.InitSDK(apiKey, mirrorObj, debugMode, environment);
```

2. Guild user to login
When user open your app, you may want to know whether this user needs to login, you can call the following code to know that.
```c#
MirrorSDK.IsLoggedIn((isLoggedIn) => {
    Debug.Log("isLoggedIn:" + isLoggedIn);
});
```

If you want him to login(or again), you can use the following code:
```c#
MirrorSDK.StartLogin();
```

## Full API Documentation
You can view the documentation for Mirror World SDK for Mobile on [our Official Documentation Site](https://docs.mirrorworld.fun/unity/unity-api)

