# Unity MirrorSDK Android Config

**Switch to Android platform**
Find File -> Build and settings -> Choose Android platform (If you are not) -> Click switch platform button.`

**Edit Androidmanifest.xml**
Find:File->Build and settings->Player Settings->Publishing Settins->Build->Custom Main Manifest

Check it, and you may see the path of this file.Let's edit it.

**Add permission of internet.**
```xml
<uses-permission android:name="android.permission.INTERNET" />
<queries>
    <intent>
        <action android:name="android.intent.action.VIEW" />
        <category android:name="android.intent.category.BROWSABLE" />
        <data android:scheme="https"/>
    </intent>
</queries>
```

**Register this activity**
```xml
<activity
    android:name="com.mirror.sdk.activities.RedirectActivity"
    android:exported="true">

    <intent-filter>
        <action android:name="android.intent.action.VIEW"/>
        <category android:name="android.intent.category.DEFAULT"/>
        <category android:name="android.intent.category.BROWSABLE"/>
        <data android:scheme="mwsdk"/>
    </intent-filter>
</activity>
```

So, the finnaly file would looked as this:
```xml
<?xml version="1.0" encoding="utf-8"?>
<manifest xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:tools="http://schemas.android.com/tools"
    package="com.mirror.mirrorworld_sdk_android">

    <uses-permission android:name="android.permission.INTERNET" />
    <queries>
        <intent>
            <action android:name="android.intent.action.VIEW" />
            <category android:name="android.intent.category.BROWSABLE" />
            <data android:scheme="https"/>
        </intent>
    </queries>

    <application
        android:allowBackup="true"
        android:dataExtractionRules="@xml/data_extraction_rules"
        android:fullBackupContent="@xml/backup_rules"
        android:icon="@mipmap/ic_launcher"
        android:label="@string/app_name"
        android:networkSecurityConfig="@xml/network_security_config"

        android:roundIcon="@mipmap/ic_launcher_round"
        android:supportsRtl="true"
        android:theme="@style/Theme.Mirrorworldsdkandroid"
        tools:targetApi="31">
        <activity
            android:name=".MainActivity"
            android:launchMode="singleTask"
            android:exported="true">
            <intent-filter>
                <action android:name="android.intent.action.MAIN" />
                <category android:name="android.intent.category.LAUNCHER" />
            </intent-filter>
        </activity>


        <activity
            android:name="com.mirror.sdk.activities.RedirectActivity"
            android:exported="true">

            <intent-filter>
                <action android:name="android.intent.action.VIEW"/>
                <category android:name="android.intent.category.DEFAULT"/>
                <category android:name="android.intent.category.BROWSABLE"/>
                <data android:scheme="mwsdk"/>
            </intent-filter>
        </activity>
    </application>

</manifest>
```

**Edit laucherTemplate.gradle**
Use the same function to open your laucherTemplate.gradle file.
Add this to dependencies:
```xml
implementation 'androidx.browser:browser:1.3.0'
```

So the finnal dependencies may like this:
```xml
apply plugin: 'com.android.application'

dependencies {
    implementation project(':unityLibrary')
    implementation 'androidx.browser:browser:1.3.0'
}
```

**Edit gradleTemplate.properties**
Add these to the end of the file:
```
android.useAndroidX=true
android.enableJetifier=true
```

**Besides, you can refer to Android Document to get more information.**
