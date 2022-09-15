using UnityEditor;

public class StreamingAssetsFab : FolderProfile
{
    [MenuItem("Assets/Create/FolderFabs/StreamingAssetsFab", priority = 20)]
    public static void CreateMenu()
    {
        FolderFabricator.GenerateFolderPrefab<StreamingAssetsFab>();
    }

    public override void GenerateFab(string path)
    {
        var strFolder = BuildFolder(path, "StreamingAssets");
    }
}
