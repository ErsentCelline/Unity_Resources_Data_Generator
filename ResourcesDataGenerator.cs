using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

public static class ResourcesDataGenerator
{
    private static string Root => Path.Combine(Application.dataPath + "/Resources");
    private static string DestinationPath => Path.Combine(Application.dataPath + "/_App/Scripts/Data"); // Can be modify
    
    public static void GenerateCode()
    {
        CheckDirectoryExists(DestinationPath); // Ensure destination exists, create if not.

        var data = new DirectoryData // Initialize root structure with script name
        {
            Name = "ResourcesData" 
        };

        ProcessDirectory(Root, ref data); // Create tree of structures

        var sb = new StringBuilder();
        sb.AppendLine(data.ToString());
        
        File.WriteAllText(Path.Combine(DestinationPath, data.Name + ".cs") , sb.ToString());
        
        AssetDatabase.Refresh();
    }

    private static void CheckDirectoryExists(string path)
    {
        if (Directory.Exists(path)) return;
        
        Directory.CreateDirectory(path);
    }

    private static void ProcessDirectory(string directoryPath, ref DirectoryData directoryData)
    {
        WriteDirectories(directoryPath, ref directoryData);
        WriteFiles(directoryPath, ref directoryData);
        
        for (int i = 0; i < directoryData.Directories.Length; i++) 
            ProcessDirectory(Path.Combine(directoryPath, directoryData.Directories[i].Name), ref directoryData.Directories[i]);
    }

    // Add all directories on path
    private static void WriteDirectories(string root, ref DirectoryData directoryData)
    {
        string[] directories = Directory.GetDirectories(root);

        directoryData.Directories = new DirectoryData[directories.Length];

        for (int i = 0; i < directories.Length; i++)
            directoryData.Directories[i] = new DirectoryData
            {
                Name = Path.GetFileName(directories[i]),
            };
    }

    // Add all files on path
    private static void WriteFiles(string root, ref DirectoryData directoryData)
    {
        string[] filePaths = Directory.GetFiles(root).Where(file => !file.EndsWith(".meta")).ToArray();
        
        directoryData.NameToPath = new Dictionary<string, string>();
        
        foreach (string filePath in filePaths)
            directoryData.NameToPath.Add(GeneratePropertyName(Path.GetFileNameWithoutExtension(filePath)), filePath);
    }

    // Generate and return valid property name
    private static string GeneratePropertyName(string filePath)
    {
        string fileName = Regex.Replace(
            filePath,
            @"[^a-zA-Z0-9]",
            " ",
            RegexOptions.Compiled
        );
        
        string[] words = fileName.Split(
            new[] { ' ', '-', '_' },
            StringSplitOptions.RemoveEmptyEntries
        );
        
        var sb = new StringBuilder();
        
        if (char.IsDigit(words[0][0]))
            sb.Append('_');

        foreach (string word in words)
        {
            if (word.Length > 0)
                sb.Append(char.ToUpper(word[0]));
            
            if (word.Length > 1)
                sb.Append(word[1..].ToLower());
        }
        
        return sb.ToString();
    }
    
    public struct DirectoryData
    {
        public string Name;
        public DirectoryData[] Directories;
        public Dictionary<string, string> NameToPath;

        public string ToString(int tabCount = 0)
        {
            var sb = new StringBuilder();
            
            string tab = new('\t', tabCount);
            string innerTab = new('\t', tabCount + 1);
            
            sb.AppendLine($"\n{tab}public class {Name}\n{tab}{{");

            foreach (var directory in Directories)
                sb.AppendLine(directory.ToString(tabCount + 1));

            foreach (var pair in NameToPath)
            {
                string assetPath = pair.Value.Remove(0, Root.Length + 1);
                var type = AssetDatabase.GetMainAssetTypeAtPath("Assets/Resources/" + assetPath);
                assetPath = assetPath.Split('.')[0];
                
                sb.AppendLine($"{innerTab}public static {type} {pair.Key} => UnityEngine.Resources.Load<{type}>(\"{assetPath}\");");
            }
            
            sb.Append($"{tab}}}");
            
            return sb.ToString();
        }
    }
}
