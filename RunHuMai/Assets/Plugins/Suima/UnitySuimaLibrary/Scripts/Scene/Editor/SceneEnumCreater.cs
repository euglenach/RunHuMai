using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public class SceneEnumCreater{
    private const string ItemName = "Tools/CreateSceneEnum";
    private const string CsFilePath = "Assets/Plugins/Suima/Scene/Scene.cs";
    private static readonly string FileName = Path.GetFileName(CsFilePath);
    private static readonly string FileNameWithoutExtension = Path.GetFileNameWithoutExtension(CsFilePath);

    [MenuItem(ItemName, true)]
    public static bool CanCreate() =>
        !EditorApplication.isPlaying && !Application.isPlaying && !EditorApplication.isCompiling;

    [MenuItem(ItemName)]
    public static void Create(){
        if(!CanCreate()) return;

        CreateScript();
    }

    private static void CreateScript(){
        var builder = new StringBuilder();

        builder.AppendLine("namespace Suima.Scene{")
               .AppendLine("\t/// <summary>")
               .AppendLine("\t/// シーン名を管理する Enum")
               .AppendLine("\t/// </summary>")
               .AppendLine($"\tpublic enum {FileNameWithoutExtension} {{");

        foreach(var n in EditorBuildSettings.scenes.Select(c => Path.GetFileNameWithoutExtension(c.path))
                                            .Distinct()
                                            .Select(c => new{var = RemoveInvalidChars(c), val = c})){
            builder.AppendLine($"\t\t{n.var},");
        }

        builder.AppendLine("\t}");
        builder.AppendLine("}");

        if(!Directory.Exists(FileName)){ Directory.CreateDirectory(FileName); }

        File.WriteAllText(CsFilePath, builder.ToString(), Encoding.UTF8);
        AssetDatabase.Refresh(ImportAssetOptions.ImportRecursive);
    }
    
    public static string RemoveInvalidChars ( string str )
    {
        Array.ForEach ( INVALUD_CHARS, c => str = str.Replace ( c, string.Empty ) );
        return str;
    }
    private static readonly string[] INVALUD_CHARS =
    {
        " ", "!", "\"", "#", "$",
        "%", "&", "\'", "(", ")",
        "-", "=", "^",  "~", "\\",
        "|", "[", "{",  "@", "`",
        "]", "}", ":",  "*", ";",
        "+", "/", "?",  ".", ">",
        "<", ",",
    };
}
