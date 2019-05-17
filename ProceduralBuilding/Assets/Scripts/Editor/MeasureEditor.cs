using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Measurement))]
public class MeasureEditor : Editor
{
    Measurement measurement;
    private void OnEnable()
    {
        measurement = (Measurement)target;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Unreadble"))
        {
            //ModelImport modelImport = new ModelImport();
            //modelImport.MakeUnreadble();
            Measurement.makeReadable = !Measurement.makeReadable;
            AssetDatabase.ImportAsset("Assets/Models/Blender/X2.fbx", ImportAssetOptions.Default);
        }
    }
}
