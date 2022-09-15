using UnityEditor;

public class ScriptSplitFab : FolderProfile
{
    [MenuItem("Assets/Create/FolderFabs/ScriptSplitFab", priority = 19)]
    public static void CreateMenu()
    {
        FolderFabricator.GenerateFolderPrefab<ScriptSplitFab>();
    }

    public override void GenerateFab(string path)
    {
        var engFolder = BuildFolder(path, "Engine");
        var gnFolder = BuildInsideFolder(engFolder, "Generics");
        var inFolder = BuildInsideFolder(engFolder, "Interfaces");
        var mnFolder = BuildInsideFolder(engFolder, "Monos");
        var soFolder = BuildInsideFolder(engFolder, "Scriptables");
        var edFolder = BuildFolder(path, "Editor");
    }
}
