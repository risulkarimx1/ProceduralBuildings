using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class OpenFBXFilePanel : EditorWindow
{
    private const string PREFAB_DESTINATION_DIRECTORY = "Assets/Prefabs/";
    [MenuItem("AAI/Read FBX")]
    static void OpenFBX()
    {
        var window = GetWindowWithRect<OpenFBXFilePanel>(new Rect(0, 0, 512, 512));
        window.Show();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        GUILayout.Label("1- Select any object in Assets ");
        
        if (Selection.activeGameObject) {
             GUILayout.Label(Selection.activeGameObject.name);
        }
        if (GUILayout.Button("Search!"))
        {
            string filePath = GetSelectedPathOrFallback();
            Debug.Log(filePath);
            foreach (var file in GetFiles(filePath)) {
                Debug.Log($"File {file}");
            }
        }
        if (GUILayout.Button("Readable"))
        {
            foreach (var file in GetFiles(GetSelectedPathOrFallback()))
            {
                AssetDatabase.ImportAsset(file, ImportAssetOptions.Default);
            }
        }
        if (GUILayout.Button("Make Prefab"))
        {
            EnsureDirectoryExists(PREFAB_DESTINATION_DIRECTORY);
            foreach (var file in GetFiles(GetSelectedPathOrFallback()))
            {
                GameObject modelAsset = AssetDatabase.LoadAssetAtPath<GameObject>(file);
                Debug.Log($"Name of game object {modelAsset.name}");
                var instanceRoot = PrefabUtility.InstantiatePrefab(modelAsset) as GameObject;
                instanceRoot.AddComponent<Measurement>();
                string destinationPath = PREFAB_DESTINATION_DIRECTORY + modelAsset.name + ".prefab";
                PrefabUtility.SaveAsPrefabAsset(instanceRoot, destinationPath);
                DestroyImmediate(instanceRoot);
                // AssetDatabase.ImportAsset(file, ImportAssetOptions.Default);
                //string destinationPath = PREFAB_DESTINATION_DIRECTORY + file + ".prefab";
                //if (!File.Exists(destinationPath))
                //{
                //    PrefabUtility.CreatePrefab(
                //        destinationPath,
                //        modelAsset);
                //}
                //else
                //{
                //    PrefabUtility.ReplacePrefab(
                //        modelAsset,
                //        AssetDatabase.LoadAssetAtPath<GameObject>(destinationPath),
                //        ReplacePrefabOptions.ReplaceNameBased);
                //}
            }
        }
        EditorGUILayout.EndHorizontal();
        this.Repaint();
    }
    private string GetSelectedPathOrFallback()
    {
        string path = "Assets";

        foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
        {
            path = AssetDatabase.GetAssetPath(obj);
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                path = Path.GetDirectoryName(path);
                break;
            }
        }
        return path;
    }
    private IEnumerable<string> GetFiles(string path)
    {
        Queue<string> queue = new Queue<string>();
        queue.Enqueue(path);
        while (queue.Count > 0)
        {
            path = queue.Dequeue();
            string[] files = null;
            try
            {
                //files = Directory.GetFiles(path);
                files = Directory.GetFiles(path).Where(s => 
                s.Contains(".fbx") == true &&
                s.Contains(".meta") == false
                ).ToArray<string>();
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex.Message);
            }
            if (files != null)
            {
                for (int i = 0; i < files.Length; i++)
                {
                    yield return files[i];
                }
            }
        }
    }
    private static void EnsureDirectoryExists(string directory)
    {
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
    }
}
