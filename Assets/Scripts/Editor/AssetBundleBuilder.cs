using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class AssetBundleBuilder : Editor
{
    [MenuItem("Assets/Build to asset bundle")]
    static void BuildLevelAssetBundle()
    {
        BuildPipeline.BuildAssetBundles("Assets/Modpack", BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
    }
}
