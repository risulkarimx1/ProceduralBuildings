using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class OpenFBXFilePanel : EditorWindow
{
   public Object source;
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
            Selection.activeGameObject.name =
            EditorGUILayout.TextField("Object Name: ", Selection.activeGameObject.name);
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
}
