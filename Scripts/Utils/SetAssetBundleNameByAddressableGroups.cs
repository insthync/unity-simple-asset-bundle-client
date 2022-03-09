#if UNITY_EDITOR && USE_ADDRESSABLE_ASSETS
using System.Collections.Generic;
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
            List<AddressableAssetEntry> entries = new List<AddressableAssetEntry>(group.entries);
            foreach (AddressableAssetEntry entry in entries)
            {
                assetPath = AssetDatabase.GetAssetPath(entry.MainAsset.GetInstanceID());
                assetImporter = AssetImporter.GetAtPath(assetPath);
                assetImporter.SetAssetBundleNameAndVariant(group.Name, "");
                group.RemoveAssetEntry(entry);
                Debug.Log("-- Set " + assetPath + " asset bundle name to " + group.Name);
            }
        }
    }
}
#endif