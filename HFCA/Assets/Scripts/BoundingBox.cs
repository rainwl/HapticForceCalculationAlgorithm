using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class BoundingBox : MonoBehaviour
{
    private Bounds bounds;
    private List<GameObject> boxList = new List<GameObject>();
    
    [SerializeField] private int div_x;
    [SerializeField] private int div_y;
    [SerializeField] private int div_z;

    private void Start()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        bounds = new Bounds(mesh.bounds.center, mesh.bounds.size);
        
        var size = bounds.size;
        var minPos = bounds.min;
        var stepX = size.x / div_x;
        var stepY = size.y / div_y;
        var stepZ = size.z / div_z;

        for (var i = 0; i < div_x; i++)
        {
            for (var j = 0; j < div_y; j++)
            {
                for (var k = 0; k < div_z; k++)
                {
                    var pos = new Vector3(minPos.x + stepX * i, minPos.y + stepY * j, minPos.z + stepZ * k);
                    var box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    box.transform.position = pos;
                    box.transform.localScale = new Vector3(stepX, stepY, stepZ);
                    boxList.Add(box);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        var mesh = GetComponent<MeshFilter>().sharedMesh;
        var _bounds = new Bounds(mesh.bounds.center, mesh.bounds.size);
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_bounds.center, _bounds.size);
    }
}
