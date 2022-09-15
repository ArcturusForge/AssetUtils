using System.IO;
using UnityEditor;

public class EditorType : CreatorProfile
{
    [MenuItem("Assets/Create/Scripts/EditorType", priority = 20)]
    public static void CreateMenu()
    {
        FileCreator.GenerateWindow<EditorType>();
    }

    public override void GenerateFile(string path, string scriptName)
    {
        using StreamWriter outfile = new StreamWriter(path);
        outfile.WriteLine("using UnityEngine;");
        outfile.WriteLine("using UnityEditor;");
        outfile.WriteLine("");
        outfile.WriteLine("[CustomEditor(typeof())]");
        outfile.WriteLine($"public class {scriptName} : Editor");
        outfile.WriteLine("{");
        outfile.WriteLine("");
        outfile.WriteLine("    private SerializedProperty Property(string path)");
        outfile.WriteLine("    {");
        outfile.WriteLine("        return serializedObject.FindProperty(path);");
        outfile.WriteLine("    }");
        outfile.WriteLine("");
        outfile.WriteLine("    public override void OnInspectorGUI()");
        outfile.WriteLine("    {");
        outfile.WriteLine("        serializedObject.Update();");
        outfile.WriteLine("        ");
        outfile.WriteLine("        ");
        outfile.WriteLine("        ");
        outfile.WriteLine("        if (serializedObject.hasModifiedProperties)");
        outfile.WriteLine("            serializedObject.ApplyModifiedProperties();");
        outfile.WriteLine("    }");
        outfile.WriteLine("}");
    }
}
