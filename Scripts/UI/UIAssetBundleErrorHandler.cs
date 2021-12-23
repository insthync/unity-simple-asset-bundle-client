using UnityEngine;
using UnityEngine.Events;

namespace SimpleABC
{
    public class UIAssetBundleErrorHandler : MonoBehaviour
    {
        public UnityEvent onManifestLoadedFail = new UnityEvent();
        public UnityEvent onAssetBundlesLoadedFail = new UnityEvent();

        private void Start()
        {
            AssetBundleManager.Singleton.onManifestLoadedFail.RemoveListener(OnManifestLoadedFail);
            AssetBundleManager.Singleton.onManifestLoadedFail.AddListener(OnManifestLoadedFail);
            AssetBundleManager.Singleton.onAssetBundlesLoadedFail.RemoveListener(OnAssetBundlesLoadedFail);
            AssetBundleManager.Singleton.onAssetBundlesLoadedFail.AddListener(OnAssetBundlesLoadedFail);
        }

        private void OnDestroy()
        {
            AssetBundleManager.Singleton.onManifestLoadedFail.RemoveListener(OnManifestLoadedFail);
            AssetBundleManager.Singleton.onAssetBundlesLoadedFail.RemoveListener(OnAssetBundlesLoadedFail);
        }

        private void OnManifestLoadedFail()
        {
            onManifestLoadedFail.Invoke();
        }

        private void OnAssetBundlesLoadedFail()
        {
            onAssetBundlesLoadedFail.Invoke();
        }
    }
}
