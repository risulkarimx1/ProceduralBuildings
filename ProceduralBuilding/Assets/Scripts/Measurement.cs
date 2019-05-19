using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class Measurement : MonoBehaviour
{
    public static bool makeReadable = false;
    public float Width { get; private set; }
    public float Height { get; private set; }
    public ModuleType CurrentModuleType { get; set; }

    public void MeasureBlock(int moduleNumber)
    {
        var mesh = GetComponent<MeshFilter>().mesh;
        var width = mesh.bounds.max.x - mesh.bounds.min.x;
        var height = mesh.bounds.max.y - mesh.bounds.min.y;
        Width = width;
        Height = Height;
        //CurrentModuleType = 
    }
}
