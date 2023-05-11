using System;
using System.Collections.Generic;
using System.Xml.Schema;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class BoundingBox : MonoBehaviour
{
    private Bounds _bounds;

    //private readonly List<GameObject> _boxList = new List<GameObject>();
    private List<Bounds> _boundsList = new List<Bounds>();

    [SerializeField] private int divX;
    [SerializeField] private int divY;
    [SerializeField] private int divZ;

    private void Start()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        _bounds = new Bounds(mesh.bounds.center, mesh.bounds.size);

        // var size = _bounds.size;
        // var minPos = _bounds.min;
        //
        // var stepX = size.x / divX;
        // var stepY = size.y / divY;
        // var stepZ = size.z / divZ;
        

        var divSize = new Vector3(_bounds.size.x / divX, _bounds.size.y / divY, _bounds.size.z / divZ);
        var min = _bounds.min;

        for (var x = 0; x < divX; x++)
        {
            for (var y = 0; y < divY; y++)
            {
                for (var z = 0; z < divZ; z++)
                {
                    // var pos = new Vector3(minPos.x + stepX * i, minPos.y + stepY * j, minPos.z + stepZ * k);
                    // var box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    // box.transform.position = pos;
                    // box.transform.localScale = new Vector3(stepX, stepY, stepZ);
                    // _boxList.Add(box);
                    var center = new Vector3(min.x + divSize.x * (x + 0.5f), min.y + divSize.y * (y + 0.5f),
                        min.z + divSize.z * (z + 0.5f));
                    var newBounds = new Bounds(center, divSize);
                    _boundsList.Add(newBounds);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var bounds = new Bounds(mesh.bounds.center, mesh.bounds.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(bounds.center, bounds.size);
        
        foreach (var newBounds in _boundsList)
        {
            Gizmos.color = UnityEngine.Random.ColorHSV();
            Gizmos.DrawWireCube(newBounds.center, newBounds.size);
        }
    }
}