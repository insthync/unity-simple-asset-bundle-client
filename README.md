# SimpleAssetBundleClient
A simple asset bundle client which is not best solution for your game, it will download all asset bundles which defined in platform manifest when start the game, if you like to load just some asset partially when change map or something you should find other solution.

## How to use
- You have to know how to prepare asset bundles, you can use https://docs.unity3d.com/Manual/AssetBundles-Browser.html to create asset bundles file or other solution.
- You also have to know how to upload files to web-server. Because you have to upload asset bundles files for the clients.
- You also can create asset bundle files for the game-server.
- Set configs in `AssetBundleManager` component:
  - `Server Url` is URL to where which contains all asset bundles for all platforms, in this folder will contains asset bundle folders for specific platforms such as **StandaloneLinux64**, **StandaloneWindows64**, **Android** and so on. This URL will be used while `Load Mode` is `From Server Url`.
  - `local Folder Path` is relative path from executable file (such as Project.exe) to folder which contains all asset bundles for the build platform, in this folder still have to contains asset bundle folders for specific platform, for example if I've built the game-server which running on **StandaloneLinux64** to `/root/project/` then upload asset bundles to `/root/project/assetbundle/StandaloneLinux64`, I will set this config to `/assetbundle`.
  - `Init Scene Name` is a scene name which will be loaded after all asset bundles downloaded.
  - `Load Mode` load mode for the executable file, it can be overrided by specific platform settings.
  - `Editor Load Mode` load mode for the Unity editor, it can be overrided by specific platform settings.
- You can exclude scenes which included in asset bundles from `Scenes In Build` setting and may set asset bundle loading scene as a initializing scene.
- You should disable `Strip Engine Code` because if there are gameplay scenes which included in asset bundles were excluded from `Scenes In Build`, some codes will be excluded from the build too, so your game may not work properly.
