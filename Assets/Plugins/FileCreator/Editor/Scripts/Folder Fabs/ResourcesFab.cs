using UnityEditor;

public class ResourcesFab : FolderProfile
{
    [MenuItem("Assets/Create/FolderFabs/ResourcesFab", priority = 20)]
    public static void CreateMenu()
    {
        FolderFabricator.GenerateFolderPrefab<ResourcesFab>();
    }

    public override void GenerateFab(string path)
    {
        var rsceFolder = BuildFolder(path, "Resources");
    }
}
