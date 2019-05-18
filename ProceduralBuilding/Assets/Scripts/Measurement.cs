using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Measurement : MonoBehaviour
{

    public static bool makeReadable = false;
    public Mesh SharedMesh
    {
        get {
            return GetComponent<MeshFilter>().sharedMesh;
        }
        
    }
    private void Start()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
       
        var bounds = mesh.bounds;
        var min = bounds.min;
        var max = bounds.max;
        var width = max.x - min.x;
        var height = max.y - min.y;
        Debug.Log($"{name} W H {width},{height}");
    }
}
