#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

public class SetAssetBundleNameByAddressableGroups : MonoBehaviour
{
    public AddressableAssetGroup[] groups;

    [ContextMenu("Execute")]
    public void Execute()
    {
        string assetPath;
        AssetImporter assetImporter;
        foreach (AddressableAssetGroup group in groups)
        {
            foreach (AddressableAssetEntry entry in group.entries)
            {
                assetPath = AssetDatabase.GetAssetPath(entry.MainAsset.GetInstanceID());
                assetImporter = AssetImporter.GetAtPath(assetPath);
                assetImporter.SetAssetBundleNameAndVariant(group.Name, "");
                Debug.Log("-- Set " + assetPath + " asset bundle name to " + group.Name);
            }
        }
    }
}
#endif