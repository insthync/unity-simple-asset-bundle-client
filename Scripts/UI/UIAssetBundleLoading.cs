using UnityEngine;
using UnityEngine.UI;

namespace SimpleABC
{
    public partial class UIAssetBundleLoading : MonoBehaviour
    {
        public string formatLoadingAssetBundleFileName = "Downloading.. {0}";
        public string formatLoadingAssetBundleFromCacheFileName = "Initializing.. {0}";
        public string formatLoadedAssetBundles = "{0}/{1}";
        public string formatLoadingSpeedPerSeconds = "{0} {1}/s";
        public string formatLoadingRemainingSeconds = "{0} Seconds";
        public string formatLoadingAssetBundleFileSize = "{0} {1}";
        public GameObject rootObject;
        public Text uiTextProgress;
        public Image imageGage;
        public GameObject totalRootObject;
        public Text uiTextTotalProgress;
        public Image imageGageTotal;
        public Text textLoadingAssetBundleFileName;
        public Text textLoadedAssetBundlesCount;
        public Text textLoadingSpeedPerSeconds;
        public Text textLoadingRemainingSeconds;
        public Text textLoadingAssetBundleFileSize;
        public GameObject[] loadingSignalObjects;
        public GameObject[] loadedSignalObjects;

        private void Awake()
        {
            if (rootObject != null)
                rootObject.SetActive(false);

            if (totalRootObject != null)
                totalRootObject.SetActive(false);
        }

        private void Update()
        {
            switch (AssetBundleManager.Singleton.CurrentLoadState)
            {
                case AssetBundleManager.LoadState.None:
                case AssetBundleManager.LoadState.LoadManifest:
                case AssetBundleManager.LoadState.Done:
                    if (rootObject != null)
                        rootObject.SetActive(false);
                    if (totalRootObject != null)
                        totalRootObject.SetActive(false);
                    if (textLoadingAssetBundleFileName != null)
                        textLoadingAssetBundleFileName.text = string.Empty;
                    if (textLoadedAssetBundlesCount != null)
                        textLoadedAssetBundlesCount.text = string.Empty;
                    break;
                case AssetBundleManager.LoadState.LoadAssetBundles:
                    if (AssetBundleManager.Singleton.LoadingAssetBundlesCount > 0 ||
                        AssetBundleManager.Singleton.LoadingAssetBundlesFromCacheCount > 0)
                    {
                        if (rootObject != null)
                            rootObject.SetActive(true);

                        if (AssetBundleManager.Singleton.LoadingAssetBundlesCount > 0)
                        {
                            if (totalRootObject != null)
                                totalRootObject.SetActive(true);
                            if (uiTextProgress != null)
                                uiTextProgress.text = (AssetBundleManager.Singleton.CurrentWebRequest.downloadProgress * 100f).ToString("N2") + "%";
                            if (imageGage != null)
                                imageGage.fillAmount = AssetBundleManager.Singleton.CurrentWebRequest.downloadProgress;
                            if (uiTextTotalProgress != null)
                                uiTextTotalProgress.text = (AssetBundleManager.Singleton.TotalLoadProgress * 100f).ToString("N2") + "%";
                            if (imageGageTotal != null)
                                imageGageTotal.fillAmount = AssetBundleManager.Singleton.TotalLoadProgress;
                            if (textLoadedAssetBundlesCount != null)
                                textLoadedAssetBundlesCount.text = string.Format(formatLoadedAssetBundles, AssetBundleManager.Singleton.LoadedAssetBundlesCount, AssetBundleManager.Singleton.LoadingAssetBundlesCount);
                        }

                        if (textLoadingAssetBundleFileName != null)
                            textLoadingAssetBundleFileName.text = !string.IsNullOrEmpty(AssetBundleManager.Singleton.LoadingAssetBundleFileName) ? string.Format(formatLoadingAssetBundleFileName, AssetBundleManager.Singleton.LoadingAssetBundleFileName) : string.Empty;

                        if (!string.IsNullOrEmpty(AssetBundleManager.Singleton.LoadingAssetBundleFileName))
                        {
                            if (textLoadingSpeedPerSeconds != null)
                                textLoadingSpeedPerSeconds.text = AssetBundleManager.Singleton.LoadingSpeedPerSeconds <= 0 ? string.Empty : SizeSuffix(formatLoadingSpeedPerSeconds, AssetBundleManager.Singleton.LoadingSpeedPerSeconds);
                            if (textLoadingRemainingSeconds != null)
                                textLoadingRemainingSeconds.text = double.IsInfinity(AssetBundleManager.Singleton.LoadingRemainingSeconds) ? string.Empty : string.Format(formatLoadingRemainingSeconds, AssetBundleManager.Singleton.LoadingRemainingSeconds.ToString("N0"));
                            if (textLoadingAssetBundleFileSize != null)
                                textLoadingAssetBundleFileSize.text = SizeSuffix(formatLoadingAssetBundleFileSize, AssetBundleManager.Singleton.LoadingAssetBundleFileSize);
                        }
                        else
                        {
                            if (textLoadingSpeedPerSeconds != null)
                                textLoadingSpeedPerSeconds.text = string.Empty;
                            if (textLoadingRemainingSeconds != null)
                                textLoadingRemainingSeconds.text = string.Empty;
                            if (textLoadingAssetBundleFileSize != null)
                                textLoadingAssetBundleFileSize.text = string.Empty;
                        }
                    }
                    break;
            }
            bool isDone = AssetBundleManager.Singleton.CurrentLoadState == AssetBundleManager.LoadState.Done;
            foreach (GameObject obj in loadingSignalObjects)
            {
                obj.SetActive(!isDone);
            }
            foreach (GameObject obj in loadedSignalObjects)
            {
                obj.SetActive(isDone);
            }
        }

        static readonly string[] SizeSuffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        static string SizeSuffix(string format, double value, int decimalPlaces = 2)
        {
            if (decimalPlaces < 0) decimalPlaces = 2;
            if (value < 0) { return "-" + SizeSuffix(format, -value, decimalPlaces); }
            if (value == 0) { return string.Format("{0:n" + decimalPlaces + "} bytes", 0); }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)System.Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (System.Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format(format, adjustedSize.ToString("N" + decimalPlaces), SizeSuffixes[mag]);
        }
    }
}
