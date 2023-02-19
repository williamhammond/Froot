using System;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// A fix for Cloud Build while using Wwise. Add to Editor Folder
/// Based on solution from Blackbox Realities: https://github.com/BlackBoxSimulations/wwise-unity-cloud-build-fixer
/// </summary>
public class WwiseFixEditor : MonoBehaviour {
    [MenuItem("Tools/Fix Wwise Cloud Build")]
    public static void RemoveWwiseScriptsFix () {
        var path = EditorUtility.OpenFolderPanel("Wwise Root Folder in Assets", "", "");
        ApplyFix(path);
        AssetDatabase.Refresh();
    }

    private static void ApplyFix (string path) {
        ReplaceFiles(Directory.GetFiles(path));

        var directories = Directory.GetDirectories(path);
        foreach (var directory in directories) {
            ApplyFix(directory);
        }
    }
    
    private static void ReplaceFiles (string[] files) {
        foreach (var file in files) {
            if (!file.EndsWith(".cs")) {
                continue;
            }
            
            var text = File.ReadAllText(file);
            var lines = text.Split(Environment.NewLine);
            for (var i = 0; i < lines.Length; i++) {
                var line = lines[i];
                foreach (WwiseReplacement patch in WwisePatches) {
                    lines[i] = patch.Replace(line);
                }
            }
            File.WriteAllLines(file, lines);
        }
    }
        
    private sealed class WwiseReplacement {
        private readonly string _original;
        private readonly string _replacement;

        public WwiseReplacement (string original, string replacement) {
            _original = original;
            _replacement = replacement;
        }
        
        public string Replace (string line) {
            var regex = new Regex(_original, RegexOptions.Multiline);
            if (regex.IsMatch(line)) {
                return regex.Replace(line, _replacement);
            }
            return line;
        }
    }

    private static readonly WwiseReplacement[] WwisePatches = {
        // Mac
        new("#if (UNITY_STANDALONE_OSX && !UNITY_EDITOR) || UNITY_EDITOR_OSX", "#if (UNITY_STANDALONE_OSX && !UNITY_EDITOR) || (UNITY_EDITOR_OSX && !UNITY_STANDALONE_WIN)"),
        
        // Windows
        new("#if (UNITY_STANDALONE_WIN && !UNITY_EDITOR) || UNITY_EDITOR_WIN", "#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN || UNITY_WSA"),
        
        //Linux
        new("#if UNITY_STANDALONE_LINUX && ! UNITY_EDITOR", "#if UNITY_EDITOR || (UNITY_STANDALONE_LINUX && !UNITY_EDITOR)"),
        new("#if UNITY_STANDALONE_LINUX && !UNITY_EDITOR", "#if UNITY_EDITOR || (UNITY_STANDALONE_LINUX && !UNITY_EDITOR)"),
        
        // Android
        new("#if UNITY_ANDROID && !UNITY_EDITOR", "#if (UNITY_ANDROID && !UNITY_EDITOR) || (UNITY_ANDROID && UNITY_EDITOR_LINUX)"),
        new("#if UNITY_ANDROID && ! UNITY_EDITOR", "#if (UNITY_ANDROID && !UNITY_EDITOR) || (UNITY_ANDROID && UNITY_EDITOR_LINUX)"),
        
        // iOS
        new("#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IOS && UNITY_EDITOR_LINUX)", "#if UNITY_IOS && !UNITY_EDITOR"),
        new("#if UNITY_IOS && ! UNITY_EDITOR", "#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IOS && UNITY_EDITOR_LINUX)"),
        new("#if (UNITY_IOS || UNITY_TVOS) && !UNITY_EDITOR", "#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IOS && UNITY_EDTIOR_LINUX)"),
        new("#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IOS && UNITY_EDTIOR_LINUX) || (UNITY_TVOS && !UNITY_EDITOR) || (UNITY_TVOS && UNITY_EDITOR_LINUX)", "#if (UNITY_IOS && !UNITY_EDITOR) || (UNITY_IOS && UNITY_EDTIOR_LINUX)"),
        
        // Switch
        new("#if (UNITY_SWITCH || UNITY_ANDROID || UNITY_STANDALONE_LINUX || UNITY_STADIA) && !UNITY_EDITOR", "#if (UNITY_SWITCH || UNITY_ANDROID || UNITY_STANDALONE_LINUX || UNITY_STADIA) && (!UNITY_EDITOR || UNITY_EDITOR_LINUX)")
    };
}